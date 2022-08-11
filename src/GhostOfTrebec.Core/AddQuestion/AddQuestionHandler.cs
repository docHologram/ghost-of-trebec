using GhostOfTrebec.Core.InnerCore;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GhostOfTrebec.Core.AddQuestion
{
    public record AddQuestionCommand(string Question, string CorrectAnswer, List<string> IncorrectAnswers) : IRequest<Unit> { }

    public class AddQuestionHandler : IRequestHandler<AddQuestionCommand, Unit>
    {
        private readonly IRepository<Problem> _repository;
        private readonly ILogger<AddQuestionHandler> _logger;

        public AddQuestionHandler(IRepository<Problem> repository, ILogger<AddQuestionHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddQuestionCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Adding question: {command.Question}");

            var completeIncorrectAnswers = command.IncorrectAnswers
                .Select(a =>
                    new Answer
                    {
                        Text = a,
                        IsCorrect = false
                    }
                );
            var completeAnswer = new Answer
            {
                Text = command.CorrectAnswer,
                IsCorrect = true
            };
            var completeAnswers = new List<Answer>(completeIncorrectAnswers.Append(completeAnswer));

            var problem = new Problem(Guid.NewGuid().ToString(), command.Question, completeAnswers);

            await _repository.SaveAsync(problem);
            return Unit.Value;
        }
    }
}
