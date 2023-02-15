using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Empleado
{
    public int Id { get; set; }

    public int? Cedula { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public int? IdPais { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdCiudad { get; set; }

    public string? Email { get; set; }

    public string? Activo { get; set; }

    public int? IdEmpresa { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Ciudad? IdCiudadNavigation { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual Pais? IdPaisNavigation { get; set; }

    public virtual ICollection<Nomina> Nominas { get; } = new List<Nomina>();
}
