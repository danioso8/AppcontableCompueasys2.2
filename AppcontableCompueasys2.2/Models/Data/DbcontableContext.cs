using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppcontableCompueasys2._2.Models.Data;

public partial class DbcontableContext : DbContext
{
    public DbcontableContext()
    {
    }

    public DbcontableContext(DbContextOptions<DbcontableContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Nomina> Nominas { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<TipoDePago> TipoDePagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__CARRITO__8B4A618C68BFD5B0");

            entity.ToTable("CARRITO");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_CARRITO_Clientes");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__CARRITO__IdProdu__7D439ABD");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__A3C02A1095AA2B61");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.ToTable("CIUDAD");

            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("CLIENTES");
            entity.Property(e => e.Id).HasColumnName("id");
            
            
            entity.Property(e => e.Cedula)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.Direccion)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_Clientes_CIUDAD");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_Clientes_DEPARTAMENTO");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Clientes_Empresa");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_Clientes_PAIS");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.ToTable("DEPARTAMENTO");

            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK__DETALLE___E046CCBBC0A53C71");

            entity.ToTable("DETALLE_COMPRA");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_DETALLE_COMPRA_Clientes");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_DETALLE_COMPRA_Empresa");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK_DETALLE_COMPRA_FACTURAS");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DETALLE_C__IdPro__02FC7413");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("EMPLEADOS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsFixedLength();

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_EMPLEADOS_CIUDAD");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_EMPLEADOS_DEPARTAMENTO");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_EMPLEADOS_Empresa");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_EMPLEADOS_PAIS");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Empresa");

            entity.ToTable("EMPRESA");

            entity.Property(e => e.DireecionEm)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NitORut)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Nit o Rut");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.TelefonoOCelular)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Telefono o Celular");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_Empresa_CIUDAD");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_Empresa_DEPARTAMENTO");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_Empresa_PAIS");

            entity.HasOne(d => d.IdPropietarioEmpresaNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdPropietarioEmpresa)
                .HasConstraintName("FK_Empresa_USUARIO");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__FACTURAS__50E7BAF12824E1E1");

            entity.ToTable("FACTURAS");

            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_FACTURAS_Clientes");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_FACTURAS_PRODUCTO");

            entity.HasOne(d => d.IdTipoDePagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdTipoDePago)
                .HasConstraintName("FK_FACTURAS_TIPO DE PAGO");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_FACTURAS_USUARIO");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__MARCA__4076A8872F9ECA88");

            entity.ToTable("MARCA");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Nomina>(entity =>
        {
            entity.ToTable("NOMINA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Auxilio).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ConseptoDescuentos)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.ConseptoNomina)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Descuentos).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.TotalApagar).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ValorEps).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ValorPension).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Nominas)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_NOMINA_EMPLEADOS");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Nominas)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_NOMINA_Empresa");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Nominas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_NOMINA_USUARIO");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.ToTable("PAIS");

            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__09889210F34A9EE0");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
           
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEmpresa).HasColumnName("idEmpresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Precio)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RutaImagen)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__PRODUCTO__IdCate__160F4887");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_PRODUCTO_Empresa");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__PRODUCTO__IdMarc__151B244E");

          
        });

        modelBuilder.Entity<TipoDePago>(entity =>
        {
            entity.ToTable("TIPO DE PAGO");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF97D8AA0A03");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Activo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_USUARIO_CIUDAD");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK_USUARIO_DEPARTAMENTO");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_USUARIO_PAIS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
