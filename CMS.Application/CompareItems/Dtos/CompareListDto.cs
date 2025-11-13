using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.CompareItems.Dtos
{
    public class CompareListDto
    {
        public long CustomerId { get; set; }
        public List<CompareItemsDto> Items { get; set; } = new();
    }
}
