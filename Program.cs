using apiv2.Configurations;
using apiv2.Data;
using apiv2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Configuration (Ideally near the top)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", 
        policyBuilder => policyBuilder.WithOrigins("http://localhost:5173")// Front End Port
                                   .AllowAnyMethod()
                                   .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IConfigSettings, AppConfigSettings>();
builder.Logging.AddConsole();

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


