using System;
using System.Collections.Generic;

namespace MusicMarket.Models;

public partial class ShoppingCart
{
    public int Id { get; set; }

    public int TrackId { get; set; }

    public int UserId { get; set; }

    public DateTimeOffset Date { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
