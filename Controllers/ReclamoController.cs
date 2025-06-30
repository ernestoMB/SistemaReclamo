using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReclamo.Models;
using SistemaReclamo.Filters;

namespace SistemaReclamo.Controllers
{
    
    public class ReclamoController : Controller
    {
        private readonly DbtempMuñozContext _Dbcontext;
        public ReclamoController(DbtempMuñozContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }
        
        public IActionResult CrearReclamo()
        {
            
            
            return View();

        }
        //Creamos el reclamo
        [HttpPost]
        public IActionResult Crear(ModelReclamoConsumidor multimodel)
        {

            if(ModelState.IsValid)
            {

                //Primero grardamos los datos del consumidor
                var consumidor = new CConsumidor
                {
                    NombreConsumidor = multimodel.Consumidor.NombreConsumidor,
                    ApeliidoConsumidor = multimodel.Consumidor.ApeliidoConsumidor,
                    Direccion = multimodel.Consumidor.Direccion,
                    CorreoElectronico = multimodel.Consumidor.CorreoElectronico,
                    DuiConsumidor = multimodel.Consumidor.DuiConsumidor,
                    Activo = multimodel.Consumidor.Activo
                };
                _Dbcontext.CConsumidors.Add(consumidor);
                _Dbcontext.SaveChanges();

                // Obtener el IdEstado correspondiente a "Pendiente de clasificar"
                // Cuando el cliente ingrese el reclamo este por defecto estará en estado
                // "Pendiente de clasificar" (un estado el la DB en la tabla CEstado)
                var estadoPendiente = _Dbcontext.CEstados
                    .FirstOrDefault(e => e.NombreEstado == "Pendiente de Clasificar");

                // Luego guardamos los datos delo reclamo


                var Reclamo = new TReclamo
                {
                    NombreProveedor = multimodel.Reclamo.NombreProveedor,
                    DireccionProveedor = multimodel.Reclamo.DireccionProveedor,
                    DetalleReclamo = multimodel.Reclamo.DetalleReclamo,
                    TelefonoProveedor = multimodel.Reclamo.TelefonoProveedor,
                    MontoReclamo = multimodel.Reclamo.MontoReclamo,
                    FechaIngreso = multimodel.Reclamo.FechaIngreso,
                    FechaRevision = multimodel.Reclamo.FechaRevision,
                    IdEmpleado = multimodel.Reclamo.IdEmpleado,
                    IdConsumidor = consumidor.IdConsumidor,
                    IdEstado = estadoPendiente.IdEstado,
                    Activo = multimodel.Reclamo.Activo
                };

                _Dbcontext.TReclamos.Add(Reclamo);
                _Dbcontext.SaveChanges();
                return RedirectToAction("Index", "Home");


            }
            return RedirectToAction("Index", "Home");

        }

        [SessionAuthorize]
        public IActionResult ListaReclamo()
        {
            // Obtenemos la lista de reclamos
            var listaReclamos = _Dbcontext.TReclamos
                .Include(r => r.IdConsumidorNavigation)
                .Include(r => r.IdEstadoNavigation)
                .ToList();
            return View(listaReclamos);



        }

        [SessionAuthorize]
        public IActionResult DetalleReclamo(int id)
        {
            // Obtenemos el reclamo por su ID
            var reclamo = _Dbcontext.TReclamos
                .Include(r => r.IdConsumidorNavigation)
                .Include(r => r.IdEstadoNavigation)
                .Include(r =>r.TAvisos)
                .Include(r => r.TAsesoria)
                .FirstOrDefault(r => r.IdReclamo == id);
            if (reclamo == null)
            {
                return NotFound();
            }
            return View(reclamo);

        }

        public IActionResult ClasificarReclamo(int id)
        {
            // Obtenemos el reclamo por su ID y los estados disponibles
            var reclamo = _Dbcontext.TReclamos
                .Include(r => r.IdConsumidorNavigation)
                .Include(r => r.IdEstadoNavigation)
                .FirstOrDefault(r => r.IdReclamo == id);

            // Hacemos un viemodel con reclamo y estado
            // obtenems la lista de estados para utilizarla en la vista
            var estados = _Dbcontext.CEstados.ToList();
            var clasificar = new ClasificarReclamoViewModel
            {
                Reclamo = reclamo,
                Estados = estados
            };


            if (id == null)
            {
                return RedirectToAction("ListaReclamo");
            }
           
            return View(clasificar);


        }
        [HttpPost]
        public IActionResult ActualizarEstado(int idReclamo, int idEstado, string motivo)
        {
            //Buscamos el reclamo
            var reclamo = _Dbcontext.TReclamos
                .FirstOrDefault(r => r.IdReclamo == idReclamo);
            if(reclamo == null)
            {
                return NotFound();
            }

            reclamo.IdEstado = idEstado;
            _Dbcontext.Entry(reclamo).Property(r =>r.IdEstado).IsModified = true;
            _Dbcontext.SaveChanges();
            if(idEstado == 3 && !string.IsNullOrEmpty(motivo))
            {
                var asesoria = new TAsesorium
                {
                    FechaIngreso = DateTime.Now,
                    MotivoAsesoria = motivo,
                    RespuestaAsesoria= motivo,
                    IdReclamo = idReclamo,
                    Activo = true
                };
                _Dbcontext.TAsesoria.Add(asesoria);
            }
            else if(idEstado==4)
            {
                var aviso = new TAviso
                {
                    FechaIngreso = DateTime.Now,
                    DetalleAviso = motivo,
                    IdReclamo = idReclamo,
                    Activo = true
                };
                _Dbcontext.TAvisos.Add(aviso);
            }
            _Dbcontext.SaveChanges();


            return RedirectToAction("ListaReclamo","Reclamo");    
        }








    }
}
