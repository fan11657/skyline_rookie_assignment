using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulApiDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ItemContext>(options => options.UseInMemoryDatabase("InMemDbName"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/item/all", async (ItemContext dbContext) =>
    await dbContext.Items.ToArrayAsync())
.WithName("GetItems");

app.MapGet("/item/{id}", async (long id, ItemContext dbContext) => {
    var item = await dbContext.FindAsync<Item>(id);
    return item != null
        ? Results.Ok(item)
        : Results.NotFound();
})
.WithName("GetItem");

app.MapPost("/item", async (PostRequest request, ItemContext dbContext) =>
{
    var item = new Item { Content = request.Content };
    dbContext.Items.Add(item);
    await dbContext.SaveChangesAsync();
    return Results.Ok(new { item.Id });
})
.WithName($"AddItem");

app.MapPut("/item/{id}", async (long id, PutRequest request, ItemContext dbContext) =>
{
    dbContext.Items.Update(new Item { Id = id, Content = request.Content });
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
})
.WithName($"UpdateItem");

app.MapDelete("/item/{id}", async (long id, ItemContext dbContext) =>
{
    var item = new Item { Id = id };
    dbContext.Items.Attach(item);
    dbContext.Items.Remove(item);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
})
.WithName($"DeleteItem");

app.Run();


internal class PostRequest
{
    public string Content { get; set; }
}
internal class PutRequest
{
    public string Content { get; set; }
}