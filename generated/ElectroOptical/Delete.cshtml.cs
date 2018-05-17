using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.ElectroOptical
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly CrystalContext _context;
        private readonly UrlBuilder _urlBuilder = new UrlBuilder();
        private readonly ContextUtils _contextUtils;

        public DeleteModel(CrystalContext context)
        {
            _context = context;
            _contextUtils = new ContextUtils(context);
        }

        [BindProperty] public ElOpTablLanguage ElOpTablLanguage { get; set; }
        [BindProperty] public ElOpTablInvariant ElOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ElOpTablLanguage = await _context.ElOpTablLanguage
                .Include(h => h.ElOpTabl)
                .FirstAsync(m => m.Id == id);

            ElOpTablInvariant = ElOpTablLanguage.ElOpTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            ElOpTablLanguage = await _context.ElOpTablLanguage
                .Include(h => h.ElOpTabl)
                .FirstAsync(m => m.Id == id);

            ElOpTablInvariant = ElOpTablLanguage.ElOpTabl;

            _context.ElOpTablLanguage.Remove(ElOpTablLanguage);
            _context.ElOpTablInvariant.Remove(ElOpTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "ElectroOptical",
                system: _contextUtils.GetSystemUrlByHeadClue(ElOpTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}