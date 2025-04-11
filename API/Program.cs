using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionsHandlerExtension();

builder.WebHost.AddKestrelWebHost();
builder.Services.AddCorsService(builder.Configuration);

builder.Services.AddOptionsServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationSettingsServices(builder.Configuration);
builder.Services.AddAuthorizationSettingsServices();

builder.Services.AddAppServices();
builder.Services.AddRepositoryServices();
builder.Services.AddAppUtilitiesServices();
builder.Services.AddFluentValidationServices();

builder.Services.AddMappingServices();

builder.Services.AddDbConnectionsServices(builder.Configuration);
builder.Services.AddKafkaProducersServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();