using System;
using System.Collections.Generic;

namespace MusicMarket.Models;

public partial class Track
{
    public int Id { get; set; }

    public string MusicName { get; set; } = null!;

    public string? GroupName { get; set; }

    public string AuthorName { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Genre { get; set; }

    public DateTimeOffset DateRelize { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? Price { get; set; }

    public int AuthorId { get; set; }

    public virtual Сompany Author { get; set; } = null!;

    public virtual ShoppingCart? ShoppingCart { get; set; }
}
