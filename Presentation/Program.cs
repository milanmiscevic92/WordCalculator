using Application;
using Application.Common.Interfaces.Services;
using Application.Texts;
using Application.Texts.GetTextById;
using Application.Texts.GetTextFromFile;
using Domain.Shared;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal sealed class Program
{
    // set up host and configure services
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();

        await Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                    .AddApplicationServices()
                    .AddInfrastructureServices(hostContext.Configuration)
                    .AddHostedService<ConsoleHostedService>();
            })
            .RunConsoleAsync();
    }
}

// simple hosted service
internal sealed class ConsoleHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly ISender _mediator;
    private readonly IWordCalculatorService _wordCalculatorService;

    public ConsoleHostedService(IHostApplicationLifetime appLifetime, ISender mediator, IWordCalculatorService wordCalculatorService)
    {
        _appLifetime = appLifetime;
        _mediator = mediator;
        _wordCalculatorService = wordCalculatorService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    int textId = 1;
                    string textPath = @"C:\Users\milan\Desktop\Clean Architecture - Final Version\WordCalculator\RandomText.txt";

                    await PrintNumberOfWordsFromDatabaseText(textId);
                    await PrintNumberOfWordsFromFileText(textPath);

                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task PrintNumberOfWordsFromDatabaseText(int textId)
    {
        var query = new GetTextByIdQuery(textId);
        Result<TextResponse> textFromDatabaseResponse = await _mediator.Send(query);

        if (textFromDatabaseResponse.IsSuccess)
        {
            var textFromDatabaseNumberOfWords = await _wordCalculatorService.CalculateNumberOfWords(textFromDatabaseResponse.Value.Content);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Text from database has {textFromDatabaseNumberOfWords.Value} words.");
        }
        else
        {
            Console.WriteLine($"Error retrieving the text from database: {textFromDatabaseResponse.Error.Message}");
        }
    }

    private async Task PrintNumberOfWordsFromFileText(string path)
    {
        var query = new GetTextFromFileQuery(path);
        Result<TextResponse> textFromFileResponse = await _mediator.Send(query);

        if (textFromFileResponse.IsSuccess)
        {
            var textFromFileNumberOfWords = await _wordCalculatorService.CalculateNumberOfWords(textFromFileResponse.Value.Content);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Text from file has {textFromFileNumberOfWords.Value} words.");
        }
        else
        {
            Console.WriteLine($"Error retrieving the text from file: {textFromFileResponse.Error.Message}");
        }
    }
}