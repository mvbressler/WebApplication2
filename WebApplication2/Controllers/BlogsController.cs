using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Repositories;
using WebApplication2.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Controllers
{
    public class BlogsController : Controller
    {
        //private readonly IBlogRepository _repo;
        private readonly IRepository<Blog> _repo;

        //public BlogsController(IBlogRepository repo) _repo = repo;
        public BlogsController(IRepository<Blog> repo) => _repo = repo;

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsyn());
            //return View(await _repo.Blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var blog = await _repo.Blogs
            //    .FirstOrDefaultAsync(m => m.BlogId == id);
            var blog = await _repo.FindAsync(m => m.BlogId == id);
                
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Url")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                //_repo.Add(blog);
                //await _repo.SaveChangesAsync();
                
                await _repo.AddAsyn(blog);
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var blog = await _repo.Blogs.FindAsync(id);
            var blog = await _repo.FindAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Url")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_repo.Update(blog);
                    //await _repo.SaveChangesAsync();
                    
                    await _repo.UpdateAsyn(blog, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExistsAsync(blog.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var blog = await _repo.Blogs
            //    .FirstOrDefaultAsync(m => m.BlogId == id);
            var blog = await _repo.FindAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var blog = await _repo.Blogs.FindAsync(id);
            //_repo.Blogs.Remove(blog);
            //await _repo.SaveChangesAsync();
            
            var blog = await _repo.FindAsync(m => m.BlogId == id);
            
            await _repo.DeleteAsyn(blog);
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExistsAsync(int id)
        {
            //return _repo.Blogs.Any(e => e.BlogId == id);
            var blog = _repo.Find(m => m.BlogId == id);
            return (blog != null);
        }

        [HttpPost, ActionName("MultiDelete")]
        public string MassiveDelete(string selectedIDs)
        {   
            var list = JsonConvert.DeserializeObject < IEnumerable<int>>(selectedIDs);

            _repo.DeleteRangeByIdAsyn(m => list.Any(s => s == m.BlogId));
            return "OK";
        }
    }
}
