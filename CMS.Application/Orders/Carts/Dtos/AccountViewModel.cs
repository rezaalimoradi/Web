using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Orders.Carts.Dtos
{
    public class AccountViewModel
    {
        // اطلاعات کاربر
        public string UserId { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? NewPassword { get; set; }

        // آدرس‌ها
        public AddressViewModel BillingAddress { get; set; } = new();
        public AddressViewModel ShippingAddress { get; set; } = new();

        // سفارشات اخیر
        public List<OrderSummaryViewModel> Orders { get; set; } = new();
    }

    // -----------------------------
    // مدل نمایش سفارشات کاربر
    // -----------------------------
    public class OrderSummaryViewModel
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
    }

    // -----------------------------
    // مدل آدرس کاربر
    // -----------------------------
    public class AddressViewModel
    {
        public string? FullName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
    }
}
