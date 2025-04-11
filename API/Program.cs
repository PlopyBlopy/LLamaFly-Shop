using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorsService(builder.Configuration);

builder.Services.AddAuthenticationSettingsServices(builder.Configuration);
builder.Services.AddAuthorizationSettingsServices();

builder.Services.AddAppConfigurations();
builder.Services.AddDbContextServices();
builder.Services.AddDbContextConfigurations(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);
builder.Services.AddFluentValidationServices();
builder.Services.AddMappingServices();
builder.Services.AddAppServices();
builder.Services.AddAppRepositoriesServices();
builder.Services.AddAppUtilitiesServices();

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