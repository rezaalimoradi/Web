using MediatR;

namespace CMS.Application.Interfaces.Messaging
{
    public interface IAppPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(
            TRequest request,
            Func<Task<TResponse>> next,
            CancellationToken cancellationToken = default);
    }
}
