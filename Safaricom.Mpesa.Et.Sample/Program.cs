using Microsoft.AspNetCore.Mvc;
using Safaricom.Mpesa.Et;
using Safaricom.Mpesa.Et.Requests;
using Safaricom.Mpesa.Et.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var config = new MpesaConfig
{
    ConsumerKey = "7FX34gPG1KBLuNRGxlqRMaEcZEa7sx6itGRl9Ttaxs8fEScP",
    ConsumerSecret = "7FX34gPG1KBLuNRGxlqRMaEcZEa7sx6itGRl9Ttaxs8fEScP",
};
builder.Services.AddMpesa(config);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/mpesa", async ([FromServices] IMpesaClient mpesa) =>
{
    //var mpesa = new MpesaClient(new MpesaConfig { ConsumerKey= "key", ConsumerSecret= "secret" });
    var balance = new AccountBalance
    {
        OriginatorConversationID = Guid.NewGuid(),
        Initiator = "testapi",
        SecurityCredential = "SecurityCredential",
        PartyA = "600000",
        IdentifierType = IdentifierType.Organization,
        Remarks = "Remarks",
        QueueTimeOutURL = new Uri("https://example.com/timeout"),
        ResultURL = new Uri("https://example.com/result"),
    };
    var r = await mpesa.AccountBalanceAsync(balance);
    return "Hello World!";
}).WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
