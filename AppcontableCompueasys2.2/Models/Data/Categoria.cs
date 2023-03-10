using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual ICollection<Empresa> Empresa { get; } = new List<Empresa>();

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();


} 