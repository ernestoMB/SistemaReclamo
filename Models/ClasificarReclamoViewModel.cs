namespace SistemaReclamo.Models
{
    public class ClasificarReclamoViewModel
    {
        public TReclamo Reclamo { get; set; } = new TReclamo();

        public List<CEstado> Estados { get; set; } = new List<CEstado>();


    }
}
