using FluentValidation;
using MEkroth.Matrix.Api.Database;
using MEkroth.Matrix.Api.StatusMatrices;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    //Sets so the whole API serialize after CamelCase policy, example {"firstName": ""}
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var scanAssambly = typeof(Program).Assembly;
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(scanAssambly));
builder.Services.AddValidatorsFromAssembly(scanAssambly);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddStatusMatrix();

builder.Services.AddCors(options =>
    options.AddPolicy("allowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStatusMatrix();
app.UseCors("allowAll");
app.Run();