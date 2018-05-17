using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Crystal_Mod
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

        [BindProperty] public ModfTablLanguage ModfTablLanguage { get; set; }
        [BindProperty] public ModfTablInvariant ModfTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ModfTablLanguage = await _context.ModfTablLanguage
                .Include(h => h.ModfTabl)
                .FirstAsync(m => m.Id == id);

            ModfTablInvariant = ModfTablLanguage.ModfTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            ModfTablLanguage = await _context.ModfTablLanguage
                .Include(h => h.ModfTabl)
                .FirstAsync(m => m.Id == id);

            ModfTablInvariant = ModfTablLanguage.ModfTabl;

            _context.ModfTablLanguage.Remove(ModfTablLanguage);
            _context.ModfTablInvariant.Remove(ModfTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Crystal_Mod",
                system: _contextUtils.GetSystemUrlByHeadClue(ModfTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}