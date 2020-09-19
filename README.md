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
