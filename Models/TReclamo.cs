using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SistemaReclamo.Models;

public partial class TReclamo
{
    public int IdReclamo { get; set; }
    [DisplayName("Nombre del Proveedor")]
    public string NombreProveedor { get; set; } = null!;
    [DisplayName("Dirección del Proveedor")]
    public string DireccionProveedor { get; set; } = null!;
    [DisplayName("Detalle del reclamo")]
    public string DetalleReclamo { get; set; } = null!;
    [DisplayName("Telefono del Proveedor")]
    public string? TelefonoProveedor { get; set; }
    [DisplayName("Monto del reclamo")]
    public decimal? MontoReclamo { get; set; }
    [DisplayName("Fecha de Ingreso")]
    public DateTime FechaIngreso { get; set; }
    [DisplayName("Fecha de Revisión")]
    public DateTime? FechaRevision { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdConsumidor { get; set; }

    public int? IdEstado { get; set; }

    public bool Activo { get; set; }

    public virtual CConsumidor? IdConsumidorNavigation { get; set; }

    public virtual CEmpleado? IdEmpleadoNavigation { get; set; }
    
    public virtual CEstado? IdEstadoNavigation { get; set; }

    public virtual ICollection<TAsesorium> TAsesoria { get; set; } = new List<TAsesorium>();

    public virtual ICollection<TAviso> TAvisos { get; set; } = new List<TAviso>();
}
