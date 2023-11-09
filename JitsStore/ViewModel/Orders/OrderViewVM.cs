using System;

namespace JitsStore.ViewModel;

public class OrderViewVM
{
    public string OrderId { get; set; } = null!;

    public string Productname { get; set; }

    public Guid ProductId { get; set; }
    public DateTime? OrderDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public decimal? ShippingFee { get; set; }

    public string ShipAddress { get; set; }

    public string Note { get; set; }

    public string ShipperName { get; set; }

    public string ShipperId { get; set; }
    public string CustomerName { get; set; }
    public Guid CustomerId { get; set; }

    public string EmployeeName { get; set; }
    public Guid EmployeeId { get; set; }
}
