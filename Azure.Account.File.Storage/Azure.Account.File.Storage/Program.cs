using Azure.Account.File.Storage.Repository;
using Azure.Account.File.Storage.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("Upload", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:6200")
            .WithOrigins("https://localhost:6200");
    });
});

// Add services to the container.
builder.Services.AddTransient<IAzureStorage, AzureStorage>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Upload");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
