/*
 *   Copyright (c) 2024 Dzianis Prokharchyk

 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.

 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.

 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Reflection;
using AirTravel.API.Extensions;
using AirTravel.API.Services;
using AirTravel.Config;
using AirTravel.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
// builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		  .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
          .AddEnvironmentVariables(); // optional extra provider

builder.Services.AddLogging(loggingBuilder =>
{
    // show in console
    loggingBuilder
        .AddConsole()
        // show sql commands
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
    // show in debug console
    loggingBuilder.AddDebug();
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.ConfigurePOCO<UrlsConfig>(builder.Configuration.GetSection("Urls"));
builder.Services.AddScoped<IExternalFlightApi,ExternalFlightApi>();
builder.Services.AddScoped<IFlightAggregator,FlightAggregator>();
builder.Services.AddHttpClient<IExternalFlightApi, ExternalFlightApi>(client =>
{
   client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Urls:AggregatorUrl"));
});

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapDefaultControllerRoute();
app.UseSerilogRequestLogging();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var logger = services.GetRequiredService<ILogger<Program>>();
// string Namespace = typeof(Program).Namespace;
// string AppName =
//             Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
try
{
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
    logger.LogInformation("database migrated");
    // await Seed.SeedData(context);
    // logger.LogInformation($"============== {AppName} - state is started =====================");
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}
Log.Information($"============== AirTravel.API is started =====================");

app.Run();
