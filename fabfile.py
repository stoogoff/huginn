#!/usr/bin/python

from fabric.api import *
from contextlib import contextmanager as _contextmanager


# prepare environment
env.hosts = ["storytella"]
env.serverPath = "/var/www/html/"
env.migrateApps = ("core", "blog")
env.user = "ubuntu"
env.key_filename = ["~/.aws/Stoo-key-pair-eu.pem"]
env.cdn = "weevolve-cdn"

# use virtual environment helper

@_contextmanager
def virtualenv():
	''' cd into project_dir and run a command using virtualenv python '''
	with cd(env.serverPath):
		with prefix("source ../env/bin/activate"):
			yield


# tasks

@task
def deploy(*args,**kwargs):
	"""
	Deploy the supplied tag to the server.

	fab deploy:tag=xxx
	"""
	with cd(env.serverPath):
		# fetch
		run("git fetch")
		run("git fetch --tags")

		if "tag" in kwargs:
			# checkout a live tag
			run("git checkout %(tag)s" % kwargs)

			upload_static(*args, **kwargs)

			run("touch muninn/wsgi.py")


@task
def upload_static(*args, **kwargs):
	"""
	Upload static content to S3.
	"""
	if "tag" in kwargs:
		with virtualenv():
			import os
			import boto
			from boto.s3.key import Key

			run("python manage.py collectstatic -v0 --noinput")

			s3 = boto.connect_s3()
			bucket = s3.get_bucket(env.cdn)
			readPath = os.path.join(os.path.dirname(__file__), "static") + "/"
			bucketPath = "storytella/%(tag)s/" % kwargs

			files = []

			for (dirpath, dirnames, filenames) in os.walk(readPath):
				for f in filenames:
					files.append(os.path.join(dirpath, f))

			for f in files:
				key = Key(bucket)
				key.key = bucketPath + f.replace(readPath, "")
				key.set_contents_from_filename(f)

				print "Updating CDN: %s" % key.key

			# TODO - delete previous tag from S3, the information will be in the current tag file

			# write out tag file to disk
			run("echo '%(tag)s' > muninn/settings/tag" % kwargs)


@task
def sync_couch():
	"""
	Update couchdb.

	fab sync_couch
	"""
	with cd(env.serverPath):
		with virtualenv():
			run("python manage.py sync_couch")


@task
def migrate_db(*args, **kwargs):
	"""
	Run South migrations for all apps or a specified app.

	fab migrate_db
	fab migrate_db:app=core
	"""
	with cd(env.serverPath):
		with virtualenv():
			if kwargs and "app" in kwargs:
				run("python manage.py migrate %(app)s" % kwargs)
			else:
				for app in env.migrateApps:
					run("python manage.py migrate %s" % app)


@task
def setupdb():
	"""
	Setup and update couch and mysql db's.

	fab setupdb
	"""
	with cd(env.serverPath):
		with virtualenv():
			run("python manage.py syncdb")

	migrate_db()
	sync_couch()


@task
def syncdb():
	"""
	Fully update couch and mysql db's.

	fab syncdb
	"""
	migrate_db()
	sync_couch()


@task
def install():
	with cd(env.serverPath):
		with virtualenv():
			run("pip install -r requirements.txt")


@task
def run_tests():
	"""
	Run all tests locally. This assumes it's being run from within the correct environment.

	fab run_tests
	"""
	# run python tests
	local("python manage.py test core")

	# prepare database
	local("python manage.py delete_users")
	local("python manage.py create_users")

	# run cucumber tests
	local("./support/scripts/cucumber.sh")


@task
def restart_servers():
	"""
	Restart Nginx servers.

	fab restart_servers
	"""
	sudo("service nginx restart", shell=False, pty=False)

