using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Refractive
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

        [BindProperty] public RefrcIndLanguage RefrcIndLanguage { get; set; }
        [BindProperty] public RefrcIndInvariant RefrcIndInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            RefrcIndLanguage = await _context.RefrcIndLanguage
                .Include(h => h.RefrcInd)
                .FirstOrDefaultAsync(m => m.Id == id);

            RefrcIndInvariant = RefrcIndLanguage.RefrcInd;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            RefrcIndLanguage = await _context.RefrcIndLanguage
                .Include(h => h.RefrcInd)
                .FirstOrDefaultAsync(m => m.Id == id);

            RefrcIndInvariant = RefrcIndLanguage.RefrcInd;

            _context.RefrcIndLanguage.Remove(RefrcIndLanguage);
            _context.RefrcIndInvariant.Remove(RefrcIndInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Refractive",
                system: _contextUtils.GetSystemUrlByHeadClue(RefrcIndInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}