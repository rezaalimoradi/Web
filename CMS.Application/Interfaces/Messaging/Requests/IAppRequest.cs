namespace CMS.Application.Interfaces.Messaging.Requests
{
    public interface IAppRequest<out TResponse> : MediatR.IRequest<TResponse>
    {
    }
}
