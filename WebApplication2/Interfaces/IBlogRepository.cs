using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.SQLite;

namespace WebApplication2.Repositories
{
    interface IBlogRepository: IRepository<Blog>
    {
    }
}
