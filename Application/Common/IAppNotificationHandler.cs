using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IAppNotificationHandler<in TNotification> : MediatR.INotificationHandler<TNotification> where TNotification : IAppNotification
    {
    }
}
