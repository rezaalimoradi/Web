using CMS.Application.Interfaces.Messaging;
using MediatR;

namespace CMS.Infrastructure.Messaging
{
    public class MediatRPipelineAdapter<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IAppPipelineBehavior<TRequest, TResponse>> _behaviors;

        public MediatRPipelineAdapter(IEnumerable<IAppPipelineBehavior<TRequest, TResponse>> behaviors)
        {
            _behaviors = behaviors;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Func<Task<TResponse>> pipeline = () => next();

            foreach (var behavior in _behaviors.Reverse())
            {
                var current = pipeline;
                pipeline = () => behavior.Handle(request, current, cancellationToken);
            }

            return await pipeline();
        }
    }
}
