using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Empresa
{
    public int Id { get; set; }

    public string? NombreEmpresa { get; set; }

    public string? DireecionEm { get; set; }

    public string? NitORut { get; set; }

    public string? TelefonoOCelular { get; set; }

    public string? Email { get; set; }

    public int? IdPais { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdCiudad { get; set; }

    public int? IdPropietarioEmpresa { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();

    public virtual ICollection<DetalleCompra> DetalleCompras { get; } = new List<DetalleCompra>();

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();

    public virtual Ciudad? IdCiudadNavigation { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Pais? IdPaisNavigation { get; set; }

    public virtual Usuario? IdPropietarioEmpresaNavigation { get; set; }

    public virtual ICollection<Nomina> Nominas { get; } = new List<Nomina>();

    public HashSet<Categoria> Categorias { get; set; } = new HashSet<Categoria>();

    public HashSet<Marca> Marcas { get; set; } = new HashSet<Marca>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
