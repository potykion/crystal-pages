using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Heat
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

        public IList<HeatTablLanguage> HeatTablLanguage { get; set; }
        public IList<GrafTablLanguage> Images { get; set; }
        public IDictionary<int, BibliogrLanguage> References { get; set; }

        public async Task OnGetAsync(string systemUrl )
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            var substanceHeatTabl = _context.HeatTablLanguage
                .Include(m => m.HeatTabl)
                .Where(m => m.HeatTabl.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId());



            HeatTablLanguage = await substanceHeatTabl.ToListAsync();

            Images = await _context.GrafTablLanguage
                .Include(image => image.GrafTabl)
                .Where(image => image.LanguageId == this.GetLanguageId())
                .Where(image => image.GrafTabl.HeadClue == headClue)
                .Where(image => image.GrafTabl.NompClue == 5)
                .ToListAsync();


            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = new Dictionary<int, BibliogrLanguage>();
            foreach (var m in HeatTablLanguage)
            {
                if (References.ContainsKey(m.HeatTablId)) continue;

                var bibliogr = m.HeatTabl.Bknumber.HasValue
                    ? bibliogrLanguage[(int) m.HeatTabl.Bknumber]
                    : null;

                References[m.HeatTablId] = bibliogr;
            }

        }
    }
}