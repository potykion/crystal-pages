using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Wave
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

        [BindProperty] public DecrTablLanguage DecrTablLanguage { get; set; }
        [BindProperty] public DecrTablInvariant DecrTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DecrTablLanguage = await _context.DecrTablLanguage
                .Include(h => h.DecrTabl)
                .FirstAsync(m => m.Id == id);

            DecrTablInvariant = DecrTablLanguage.DecrTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            DecrTablLanguage = await _context.DecrTablLanguage
                .Include(h => h.DecrTabl)
                .FirstAsync(m => m.Id == id);

            DecrTablInvariant = DecrTablLanguage.DecrTabl;

            _context.DecrTablLanguage.Remove(DecrTablLanguage);
            _context.DecrTablInvariant.Remove(DecrTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Wave",
                system: _contextUtils.GetSystemUrlByHeadClue(DecrTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}