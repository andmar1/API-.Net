using apiGestores.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGestores.Context
{
    //Donde hacemos la representacion de la tabla
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Conexion a DB
        }

        public DbSet<Gestores_Bd> gestores_bd { get; set; }  //poner Get y Set para poder modificar la tabla   

    }
}
