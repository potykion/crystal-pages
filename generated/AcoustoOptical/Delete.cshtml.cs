using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.AcoustoOptical
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

        [BindProperty] public AcOpTablLanguage AcOpTablLanguage { get; set; }
        [BindProperty] public AcOpTablInvariant AcOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AcOpTablLanguage = await _context.AcOpTablLanguage
                .Include(h => h.AcOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            AcOpTablInvariant = AcOpTablLanguage.AcOpTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            AcOpTablLanguage = await _context.AcOpTablLanguage
                .Include(h => h.AcOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            AcOpTablInvariant = AcOpTablLanguage.AcOpTabl;

            _context.AcOpTablLanguage.Remove(AcOpTablLanguage);
            _context.AcOpTablInvariant.Remove(AcOpTablInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "AcoustoOptical",
                system: _contextUtils.GetSystemUrlByHeadClue(AcOpTablInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}