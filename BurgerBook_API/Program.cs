using Azure.Identity;
using Azure.Storage.Blobs;
using BurgerBook.Models.Database;
using BurgerBook.Services;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.Configure<BurgerBookDatabaseSettings>(
    builder.Configuration.GetSection("BurgerBookDatabase"));

builder.Services.AddSingleton<BurgerPlaceService>();
builder.Services.AddSingleton<BurgerReviewService>();
builder.Services.AddSingleton<BlobServiceClient>(x =>
    new BlobServiceClient(
        new Uri("https://kisslacstorage.blob.core.windows.net"),
        new DefaultAzureCredential(new DefaultAzureCredentialOptions() { ExcludeEnvironmentCredential = true })));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var pack = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("elementNameConvention", pack, x => true);

app.Run();

