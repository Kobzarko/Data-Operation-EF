using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WF_OperationData_EF
{
    class SoccerContext : DbContext
    {
        public SoccerContext():base("DefaultConnection")
        {

        }
        public DbSet<Player> Players{ get; set; }

    }
}
