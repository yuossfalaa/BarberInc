using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class DBContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public DBContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }
        public DBContext CreateDbContext()
        {
            DbContextOptionsBuilder<DBContext> options = new DbContextOptionsBuilder<DBContext>();
            _configureDbContext(options);
            return new DBContext(options.Options);

        }
    }
}
