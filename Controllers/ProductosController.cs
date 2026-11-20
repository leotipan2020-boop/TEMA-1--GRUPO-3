

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    [Authorize]
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Productos
                .Include(p => p.Categoria);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Productos/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
                return NotFound();

            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias, "Id", "Nombre", producto.CategoriaId);

            return View(producto);
        }

        // GET: Productos/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias, "Id", "Nombre");

            return View();
        }

        // POST: Productos/Create
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nombre,Precio,Stock,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias, "Id", "Nombre", producto.CategoriaId);

            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias, "Id", "Nombre", producto.CategoriaId);

            return View(producto);
        }

        // POST: Productos/Edit/5
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nombre,Precio,Stock,CategoriaId")] Producto producto)
        {
            if (id != producto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(
                _context.Categorias, "Id", "Nombre", producto.CategoriaId);

            return View(producto);
        }

        // GET: Productos/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        // POST: Productos/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
