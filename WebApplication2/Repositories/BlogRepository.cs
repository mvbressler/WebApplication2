using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.SQLite;

namespace WebApplication2.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(BloggingContext dbContext) : base(dbContext)
        {
        }
    }
}
