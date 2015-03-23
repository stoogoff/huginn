#!/usr/bin/python

from fabric.api import *


# prepare environment
env.hosts = ["couchdb"]
env.serverPath = "/var/www/html/"
env.migrateApps = ("core", "blog")
env.user = "ubuntu"
env.key_filename = ["~/.aws/Stoo-key-pair-eu.pem"]

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
def sync_couch():
	"""
	Update couchdb.

	fab sync_couch
	"""
	with cd(env.serverPath):
		with virtualenv():
			run("python manage.py sync_couch")


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

