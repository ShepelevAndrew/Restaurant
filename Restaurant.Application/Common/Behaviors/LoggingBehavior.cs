using System.Text;
using ErrorOr;
using MediatR;
using Serilog;

namespace Restaurant.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.Information(
            "Starting request {@RequestName}, at {@DateTimeUtc} time",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();
        if (result.IsError)
        {
            var errorsDescription = new StringBuilder();
            if (result.Errors is not null)
            {
                foreach (var error in result.Errors)
                {
                    errorsDescription.Append(error.Description + " ");
                }
            }

            _logger.Error(
                "Request failure: {@RequestName}\n\tErrors: {@Error}\n\tAt {@DateTimeUtc} time.",
                typeof(TRequest).Name,
                errorsDescription.ToString(),
                DateTime.UtcNow);
        }
        else
        {
            _logger.Information(
                "Completed request {@RequestName}, at {@DateTimeUtc} time",
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }


        return result;
    }
}