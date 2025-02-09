var builder = DistributedApplication.CreateBuilder(args);

// Add the API
var api = builder.AddProject<Projects.DriverStandingsApi>("driver-standings-api")
    .WithExternalHttpEndpoints();

var webApp = builder.AddNpmApp("driver-standings-webapp", "../DriverStandingsWebApp")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();


builder.Build().Run();
