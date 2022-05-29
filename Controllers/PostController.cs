using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumContext db;
        public PostController(ForumContext DB)
        {
            db = DB;
        }
        public IActionResult Index(int id)
        {
            Topic topic = db.Topics.FirstOrDefault(t => t.Id == id);
            ViewData["Topic"] = topic.Name;
            ViewBag.AllUsers = db.Users.ToList();
            topic.Posts = db.Posts.Where(p => p.Topic.Id == id).ToList();
            return View(topic);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            Topic topic = db.Topics.FirstOrDefault(t => t.Id == id);
            ViewData["Topic"] = topic.Name;
            ViewData["Topicid"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int Topicid, string title, string text)
        {
            Post post = new Post();
             
            Topic thisTopic = await db.Topics.FirstOrDefaultAsync(t => t.Id == Topicid);
            User thisUser = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            post.Title = title;
            post.Text = text;
            post.User = thisUser;
            post.Topic = thisTopic;
            post.UserId = thisUser.Id;
            post.TopicId = thisTopic.Id;
            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = Topicid });
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await db.Posts.FindAsync(id);
            ViewData["TopicId"] = post?.TopicId;
            ViewData["UserId"] = post?.UserId;
            ViewData["PostId"] = post?.Id;
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(post);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.Posts.Any(t => t.Id != post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = post.TopicId });
            }
            return View(post);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = post.TopicId });
        }
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Posts
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}
