using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Celular { get; set; }

    public string? Cedula { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdPais { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdCiudad { get; set; }

    public virtual ICollection<Carrito> Carritos { get; } = new List<Carrito>();

    public virtual ICollection<DetalleCompra> DetalleCompras { get; } = new List<DetalleCompra>();

    public virtual ICollection<Factura> Facturas { get; } = new List<Factura>();

    public virtual Ciudad? IdCiudadNavigation { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Pai? IdPaisNavigation { get; set; }
}
