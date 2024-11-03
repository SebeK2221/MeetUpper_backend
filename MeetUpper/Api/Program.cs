using Api.Exceptions;
using Application;
using Domain.Entities;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services
    .AddCors()
    .AddAplication()
    .AddInfrastructure(builder.Configuration)
    .AddExceptionHandler<ExceptionHandler>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseExceptionHandler(_ => {});
app.UseCors();
app.MapControllers();
app.Run();
