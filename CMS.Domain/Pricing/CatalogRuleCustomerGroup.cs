using CMS.Domain.Common;
using CMS.Domain.Users.Entities;

namespace CMS.Domain.Pricing
{
    //رابطهٔ CatalogRule ↔ CustomerGroup.
    public class CatalogRuleCustomerGroup : BaseEntity
    {
        protected CatalogRuleCustomerGroup() { }

        public CatalogRuleCustomerGroup(long catalogRuleId, long customerGroupId)
        {
            CatalogRuleId = catalogRuleId;
            CustomerGroupId = customerGroupId;
        }

        public long CatalogRuleId { get; private set; }
        public CatalogRule CatalogRule { get; private set; }

        public long CustomerGroupId { get; private set; }
        public AppUser CustomerGroup { get; private set; }
    }
}
