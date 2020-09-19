# Tests

This directory contains functional tests based on NodeJS, Jasmine, and Chai. They are
designed to be run within a container as part of a continuous integration pipeline.

## Running Outside Containers

Assuming the application is already launched on your local machine, the tests may be
executed outside of containers by setting environment variables.

For Linux/MacOS:

```sh
export BASE_URL=http://localhost:8080/
npm run build
npm run test
```

For Windows:

```powershell
$env:BASE_URL = "http://localhost:8080/"
npm run build
npm run test
```

## Running Containerized

To run containerized execute the following commands from the root of the repository:

```sh
# Cleanup any earlier test runs
docker-compose -f docker-compose.yml -f docker-compose.debug.yml -f docker-compose.test.yml down -v

# Build images
docker-compose -f docker-compose.yml -f docker-compose.debug.yml -f docker-compose.test.yml build --parallel

# Execute tests
docker-compose -f docker-compose.yml -f docker-compose.debug.yml -f docker-compose.test.yml up --abort-on-container-exit --exit-code-from tests
```
