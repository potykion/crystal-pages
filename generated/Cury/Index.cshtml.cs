using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Cury
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

        public IList<CuryTablLanguage> CuryTablLanguage { get; set; }
        public IList<GrafTablLanguage> Images { get; set; }
        public IDictionary<int, BibliogrLanguage> References { get; set; }

        public async Task OnGetAsync(string systemUrl )
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            var substanceCuryTabl = _context.CuryTablLanguage
                .Include(m => m.CuryTabl)
                .Where(m => m.CuryTabl.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId());



            CuryTablLanguage = await substanceCuryTabl.ToListAsync();

            Images = await _context.GrafTablLanguage
                .Include(image => image.GrafTabl)
                .Where(image => image.LanguageId == this.GetLanguageId())
                .Where(image => image.GrafTabl.HeadClue == headClue)
                .Where(image => image.GrafTabl.NompClue == 10)
                .ToListAsync();


            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = new Dictionary<int, BibliogrLanguage>();
            foreach (var m in CuryTablLanguage)
            {
                if (References.ContainsKey(m.CuryTablId)) continue;

                var bibliogr = m.CuryTabl.Bknumber.HasValue
                    ? bibliogrLanguage[(int) m.CuryTabl.Bknumber]
                    : null;

                References[m.CuryTablId] = bibliogr;
            }

        }
    }
}