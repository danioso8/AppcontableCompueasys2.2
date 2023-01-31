using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Nomina
{
    public int Id { get; set; }

    public int? IdEmpleado { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Valor { get; set; }

    public decimal? TotalApagar { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEmpresa { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal? Descuentos { get; set; }

    public decimal? ValorEps { get; set; }

    public decimal? ValorPension { get; set; }

    public string? ConseptoNomina { get; set; }

    public string? ConseptoDescuentos { get; set; }

    public decimal? Auxilio { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
