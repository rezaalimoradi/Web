namespace CMS.Application.Interfaces.Messaging.Requests
{
    public interface IAppRequestHandler<TRequest, TResponse> : MediatR.IRequestHandler<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
    {
    }
}
