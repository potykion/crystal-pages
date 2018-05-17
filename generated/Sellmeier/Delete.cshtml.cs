using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Sellmeier
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

        [BindProperty] public ConstSelLanguage ConstSelLanguage { get; set; }
        [BindProperty] public ConstSelInvariant ConstSelInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ConstSelLanguage = await _context.ConstSelLanguage
                .Include(h => h.ConstSel)
                .FirstOrDefaultAsync(m => m.Id == id);

            ConstSelInvariant = ConstSelLanguage.ConstSel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            ConstSelLanguage = await _context.ConstSelLanguage
                .Include(h => h.ConstSel)
                .FirstOrDefaultAsync(m => m.Id == id);

            ConstSelInvariant = ConstSelLanguage.ConstSel;

            _context.ConstSelLanguage.Remove(ConstSelLanguage);
            _context.ConstSelInvariant.Remove(ConstSelInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Sellmeier",
                system: _contextUtils.GetSystemUrlByHeadClue(ConstSelInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}