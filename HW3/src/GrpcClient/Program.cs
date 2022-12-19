using Grpc.Core;
using GrpcClient;
using System.Text;

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

app.MapGet("/greet/once/{name}", async (Greeter.GreeterClient client, string name) =>
{
    var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
    return new GreetResponse(reply.Message);
})
.WithName("GreetOnce");

app.MapGet("/greet/many/times/{name}", async (Greeter.GreeterClient client, string name) =>
{
    using var call = client.SayHelloStreamingFromServer(new HelloRequest { Name = name });
    var sb = new StringBuilder();
    await foreach (var reply in call.ResponseStream.ReadAllAsync())
    {
        sb.Append($"{reply.Message} ");
    }
    return new GreetResponse(sb.ToString());
})
.WithName("GreetManyTimes");

app.MapGet("/greet/many/names/{namesSplitByComma}", async (Greeter.GreeterClient client, string namesSplitByComma) =>
{
    var names = namesSplitByComma.Split(',');
    using var call = client.SayHelloStreamingFromClient();
    foreach (var name in names)
    {
        await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
    }
    await call.RequestStream.CompleteAsync();
    var reply = await call.ResponseAsync;
    return new GreetResponse(reply.Message);
})
.WithName("GreetManyNames");

app.MapGet("/greet/many/times/many/names/{namesSplitByComma}", async (Greeter.GreeterClient client, string namesSplitByComma) =>
{
    var names = namesSplitByComma.Split(',');
    using var call = client.SayHelloStreamingFromBoth();
    foreach (var name in names)
    {
        await call.RequestStream.WriteAsync(new HelloRequest { Name = name });
    }
    await call.RequestStream.CompleteAsync();

    var sb = new StringBuilder();
    await foreach (var reply in call.ResponseStream.ReadAllAsync())
    {
        sb.Append($"{reply.Message} ");
    }
    return new GreetResponse(sb.ToString());
})
.WithName("GreetManyTimesManyNames");

app.Run();

internal record GreetResponse(string Message)
{
}