

using CashFlow.API.Filters;
using CashFlow.API.Middleware;
using CashFlow.Application;
using CashFlow.Infraestructure.DataAccess;
using CashFlow.Infraestructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExpenseExceptionFilter)));
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await UpdateDatabase();

app.Run();

async Task UpdateDatabase()
{
    var service = app.Services.CreateAsyncScope().ServiceProvider;

    await service.UpdateDatabase();
}