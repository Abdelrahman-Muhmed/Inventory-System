
using Inventory_System_EF.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add services to the container.
//For Swaggere UI 
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amazon", Version = "v1" });
});


builder.Services.AddControllers();

var app = builder.Build();


using var scope = app.Services.CreateScope();


#region Start Seeding Data 
//Aske cli to Make Object From Class 
var servies = scope.ServiceProvider;
var dbContext = servies.GetRequiredService<StoreContext>();

//Seeding Data
var loggerFactory = servies.GetRequiredService<ILoggerFactory>();

try
{
    //Migration Data 
    await dbContext.Database.MigrateAsync();
    //Seeding Data 
    await DataSeeding.seedAsync(dbContext);

}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();


    logger.LogError(ex, "An error happened When during migration");

} 
#endregion


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amazon V1");
    c.InjectStylesheet("/SwaggerDark.css");


});
app.Run();
