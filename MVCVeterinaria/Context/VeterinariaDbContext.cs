using Microsoft.EntityFrameworkCore;
using MVCVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCVeterinaria.Context
{
    public class VeterinariaDbContext: DbContext
    {
        public VeterinariaDbContext(DbContextOptions<VeterinariaDbContext> options):base (options) { }
        public DbSet<Turno> Turnos { get; set; }

        public DbSet<Medico> Medicos { get; set; }

    }
}
