﻿using System;
using System.Collections.Generic;

namespace WebOrder.Models;

public partial class OrderProduct
{
    public int OrderProductId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int OrderId { get; set; }
    
    public DateTime? ExpirationDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
