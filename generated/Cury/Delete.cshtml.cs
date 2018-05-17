using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Cury
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

        [BindProperty] public CuryTablLanguage CuryTablLanguage { get; set; }
        [BindProperty] public CuryTablInvariant CuryTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CuryTablLanguage = await _context.CuryTablLanguage
                .Include(h => h.CuryTabl)
                .FirstAsync(m => m.Id == id);

            CuryTablInvariant = CuryTablLanguage.CuryTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            CuryTablLanguage = await _context.CuryTablLanguage
                .Include(h => h.CuryTabl)
                .FirstAsync(m => m.Id == id);

            CuryTablInvariant = CuryTablLanguage.CuryTabl;

            _context.CuryTablLanguage.Remove(CuryTablLanguage);
            _context.CuryTablInvariant.Remove(CuryTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Cury",
                system: _contextUtils.GetSystemUrlByHeadClue(CuryTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}