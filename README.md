# Driver Standings Web App

## Overview

This is an Aspire project containing an Angular front end, a .NET back end and a suite of unit tests for the back end.

## Usage instructions

For local development this project should be run through Aspire.

Aspire will start an instance of the web API and the front end

The API is accessible at ```https://localhost:7197/DriverStandings/GetDriverStandings/{year}```

The frontend is accessible at ```http://localhost:4200/```

1. Before running the project please add a .env file with the api key to the same folder as DriverStandingsApi program.cs, for example
```
RB_API_KEY=<your api key>
```
1. Run the Aspire AppHost project
1. Navigate to ```http://localhost:4200/```

## Limitations & Future Work

Future development would include:

- [] Front end requires Red Bull's fonts
- [] Front end should use HTTPS, this should help fix the certificate validation issues
- [] Front end requires a test suite
- [] Clarification is required on whether to display the driver country
- [] Front end should include responsive design to accomodate different screen sizes
- [] Front end should only display years for which there's data or an informative message if there's no data
- [] Strings should be localised
- [] Country codes should be replaced with country names
- [] Back end authentication should be configured to allow requests from front end