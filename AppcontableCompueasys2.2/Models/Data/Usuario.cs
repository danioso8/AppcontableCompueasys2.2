using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Cedula { get; set; }

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Celular { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public bool? EsAdministrador { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }   

    public int? IdPais { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdCiudad { get; set; }

    public string? NombreEmpresa { get; set; }

    public virtual ICollection<Empresa> Empresas { get; } = new List<Empresa>();

    public virtual ICollection<Factura> Facturas { get; } = new List<Factura>();

    public virtual Ciudad? IdCiudadNavigation { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Pais? IdPaisNavigation { get; set; }

    public virtual ICollection<Nomina> Nominas { get; } = new List<Nomina>();
}
