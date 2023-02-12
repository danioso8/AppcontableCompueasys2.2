using System;
using System.Collections.Generic;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    //public int? IdEmpresa { get; set; }

    //public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
