var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();

var apiService = builder.AddProject<Projects.Customer_Web>("customer")
    .WithReference(cache)
    .WithReference(postgres);

builder.Build().Run();
