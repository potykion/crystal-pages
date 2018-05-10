using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Dielectric_Loss
{
    public class IndexModel : PageModel
    {
        private readonly CrystalContext _context;
        private readonly ContextUtils _contextUtils;

        public IndexModel(CrystalContext context)
        {
            _context = context;
            _contextUtils = new ContextUtils(_context);
        }

        public IList<DielDissLanguage> DielDissLanguage { get; set; }
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        public IList<SingTabl> SingTabl { get; set; }

        public async Task OnGetAsync(string systemUrl , string sing)
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            var substanceDielDiss = _context.DielDissLanguage
                .Include(m => m.DielDiss)
                .Where(m => m.DielDiss.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId());

            if (!string.IsNullOrEmpty(sing))
            {
                substanceDielDiss = substanceDielDiss.Where(m => m.DielDiss.SingCode == sing);
            }


            DielDissLanguage = await substanceDielDiss.ToListAsync();

            SingTabl = await _context.SingTabl
                .Where(s => s.HeadClue == headClue)
                .ToListAsync();

            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = DielDissLanguage
                .ToDictionary(h => h.DielDissId, h =>
                    h.DielDiss.Bknumber.HasValue ? bibliogrLanguage[(int) h.DielDiss.Bknumber] : null
                );
        }
    }
}