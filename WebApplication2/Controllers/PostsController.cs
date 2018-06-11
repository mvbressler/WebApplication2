using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    public class PostsController : Controller
    {
        private readonly IRepository<Post> _repo;
        private readonly IRepository<Blog> _blogrepo;

        public PostsController(IRepository<Post> repo, IRepository<Blog> blogrepo)
        {
            _repo = repo;
            _blogrepo = blogrepo;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsyn());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _repo.FindAsync(m => m.PostId == id);
                
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()

        {       
            ViewData["BlogId"] = new SelectList(_blogrepo.GetAll(), "BlogId", "BlogId");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Content,BlogId")] Post post)
        {
            if (ModelState.IsValid)
            {
                //_repo.Add(post);
                await _repo.AddAsyn(post);
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["BlogId"] = new SelectList(_blogrepo.GetAll(), "BlogId", "BlogId", post.BlogId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _repo.FindAsync(m =>m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_blogrepo.GetAll(), "BlogId", "BlogId", post.BlogId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Content,BlogId")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_repo.Update(post);
                    await _repo.UpdateAsyn(post, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["BlogId"] = new SelectList(_blogrepo.GetAll(), "BlogId", "BlogId", post.BlogId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _repo.FindAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _repo.FindAsync(m => m.PostId == id);
            await _repo.DeleteAsyn(post);
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            //var post = _repo.Find(m => m.PostId == id);
            
            return (_repo.Find(m => m.PostId == id) != null);
        }
    }
}
