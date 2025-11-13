using CMS.Application.CompareItems.Dtos;
using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;
using CMS.Application.Wishlists.Dtos;

namespace CMS.Application.CompareItems.Queries
{
    public class GetCompareByCustomerQuery : IAppRequest<ResultModel<CompareListDto>>
    {
        public long CustomerId { get; set; }

        public GetCompareByCustomerQuery(long customerId)
        {
            CustomerId = customerId;
        }
    }
}
