using MusicMarket.ContextModels;
using System;
using System.Collections.Generic;

namespace MusicMarket.Models;

public partial class Order
{
    public int Id { get; set; }

    public int Trackid { get; set; }

    public int Userid { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
