using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
