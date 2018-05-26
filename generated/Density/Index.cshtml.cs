using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Density
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

        public IList<DensTablLanguage> DensTablLanguage { get; set; }
        public IList<GrafTablLanguage> Images { get; set; }
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        public IList<SingTabl> SingTabl { get; set; }

        public async Task OnGetAsync(string systemUrl , string sing)
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            var substanceDensTabl = _context.DensTablLanguage
                .Include(m => m.DensTabl)
                .Where(m => m.DensTabl.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId());

            if (!string.IsNullOrEmpty(sing))
            {
                substanceDensTabl = substanceDensTabl.Where(m => m.DensTabl.SingCode == sing);
            }


            DensTablLanguage = await substanceDensTabl.ToListAsync();

            Images = await _context.GrafTablLanguage
                .Include(image => image.GrafTabl)
                .Where(image => image.LanguageId == this.GetLanguageId())
                .Where(image => image.GrafTabl.HeadClue == headClue)
                .Where(image => image.GrafTabl.NompClue == 6)
                .ToListAsync();

            SingTabl = await _context.SingTabl
                .Where(s => s.HeadClue == headClue)
                .ToListAsync();

            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = new Dictionary<int, BibliogrLanguage>();
            foreach (var m in DensTablLanguage)
            {
                if (References.ContainsKey(m.DensTablId)) continue;

                var bibliogr = m.DensTabl.Bknumber.HasValue
                    ? bibliogrLanguage[(int) m.DensTabl.Bknumber]
                    : null;

                References[m.DensTablId] = bibliogr;
            }

        }
    }
}