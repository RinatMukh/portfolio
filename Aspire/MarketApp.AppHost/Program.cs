var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgre");

var apiService = builder.AddProject<Projects.Customer_Web>("customer")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(postgres)
    .WaitFor(cache);

builder.AddNpmApp("vue", "../../Services/Customer/Customer.Web/client")
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
