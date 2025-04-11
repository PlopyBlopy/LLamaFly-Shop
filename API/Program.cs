using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionsHandlerExtension();
builder.Services.AddProblemDetails();

builder.WebHost.AddKestrelWebHost();
builder.Services.AddCorsService(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfigServices(builder.Configuration);
builder.Services.AddHttpClientServices(builder.Configuration);

builder.Services.AddAuthenticationSettingsServices(builder.Configuration);
builder.Services.AddAuthorizationSettingsServices();

builder.Services.AddAppServices();
builder.Services.AddAppRepositoriesServices();

builder.Services.AddDbConnectionsServices(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);

builder.Services.AddFluentValidationServices();
builder.Services.AddMappingServices();
builder.Services.AddAppUtilitiesServices();

var app = builder.Build();

app.UseExceptionHandler();

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