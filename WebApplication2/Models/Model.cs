using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel;
using WebApplication2.Models;

namespace WebApplication2.SQLite
{
    public class BloggingContext : DbContext
    {
        public BloggingContext( DbContextOptions<BloggingContext> options) : base(options) {}

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }       
    }
    
    
}
