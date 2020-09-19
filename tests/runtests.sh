#!/bin/bash

# This script runs the tests within the test container, it is not meant to be run
# except as part of docker-compose.test.yml

set -e

until $(curl --output /dev/null --silent --fail $READY_URL); do
  echo 'Waiting for application...'
  sleep 1
done

npm run test "$@"
