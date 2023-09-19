using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Betacomio_Project.LogModels;

public partial class OrderProxy
{
    public int GenericId { get; set; }

    public int? CustomerId { get; set; }

    public int? AddressId { get; set; }

    public string? Address { get; set; }

    public string? AddressDetail { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public decimal? SubTotal { get; set; }

    public List<ResultCart> ResultCarr = new List<ResultCart>();

    public decimal? TotalPrice { get; set; }

    public string? Address { get; set; }

    public string? AddressDetail { get; set; }

}
public class ResultCart
{
    public decimal? TotalPrice { get; set; }

    public short? OrderQty { get; set; }

    public int[]? ProductId { get; set; }

    public decimal? UnitPrice { get; set; }
}
