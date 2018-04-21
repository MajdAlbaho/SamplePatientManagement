using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.Linq.LinqDb
{
    public class DbContextManager : ClinicSystemDataContext
    {
        public DbContextManager()
        {
            // Change you connection string
            this.Connection.ConnectionString = "Data Source=Home_Laptop;Initial Catalog=ClinicSystem.Dev;Integrated Security=True";
        }
    }
}
