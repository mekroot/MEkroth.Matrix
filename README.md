# MEkroth.Matrix
This is a POC of a overview of statuses.

## Prerequirement
- .NET8 install.
- Visual Studio 2022
- Node 20.11.0
  
## How to setup
First of all clone this repository, then follow up the steps below.

### Server
1. Open up ```MEkroth.Matrix.sln``` in Visual Studio.
2. Press to run IISExpress.
3. Swagger will show up the current endpoints on: [https://localhost:44396/swagger/index.html](https://localhost:44396/swagger/index.html)

### Frontend
1. Open up a terminal.
2. Locate to ```cd path/you/clone/to/src/status-matrix```.
3. Run ```npm install```, this is to install all needed packages.
4. After that the install success, run ```npm start```

## Backend frameworks overview
Following framework has been used. It's Micrsofts own and some third parties.
- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- ASP.NET Core WebAPI
- [MediatR v.12.2.0](https://github.com/jbogard/MediatR/wiki), for send and handle command and queries (in CQRS style).
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/), help to validate incoming commands/queries. 
- Enitiy Framework Core
- SQLite

## Test frameworks overview
Following framework has been used in case of unit testing.
- [xUnit](https://xunit.net), core test frameworks.
- [FluentAssertion](https://fluentassertions.com/), used to make it easier to read the assertion.
- [NSubstitute](https://nsubstitute.github.io/), ease to use mock library. Help to mock services when setting up the unit tests.

## Frontend frameworks overview
Following framework has been used in case of unit testing.
- [Aurelia](https://docs.aurelia.io/), main frontend framework the client is based on.
- [TailwindCss](https://tailwindcss.com), CSS framework to make it easier to make nice UI.

## Known Issues
Here is some of known issues in the application.
1. While Save existing Matrix the list will be empty. Reload the page and the it will fix it.
2. Validation message will nog show up for errors. 
