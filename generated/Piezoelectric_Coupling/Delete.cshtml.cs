using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Piezoelectric_Coupling
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

        [BindProperty] public MechTablLanguage MechTablLanguage { get; set; }
        [BindProperty] public MechTablInvariant MechTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MechTablLanguage = await _context.MechTablLanguage
                .Include(h => h.MechTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            MechTablInvariant = MechTablLanguage.MechTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            MechTablLanguage = await _context.MechTablLanguage
                .Include(h => h.MechTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            MechTablInvariant = MechTablLanguage.MechTabl;

            _context.MechTablLanguage.Remove(MechTablLanguage);
            _context.MechTablInvariant.Remove(MechTablInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Piezoelectric_Coupling",
                system: _contextUtils.GetSystemUrlByHeadClue(MechTablInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}