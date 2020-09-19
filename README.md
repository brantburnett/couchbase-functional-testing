# Couchbase Functional Testing

This repository contains an example for supporting both local machine development and
continuous integration testing using containerized instances of Couchbase. The application
is written in C# using .NET Core. The functional tests are written using NodeJS and Typescript.
However, this pattern could be applied to any

## Prerequisites

- [Docker](https://www.docker.com/)
- [Visual Studio Code](https://code.visualstudio.com/) (not required, but makes some steps easier)
- [Docker extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker)

> :info: This approach may also be applied to Visual Studio using docker-compose projects. In fact, the rebuild/debug cycle
> for local development is more efficient in Visual Studio. However, Visual Studio Code was chosen for this example because
> it's free, cross-platform, and cross-language, and therefore reaches a broader audience. Porting the docker-compose files
> from this example to a Visual Studio style docker-compose project is a relatively straightforward process.

## Configuring Couchbase

The Couchbase instance launched in a container is configured via files in the `couchbase` directory. For more details,
see the [readme file](./couchbase/README.md).

## Local Machine Development

To run the application in VSCode, under `Terminal | Run Task...` run the `docker-compose up: debug` task. This will
launch and configure Couchbase and launch the application. If this is the first run, be patient. Downloading, building, and
starting the containers may take some time on the first run.

When you see `Application started. Press Ctrl+C to shut down.` in the terminal, the application is running. Open `http://localhost:8080/customers`
in your browser or Postman to see the first 100 customers alphabetically.

To restart/rebuild the application, press Ctrl+C to stop the containers. Once stopped, you can launch again using the same process.

### Debugging

To debug the application, press F5 to start the debugger. When prompted, select the `appundertest` container to attach. It is now
possible to set breakpoints in the application found in the `AppUnderTest` folder.

### Starting from Scratch

Each time you launch the application using the `up` task, Couchbase is simply restarted with it's previous configuration and data.
This makes startup times much faster. If it's necessary to rebuild (for example, if you change configuration in the `couchbase` directory),
run the `docker-compose down: debug` task. This will clean up all related volumes so the next launch is clean.

## Functional Tests

Functional tests, which may be run as part of a CI pipeline, confirm that the application starts and the API behaves as expected in
an isolated test environment.

### Running CI Tests Locally

To run the CI tests locally in VSCode, under `Terminal | Run Task...` run the `docker-compose up: test` task.

Alternatively, the tests can be run from the command line. See detailed documentation in the [tests directory](./tests/README.md).

### Debugging Tests

To debug the tests, first be sure that you've launched the application using the `docker-compose up: debug` task.
Then launch the "Debug Tests" launch configuration. This runs the test with debugging enabled without containerization,
allowing breakpoints to be set within the tests.

## Key Points of Interest

It probably isn't possible to simply use this repository as a template for an application, instead parts of this repository
must be chosen and applied to other applications. Here are the key points of interest to examine when chosing the pieces.

1. The `couchbase` directory, which defines how Couchbase is configured on startup for both local development and CI tests.
2. The `docker-compose.yml` file, which defines how Couchbase and the application are started for both local development and CI tests.
3. `AppUnderTest/Startup.cs`, which adds an optional loop to wait for Couchbase startup. This loop is only used during local development or CI tests, not in production.
4. The `tests` folder, which defines the tests to run.
5. The `docker-compose.test.yml` file, which extends `docker-compose.yml` with the additional test container.

> :info: Additional dependencies required for the application may also be added to docker-compose.yml. This could include
> message buses, other databases, other microservices, StatsD agents, localstack to emulate AWS, and more.
