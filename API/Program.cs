using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        //policy.WithOrigins("http://localhost:4173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
    });
});

//TODO: ƒобавить кастомную обработку exeptions (отдельный класс дл€ конкретной ошибки наследуемый от IExceptionHandler)
//builder.Services.AddExceptionsHandlerExtension();
//builder.Services.AddProblemDetails();

//TODO: »нтеграци€ логировани€ (Middleware и если нужно другие варианты)
//builder.Host.UseSerilogWithConfiguration();

builder.Services.AddAuthenticationSettingsServices(builder.Configuration);
builder.Services.AddAuthorizationSettingsServices();

builder.Services.AddDbConnectionsServices(builder.Configuration);

builder.Services.AddFluentValidationServices();
builder.Services.AddMappingServices();
builder.Services.AddAppUtilitiesServices();
builder.Services.AddAppRepositoriesServices();
builder.Services.AddAppServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
//app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseMiddleware<CorrelationIdMiddleware>();

app.Run();