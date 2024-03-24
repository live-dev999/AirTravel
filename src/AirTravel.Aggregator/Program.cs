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
using AirTravel.Aggregator;
using AirTravel.Aggregator.Core;
using AirTravel.Aggregator.Services;
using AirTravel.Aggregator.Services.Models;
using AirTravel.Aggregator.Services.Sources.FirstSource;
using AirTravel.Aggregator.Services.Sources.SecondSource;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IFlightInfo, FlightInfo>();
builder.Services.AddScoped<IFakeFirstFlightSource, FakeFirstFlightSource>();
builder.Services.AddScoped<IFakeSecondFlightSource, FakeSecondFlightSource>();
builder.Services.AddScoped<IFlightDataAdapter, FakeFirstFlightSourceAdapter>();
builder.Services.AddScoped<IFlightDataAdapter, FakeSecondFlightSourceAdapter>();
builder.Services.AddScoped<IFlightAggregator, FlightAggregator>();
builder.Services.AddScoped<FlightDataAdapterStrategy>(provider =>
    (type) =>
    {
        return type switch
        {
            FlightDataAdapterSource.FakeFirstFlightSourceAdapter
                => provider.GetRequiredService<FakeFirstFlightSourceAdapter>(),
            FlightDataAdapterSource.FakeSecondFlightSourceAdapter
                => provider.GetRequiredService<FakeSecondFlightSourceAdapter>(),
            _ => throw new NotImplementedException(),
        };
    }
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapDefaultControllerRoute();
app.UseSerilogRequestLogging();
app.Run();
