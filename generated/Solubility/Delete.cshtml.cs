using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Solubility
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

        [BindProperty] public SuspTablLanguage SuspTablLanguage { get; set; }
        [BindProperty] public SuspTablInvariant SuspTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SuspTablLanguage = await _context.SuspTablLanguage
                .Include(h => h.SuspTabl)
                .FirstAsync(m => m.Id == id);

            SuspTablInvariant = SuspTablLanguage.SuspTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            SuspTablLanguage = await _context.SuspTablLanguage
                .Include(h => h.SuspTabl)
                .FirstAsync(m => m.Id == id);

            SuspTablInvariant = SuspTablLanguage.SuspTabl;

            _context.SuspTablLanguage.Remove(SuspTablLanguage);
            _context.SuspTablInvariant.Remove(SuspTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Solubility",
                system: _contextUtils.GetSystemUrlByHeadClue(SuspTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}