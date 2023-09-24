
using Example.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// var app = builder.Build();

// app.MapGet("/", () => "Welcome!");

// app.Run();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
