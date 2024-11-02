var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.JwtProjeto_ApiService>("apiservice");

builder.AddProject<Projects.JwtProjeto_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
