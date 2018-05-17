using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Density
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

        [BindProperty] public DensTablLanguage DensTablLanguage { get; set; }
        [BindProperty] public DensTablInvariant DensTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DensTablLanguage = await _context.DensTablLanguage
                .Include(h => h.DensTabl)
                .FirstAsync(m => m.Id == id);

            DensTablInvariant = DensTablLanguage.DensTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            DensTablLanguage = await _context.DensTablLanguage
                .Include(h => h.DensTabl)
                .FirstAsync(m => m.Id == id);

            DensTablInvariant = DensTablLanguage.DensTabl;

            _context.DensTablLanguage.Remove(DensTablLanguage);
            _context.DensTablInvariant.Remove(DensTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Density",
                system: _contextUtils.GetSystemUrlByHeadClue(DensTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}