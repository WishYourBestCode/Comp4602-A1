// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using Blog.Data;
// using Blog.Models;

// namespace Blog.Controllers
// {
//     [Authorize] // All actions require authorization unless explicitly allowed
//     public class ArticlesController : Controller
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly UserManager<CustomUser> _userManager;

//         public ArticlesController(ApplicationDbContext context, UserManager<CustomUser> userManager)
//         {
//             _context = context;
//             _userManager = userManager;
//         }

//         // GET: /Articles/Index
//         [AllowAnonymous] // Anyone can view the list of articles
//         public async Task<IActionResult> Index()
//         {
//             // Fetch all articles ordered by CreateDate descending
//             var articles = await _context.Articles
//                 .OrderByDescending(a => a.CreateDate)
//                 .ToListAsync();

//             // Check if the current user is logged in
//             CustomUser? user = null;
//             if (User?.Identity?.IsAuthenticated ?? false)
//             {
//                 user = await _userManager.GetUserAsync(User);
//             }

//             // Pass "IsApproved" to the view (used for "more..." links, etc.)
//             ViewBag.IsApprovedUser = user?.IsApproved ?? false;

//             return View(articles);
//         }

//         [AllowAnonymous]
//         public async Task<IActionResult> Detail(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var article = await _context.Articles
//                 .FirstOrDefaultAsync(a => a.ArticleId == id);
//             if (article == null)
//             {
//                 return NotFound();
//             }

//             return View(article);
//         }


//         // GET: /Articles/Create
//         public async Task<IActionResult> Create()
//         {
//             var currentUser = await _userManager.GetUserAsync(User);
//             if (currentUser == null ||
//                 !await _userManager.IsInRoleAsync(currentUser, "Contributor") ||
//                 !currentUser.IsApproved)
//             {
//                 return Forbid(); 
//             }

//             return View();
//         }

//         // POST: /Articles/Create
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Title,Body")] Article article)
//         {
//             var currentUser = await _userManager.GetUserAsync(User);
//             if (currentUser == null ||
//                 !await _userManager.IsInRoleAsync(currentUser, "Contributor") ||
//                 !currentUser.IsApproved)
//             {
//                 return Forbid();
//             }else{
//                  Console.WriteLine("~~~create~~~~~~~~~~~~");
           
//                 // Populate the article fields
//                 article.CreateDate = DateTime.Now;
//                 article.ContributorUsername = currentUser.UserName;

//                 _context.Add(article);
//                 Console.WriteLine("~~~ Saving article. Title: " + article.Title);

//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
           
//         }
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var article = await _context.Articles.FindAsync(id);
//             if (article == null)
//             {
//                 return NotFound();
//             }

//             // Only the original contributor is allowed to edit their article
//             var currentUser = await _userManager.GetUserAsync(User);
//             if (currentUser == null ||
//                 !await _userManager.IsInRoleAsync(currentUser, "Contributor") ||
//                 !currentUser.IsApproved ||
//                 article.ContributorUsername != currentUser.UserName)
//             {
//                 return Forbid();
//             }

//             return View(article);
//         }

//         // POST: /Articles/Edit/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,Body")] Article updatedArticle)
//         {
//             if (id != updatedArticle.ArticleId)
//             {
//                 return NotFound();
//             }

//             var article = await _context.Articles.FindAsync(id);
//             if (article == null)
//             {
//                 return NotFound();
//             }

//             // Only the original contributor is allowed to edit their article
//             var currentUser = await _userManager.GetUserAsync(User);
//             if (currentUser == null ||
//                 !await _userManager.IsInRoleAsync(currentUser, "Contributor") ||
//                 !currentUser.IsApproved ||
//                 article.ContributorUsername != currentUser.UserName)
//             {
//                 return Forbid();
//             }else{
//                 try
//                 {
//                     article.Title = updatedArticle.Title;
//                     article.Body = updatedArticle.Body;
                    
//                     _context.Update(article);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!_context.Articles.Any(a => a.ArticleId == id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
         
//         }

//         // POST: /Articles/Delete/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var article = await _context.Articles.FindAsync(id);
//             if (article == null)
//             {
//                 return NotFound();
//             }

//             // Only the original contributor is allowed to delete their article
//             var currentUser = await _userManager.GetUserAsync(User);
//             if (currentUser == null ||
//                 !await _userManager.IsInRoleAsync(currentUser, "Contributor") ||
//                 !currentUser.IsApproved ||
//                 article.ContributorUsername != currentUser.UserName)
//             {
//                 return Forbid();
//             }

//             _context.Articles.Remove(article);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
