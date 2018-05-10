using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Thermal_Expansion
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

        public IList<HeatExpnLanguage> HeatExpnLanguage { get; set; }
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        public IList<SingTabl> SingTabl { get; set; }

        public async Task OnGetAsync(string systemUrl , string sing)
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            var substanceHeatExpn = _context.HeatExpnLanguage
                .Include(m => m.HeatExpn)
                .Where(m => m.HeatExpn.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId());

            if (!string.IsNullOrEmpty(sing))
            {
                substanceHeatExpn = substanceHeatExpn.Where(m => m.HeatExpn.SingCode == sing);
            }

            substanceHeatExpn = substanceHeatExpn
                .Where(m => m.HeatExpn.DataType == 1);

            HeatExpnLanguage = await substanceHeatExpn.ToListAsync();

            SingTabl = await _context.SingTabl
                .Where(s => s.HeadClue == headClue)
                .ToListAsync();

            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = HeatExpnLanguage
                .ToDictionary(h => h.HeatExpnId, h =>
                    h.HeatExpn.Bknumber.HasValue ? bibliogrLanguage[(int) h.HeatExpn.Bknumber] : null
                );
        }
    }
}