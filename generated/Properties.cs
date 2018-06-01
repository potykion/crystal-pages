using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances
{
    public class SubstanceInfoModel : PageModel
    {
        private readonly Dictionary<string, bool> _availableProperties =
            new Dictionary<string, bool>();

        private readonly CrystalContext _context;

        public SubstanceInfoModel(CrystalContext context)
        {
            _context = context;
        }

        public HeadTablLanguage HeadTablLanguage { get; set; }
        public IList<PropertiesLanguage> PropertiesLanguage { get; set; }

        public async Task OnGetAsync(string systemUrl)
        {
            HeadTablLanguage = await _context.HeadTablLanguage
                .Include(headTabl => headTabl.HeadTabl)
                .Where(headTabl => headTabl.HeadTabl.SystemUrl == systemUrl)
                .FirstAsync();

            _availableProperties["HeatTabl"] = await _context.HeatTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["DensTabl"] = await _context.DensTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["HardTabl"] = await _context.HardTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["SuspTabl"] = await _context.SuspTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["PlavTabl"] = await _context.PlavTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["CuryTabl"] = await _context.CuryTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["ModfTabl"] = await _context.ModfTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["ElemTabl"] = await _context.ElemTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["HeatExpn0"] = await _context.HeatExpn0Invariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["HeatExpn1"] = await _context.HeatExpn1Invariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["Dielectr"] = await _context.DielectrInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["DielDiss"] = await _context.DielDissInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["PzElTabl"] = await _context.PzElTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["MechTabl"] = await _context.MechTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["Elastic1"] = await _context.Elastic1Invariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["RefrcInd"] = await _context.RefrcIndInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["ConstSel"] = await _context.ConstSelInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["ElOpTabl"] = await _context.ElOpTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["NlOpTabl"] = await _context.NlOpTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["EsOpTabl"] = await _context.EsOpTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["DecrTabl"] = await _context.DecrTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["AcOpTabl"] = await _context.AcOpTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["Wavepure"] = await _context.WavepureInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;
            _availableProperties["SistTabl"] = await _context.SistTablInvariant
                                                   .Where(m => m.HeadClue == HeadTablLanguage.HeadTablId)
                                                   .FirstOrDefaultAsync() != null;

            PropertiesLanguage = await _context.PropertiesLanguage
                .Include(p => p.Properties)
                .Where(p => _availableProperties.Keys.Contains(p.Properties.TableName))
                .Where(p => p.LanguageId == this.GetLanguageId())
                .ToListAsync();
        }
    }
}