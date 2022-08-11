using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using GhostOfTrebec.Core.AddQuestion;

namespace GhostOfTrebec.ConsoleApp
{
    public class AddNewQuestions : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<AddNewQuestions> _logger;
        private readonly IMediator _mediator;

        public AddNewQuestions(IServiceScopeFactory serviceScopeFactory, ILogger<AddNewQuestions> logger, IMediator mediator)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var sessionActive = true;
            Console.WriteLine("Welcome to the Ghost of Trebec Trivia Builder.");

            do
            {
                string? question;
                do
                {
                    Console.WriteLine($"{Environment.NewLine}Enter the question first: ");
                    question = Console.ReadLine();
                } while (question == null);

                string? correctAnswer;
                do
                {
                    Console.WriteLine($"{Environment.NewLine}Now enter the correct answer: ");
                    correctAnswer = Console.ReadLine();
                } while (correctAnswer == null);

                var incorrectAnswers = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    var ordinal = i switch
                    {
                        0 => "first",
                        1 => "second",
                        _ => "third"
                    };

                    string? answer;
                    do
                    {
                        Console.WriteLine($"{Environment.NewLine}Next, enter the {ordinal} of 3 incorrect answers.");
                        answer = Console.ReadLine();
                    } while (answer == null);

                    incorrectAnswers.Append(answer);
                }

                var command = new AddQuestionCommand(question, correctAnswer, incorrectAnswers);
                await _mediator.Send(command);
                _logger.LogInformation($"Question added: {question}");

                Console.WriteLine($"{Environment.NewLine}Shall we do another? (Y or N){Environment.NewLine}");
                var yesOrNoChar = Console.ReadKey().KeyChar;
                sessionActive = char.ToLower(yesOrNoChar) == 'y';

            } while (sessionActive);


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
