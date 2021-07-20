using apiGestores.Context;
using apiGestores.Models;  //usar modelo
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiGestores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestoresController : ControllerBase
    {
        private readonly AppDbContext context;

        public GestoresController(AppDbContext context)
        {
            this.context = context;
        }

        //Peticiones

        // GET: api/<GestoresController>
        [HttpGet]
        public ActionResult Get()
        {
            //Manejo de Errores
            try
            {
                return Ok(context.gestores_bd.ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<GestoresController>/5
        [HttpGet("{id}", Name ="GetGestor")]
        public ActionResult Get(int id)
        {
            try
            {
                //Ver con Linq cual coincide con el ID para mostrarlo con el metodo OK
                var gestor = context.gestores_bd.FirstOrDefault(g => g.id == id);
                return Ok(gestor);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<GestoresController>
        [HttpPost]
        public ActionResult Post([FromBody] Gestores_Bd gestor)
        {
            try
            {
                context.gestores_bd.Add(gestor);   //Insertar registro dentro de DB
                context.SaveChanges(); //Gurdar cambios
                return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor); ///Retornamos al usuario lo que se inserto, reutilizando metodo "GetGestor"

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<GestoresController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Gestores_Bd gestor)
        {
            try
            {
                if (gestor.id == id)
                {
                    context.Entry(gestor).State = EntityState.Modified; 
                    context.SaveChanges(); //Gurdar cambios
                    return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor); 
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<GestoresController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var gestor = context.gestores_bd.FirstOrDefault(g => g.id == id);
                if (gestor != null)
                {
                    context.gestores_bd.Remove(gestor);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
