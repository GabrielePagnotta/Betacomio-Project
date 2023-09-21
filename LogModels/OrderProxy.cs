using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Betacomio_Project.LogModels;

public class OrderProxy
{
    public int GenericId { get; set; }

    public UniqueData userUniqueData { get; set; }

    public List<OrderDetailData> detailData { get; set; }

}
public class OrderDetailData
{
    //dati per ogni singolo prodotto
    [Key]
    public int ProductId { get; set; }
    public short OrderQty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

}

public class UniqueData
{
    [Key]
    public int CustomerId { get; set; }  //user id token

    public int? AddressId { get; set; }

    public string? Address { get; set; }

    public string? AddressDetail { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public decimal SubTotal { get; set; }  //totale carrello

}
