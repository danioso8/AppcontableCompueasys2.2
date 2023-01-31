using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdUsuario { get; set; }

    public int? CantidadProducto { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaCompra { get; set; }

    public int? IdCliente { get; set; }

    public int? IdProducto { get; set; }

    public int? Iva { get; set; }

    public int? Descuento { get; set; }

    public string? Observaciones { get; set; }

    public string? EstadoFactura { get; set; }

    public int? IdTipoDePago { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; } = new List<DetalleCompra>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual TipoDePago? IdTipoDePagoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
