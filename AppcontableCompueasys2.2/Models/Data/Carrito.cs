using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int? IdProducto { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
