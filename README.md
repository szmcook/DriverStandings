# Driver Standings Web App

## Overview

This is an Aspire project containing an Angular front end, a .NET back end and a suite of unit tests for the back end.

## Usage instructions

For local development this project should be run through Aspire.

Aspire will start an instance of the web API and the front end

The API is accessible at ```https://localhost:7197/DriverStandings/GetDriverStandings/{year}```

The frontend is accessible at ```http://localhost:4200/```

Before running the project please add a .env file with the api key to the same folder as DriverStandingsApi program.cs, for example
```
RB_API_KEY=<your api key>
```

## Limitations Future Work

As I have no experience with Angular, this project is limited and requires the following work:

- [] Front end filters should work
- [] Front end should only display years for which there's data & an informative message if there's no data
- [] Front end should be styled according to Red Bull guidelines
- [] clarification is required on whether to display the driver country
- [] Front end should use HTTPS
- [] Front end requires a test suite
- [] strings should be localised
