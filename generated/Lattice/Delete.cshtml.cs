using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Lattice
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

        [BindProperty] public ElemTablNewLanguage ElemTablNewLanguage { get; set; }
        [BindProperty] public ElemTablNewInvariant ElemTablNewInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ElemTablNewLanguage = await _context.ElemTablNewLanguage
                .Include(h => h.ElemTablNew)
                .FirstOrDefaultAsync(m => m.Id == id);

            ElemTablNewInvariant = ElemTablNewLanguage.ElemTablNew;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            ElemTablNewLanguage = await _context.ElemTablNewLanguage
                .Include(h => h.ElemTablNew)
                .FirstOrDefaultAsync(m => m.Id == id);

            ElemTablNewInvariant = ElemTablNewLanguage.ElemTablNew;

            _context.ElemTablNewLanguage.Remove(ElemTablNewLanguage);
            _context.ElemTablNewInvariant.Remove(ElemTablNewInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Lattice",
                system: _contextUtils.GetSystemUrlByHeadClue(ElemTablNewInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}