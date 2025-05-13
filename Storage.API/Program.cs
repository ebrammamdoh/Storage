using Storage.API.DIHelper;
using Storage.API.Models.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.RegisterIdentity(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.Configure<FileValidationOptions>(
    builder.Configuration.GetSection("FileValidation"));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//TODO: add exception middleware pipeline to catch, handle and log the exception

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
