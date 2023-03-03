using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int? IdFactura { get; set; }

    public int? IdProducto { get; set; }

    public string? Nombre { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
