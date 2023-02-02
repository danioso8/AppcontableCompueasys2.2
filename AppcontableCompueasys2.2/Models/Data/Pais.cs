using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Pais
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();

    public virtual ICollection<Empresa> Empresas { get; } = new List<Empresa>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
