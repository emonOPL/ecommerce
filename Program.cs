var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Category> categories = new List<Category>(); 

app.MapGet("/", () => "Hello Emon!");

app.MapPost("/api/categories", (string name, string description) =>
{
    Category category = new Category();
    category.Id = Guid.NewGuid();
    category.Name = name;
    category.Description = description;
    category.CreatedAt = DateTime.Now;
    categories.Add(category);

    return Results.Created($"/api/categories/{category.Id}", category);
});

app.MapGet("/api/categories", () => categories);

app.MapPut("/api/categories/{id}", (Guid id, string? name, string? description) =>
{
    Category? category = categories.FirstOrDefault(c => c.Id == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    category.Name = name ?? category.Name;
    category.Description = description ?? category.Description;
    return Results.Ok(category);
});

app.MapDelete("/api/categories/{id}", (Guid id) =>
{
    Category? category = categories.FirstOrDefault(c => c.Id == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    categories.Remove(category);
    return Results.NoContent();
});

app.Run();

public record Category
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
