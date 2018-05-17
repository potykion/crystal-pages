using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Dielectric
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

        [BindProperty] public DielectrLanguage DielectrLanguage { get; set; }
        [BindProperty] public DielectrInvariant DielectrInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DielectrLanguage = await _context.DielectrLanguage
                .Include(h => h.Dielectr)
                .FirstAsync(m => m.Id == id);

            DielectrInvariant = DielectrLanguage.Dielectr;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            DielectrLanguage = await _context.DielectrLanguage
                .Include(h => h.Dielectr)
                .FirstAsync(m => m.Id == id);

            DielectrInvariant = DielectrLanguage.Dielectr;

            _context.DielectrLanguage.Remove(DielectrLanguage);
            _context.DielectrInvariant.Remove(DielectrInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Dielectric",
                system: _contextUtils.GetSystemUrlByHeadClue(DielectrInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}