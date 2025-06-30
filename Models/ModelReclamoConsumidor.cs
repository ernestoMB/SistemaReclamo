namespace SistemaReclamo.Models
{
    public class ModelReclamoConsumidor
    {


        public CConsumidor Consumidor { get; set; } = new CConsumidor();
        public TReclamo Reclamo { get; set; } = new TReclamo();




    }
}
