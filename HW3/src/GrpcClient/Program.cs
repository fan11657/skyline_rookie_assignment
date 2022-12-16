using GrpcClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpcClient<Greeter.GreeterClient>(o => o.Address = new Uri("https://localhost:7206"));

var app = builder.Build();  

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/greet/{name}", async (Greeter.GreeterClient client, string name) =>
{
    var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
    return new GreetResponse(reply.Message);
})
.WithName("Greet");

app.Run();

internal record GreetResponse(string Message)
{
}