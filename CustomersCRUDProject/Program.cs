var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
ConfigureServices(services);




var app = builder.Build();
var webHostEnvironment = app.Environment;

Configure(app, webHostEnvironment );
void ConfigureServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddSwaggerGen();
}
void Configure(WebApplication webApplication, IWebHostEnvironment webHostEnvironment)
{
    // Configure the HTTP request pipeline.
    if (webHostEnvironment.IsDevelopment())
    {
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}