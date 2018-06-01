using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal
{
    public class WithoutTranslateModel : PageModel
    {
        private readonly CrystalContext _context;

        public WithoutTranslateModel(CrystalContext context)
        {
            _context = context;
        }

        public IList<SuspTablInvariant> SuspTablWithoutEnglish { get; set; }
        public IList<SuspTablInvariant> SuspTablWithoutRussian { get; set; }
        public IList<HeatTablInvariant> HeatTablWithoutEnglish { get; set; }
        public IList<HeatTablInvariant> HeatTablWithoutRussian { get; set; }
        public IList<ElemTablNewInvariant> ElemTablNewWithoutEnglish { get; set; }
        public IList<ElemTablNewInvariant> ElemTablNewWithoutRussian { get; set; }
        public IList<HeatExpnInvariant> HeatExpnWithoutEnglish { get; set; }
        public IList<HeatExpnInvariant> HeatExpnWithoutRussian { get; set; }
        public IList<AcOpTablInvariant> AcOpTablWithoutEnglish { get; set; }
        public IList<AcOpTablInvariant> AcOpTablWithoutRussian { get; set; }
        public IList<CuryTablInvariant> CuryTablWithoutEnglish { get; set; }
        public IList<CuryTablInvariant> CuryTablWithoutRussian { get; set; }
        public IList<MnOpTablInvariant> MnOpTablWithoutEnglish { get; set; }
        public IList<MnOpTablInvariant> MnOpTablWithoutRussian { get; set; }
        public IList<EsOpTablInvariant> EsOpTablWithoutEnglish { get; set; }
        public IList<EsOpTablInvariant> EsOpTablWithoutRussian { get; set; }
        public IList<DecrTablInvariant> DecrTablWithoutEnglish { get; set; }
        public IList<DecrTablInvariant> DecrTablWithoutRussian { get; set; }
        public IList<HeadTablInvariant> HeadTablWithoutEnglish { get; set; }
        public IList<HeadTablInvariant> HeadTablWithoutRussian { get; set; }
        public IList<HardTablInvariant> HardTablWithoutEnglish { get; set; }
        public IList<HardTablInvariant> HardTablWithoutRussian { get; set; }
        public IList<BibliogrInvariant> BibliogrWithoutEnglish { get; set; }
        public IList<BibliogrInvariant> BibliogrWithoutRussian { get; set; }
        public IList<DielectrInvariant> DielectrWithoutEnglish { get; set; }
        public IList<DielectrInvariant> DielectrWithoutRussian { get; set; }
        public IList<ElOpTablInvariant> ElOpTablWithoutEnglish { get; set; }
        public IList<ElOpTablInvariant> ElOpTablWithoutRussian { get; set; }
        public IList<PzElTablInvariant> PzElTablWithoutEnglish { get; set; }
        public IList<PzElTablInvariant> PzElTablWithoutRussian { get; set; }
        public IList<NlOpTablInvariant> NlOpTablWithoutEnglish { get; set; }
        public IList<NlOpTablInvariant> NlOpTablWithoutRussian { get; set; }
        public IList<DielDissInvariant> DielDissWithoutEnglish { get; set; }
        public IList<DielDissInvariant> DielDissWithoutRussian { get; set; }
        public IList<PlavTablInvariant> PlavTablWithoutEnglish { get; set; }
        public IList<PlavTablInvariant> PlavTablWithoutRussian { get; set; }
        public IList<ElemTablInvariant> ElemTablWithoutEnglish { get; set; }
        public IList<ElemTablInvariant> ElemTablWithoutRussian { get; set; }
        public IList<Elastic1Invariant> Elastic1WithoutEnglish { get; set; }
        public IList<Elastic1Invariant> Elastic1WithoutRussian { get; set; }
        public IList<GrafTablInvariant> GrafTablWithoutEnglish { get; set; }
        public IList<GrafTablInvariant> GrafTablWithoutRussian { get; set; }
        public IList<ConstSelInvariant> ConstSelWithoutEnglish { get; set; }
        public IList<ConstSelInvariant> ConstSelWithoutRussian { get; set; }
        public IList<ModfTablInvariant> ModfTablWithoutEnglish { get; set; }
        public IList<ModfTablInvariant> ModfTablWithoutRussian { get; set; }
        public IList<DensTablInvariant> DensTablWithoutEnglish { get; set; }
        public IList<DensTablInvariant> DensTablWithoutRussian { get; set; }
        public IList<MechTablInvariant> MechTablWithoutEnglish { get; set; }
        public IList<MechTablInvariant> MechTablWithoutRussian { get; set; }
        public IList<RefrcIndInvariant> RefrcIndWithoutEnglish { get; set; }
        public IList<RefrcIndInvariant> RefrcIndWithoutRussian { get; set; }

        public async Task OnGetAsync()
        {
            var SuspTablLanguageRussian = _context.SuspTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.SuspTablId);

            var SuspTablLanguageEnglish = _context.SuspTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.SuspTablId);

            SuspTablWithoutEnglish = await _context.SuspTablInvariant
                .Where(h => !SuspTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            SuspTablWithoutRussian = await _context.SuspTablInvariant
                .Where(h => !SuspTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var HeatTablLanguageRussian = _context.HeatTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.HeatTablId);

            var HeatTablLanguageEnglish = _context.HeatTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.HeatTablId);

            HeatTablWithoutEnglish = await _context.HeatTablInvariant
                .Where(h => !HeatTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            HeatTablWithoutRussian = await _context.HeatTablInvariant
                .Where(h => !HeatTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var ElemTablNewLanguageRussian = _context.ElemTablNewLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.ElemTablNewId);

            var ElemTablNewLanguageEnglish = _context.ElemTablNewLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.ElemTablNewId);

            ElemTablNewWithoutEnglish = await _context.ElemTablNewInvariant
                .Where(h => !ElemTablNewLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            ElemTablNewWithoutRussian = await _context.ElemTablNewInvariant
                .Where(h => !ElemTablNewLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var HeatExpnLanguageRussian = _context.HeatExpnLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.HeatExpnId);

            var HeatExpnLanguageEnglish = _context.HeatExpnLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.HeatExpnId);

            HeatExpnWithoutEnglish = await _context.HeatExpnInvariant
                .Where(h => !HeatExpnLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            HeatExpnWithoutRussian = await _context.HeatExpnInvariant
                .Where(h => !HeatExpnLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var AcOpTablLanguageRussian = _context.AcOpTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.AcOpTablId);

            var AcOpTablLanguageEnglish = _context.AcOpTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.AcOpTablId);

            AcOpTablWithoutEnglish = await _context.AcOpTablInvariant
                .Where(h => !AcOpTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            AcOpTablWithoutRussian = await _context.AcOpTablInvariant
                .Where(h => !AcOpTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var CuryTablLanguageRussian = _context.CuryTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.CuryTablId);

            var CuryTablLanguageEnglish = _context.CuryTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.CuryTablId);

            CuryTablWithoutEnglish = await _context.CuryTablInvariant
                .Where(h => !CuryTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            CuryTablWithoutRussian = await _context.CuryTablInvariant
                .Where(h => !CuryTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var MnOpTablLanguageRussian = _context.MnOpTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.MnOpTablId);

            var MnOpTablLanguageEnglish = _context.MnOpTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.MnOpTablId);

            MnOpTablWithoutEnglish = await _context.MnOpTablInvariant
                .Where(h => !MnOpTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            MnOpTablWithoutRussian = await _context.MnOpTablInvariant
                .Where(h => !MnOpTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var EsOpTablLanguageRussian = _context.EsOpTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.EsOpTablId);

            var EsOpTablLanguageEnglish = _context.EsOpTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.EsOpTablId);

            EsOpTablWithoutEnglish = await _context.EsOpTablInvariant
                .Where(h => !EsOpTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            EsOpTablWithoutRussian = await _context.EsOpTablInvariant
                .Where(h => !EsOpTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var DecrTablLanguageRussian = _context.DecrTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.DecrTablId);

            var DecrTablLanguageEnglish = _context.DecrTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.DecrTablId);

            DecrTablWithoutEnglish = await _context.DecrTablInvariant
                .Where(h => !DecrTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            DecrTablWithoutRussian = await _context.DecrTablInvariant
                .Where(h => !DecrTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var HeadTablLanguageRussian = _context.HeadTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.HeadTablId);

            var HeadTablLanguageEnglish = _context.HeadTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.HeadTablId);

            HeadTablWithoutEnglish = await _context.HeadTablInvariant
                .Where(h => !HeadTablLanguageEnglish.Contains(h.HeadClue))
                .ToListAsync();

            HeadTablWithoutRussian = await _context.HeadTablInvariant
                .Where(h => !HeadTablLanguageRussian.Contains(h.HeadClue))
                .ToListAsync();

            var HardTablLanguageRussian = _context.HardTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.HardTablId);

            var HardTablLanguageEnglish = _context.HardTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.HardTablId);

            HardTablWithoutEnglish = await _context.HardTablInvariant
                .Where(h => !HardTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            HardTablWithoutRussian = await _context.HardTablInvariant
                .Where(h => !HardTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var BibliogrLanguageRussian = _context.BibliogrLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.BibliogrId);

            var BibliogrLanguageEnglish = _context.BibliogrLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.BibliogrId);

            BibliogrWithoutEnglish = await _context.BibliogrInvariant
                .Where(h => !BibliogrLanguageEnglish.Contains(h.Bknumber))
                .ToListAsync();

            BibliogrWithoutRussian = await _context.BibliogrInvariant
                .Where(h => !BibliogrLanguageRussian.Contains(h.Bknumber))
                .ToListAsync();

            var DielectrLanguageRussian = _context.DielectrLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.DielectrId);

            var DielectrLanguageEnglish = _context.DielectrLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.DielectrId);

            DielectrWithoutEnglish = await _context.DielectrInvariant
                .Where(h => !DielectrLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            DielectrWithoutRussian = await _context.DielectrInvariant
                .Where(h => !DielectrLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var ElOpTablLanguageRussian = _context.ElOpTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.ElOpTablId);

            var ElOpTablLanguageEnglish = _context.ElOpTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.ElOpTablId);

            ElOpTablWithoutEnglish = await _context.ElOpTablInvariant
                .Where(h => !ElOpTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            ElOpTablWithoutRussian = await _context.ElOpTablInvariant
                .Where(h => !ElOpTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var PzElTablLanguageRussian = _context.PzElTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.PzElTablId);

            var PzElTablLanguageEnglish = _context.PzElTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.PzElTablId);

            PzElTablWithoutEnglish = await _context.PzElTablInvariant
                .Where(h => !PzElTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            PzElTablWithoutRussian = await _context.PzElTablInvariant
                .Where(h => !PzElTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var NlOpTablLanguageRussian = _context.NlOpTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.NlOpTablId);

            var NlOpTablLanguageEnglish = _context.NlOpTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.NlOpTablId);

            NlOpTablWithoutEnglish = await _context.NlOpTablInvariant
                .Where(h => !NlOpTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            NlOpTablWithoutRussian = await _context.NlOpTablInvariant
                .Where(h => !NlOpTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var DielDissLanguageRussian = _context.DielDissLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.DielDissId);

            var DielDissLanguageEnglish = _context.DielDissLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.DielDissId);

            DielDissWithoutEnglish = await _context.DielDissInvariant
                .Where(h => !DielDissLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            DielDissWithoutRussian = await _context.DielDissInvariant
                .Where(h => !DielDissLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var PlavTablLanguageRussian = _context.PlavTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.PlavTablId);

            var PlavTablLanguageEnglish = _context.PlavTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.PlavTablId);

            PlavTablWithoutEnglish = await _context.PlavTablInvariant
                .Where(h => !PlavTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            PlavTablWithoutRussian = await _context.PlavTablInvariant
                .Where(h => !PlavTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var ElemTablLanguageRussian = _context.ElemTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.ElemTablId);

            var ElemTablLanguageEnglish = _context.ElemTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.ElemTablId);

            ElemTablWithoutEnglish = await _context.ElemTablInvariant
                .Where(h => !ElemTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            ElemTablWithoutRussian = await _context.ElemTablInvariant
                .Where(h => !ElemTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var Elastic1LanguageRussian = _context.Elastic1Language
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.Elastic1Id);

            var Elastic1LanguageEnglish = _context.Elastic1Language
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.Elastic1Id);

            Elastic1WithoutEnglish = await _context.Elastic1Invariant
                .Where(h => !Elastic1LanguageEnglish.Contains(h.Id))
                .ToListAsync();

            Elastic1WithoutRussian = await _context.Elastic1Invariant
                .Where(h => !Elastic1LanguageRussian.Contains(h.Id))
                .ToListAsync();

            var GrafTablLanguageRussian = _context.GrafTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.GrafTablId);

            var GrafTablLanguageEnglish = _context.GrafTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.GrafTablId);

            GrafTablWithoutEnglish = await _context.GrafTablInvariant
                .Where(h => !GrafTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            GrafTablWithoutRussian = await _context.GrafTablInvariant
                .Where(h => !GrafTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var ConstSelLanguageRussian = _context.ConstSelLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.ConstSelId);

            var ConstSelLanguageEnglish = _context.ConstSelLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.ConstSelId);

            ConstSelWithoutEnglish = await _context.ConstSelInvariant
                .Where(h => !ConstSelLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            ConstSelWithoutRussian = await _context.ConstSelInvariant
                .Where(h => !ConstSelLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var ModfTablLanguageRussian = _context.ModfTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.ModfTablId);

            var ModfTablLanguageEnglish = _context.ModfTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.ModfTablId);

            ModfTablWithoutEnglish = await _context.ModfTablInvariant
                .Where(h => !ModfTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            ModfTablWithoutRussian = await _context.ModfTablInvariant
                .Where(h => !ModfTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var DensTablLanguageRussian = _context.DensTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.DensTablId);

            var DensTablLanguageEnglish = _context.DensTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.DensTablId);

            DensTablWithoutEnglish = await _context.DensTablInvariant
                .Where(h => !DensTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            DensTablWithoutRussian = await _context.DensTablInvariant
                .Where(h => !DensTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var MechTablLanguageRussian = _context.MechTablLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.MechTablId);

            var MechTablLanguageEnglish = _context.MechTablLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.MechTablId);

            MechTablWithoutEnglish = await _context.MechTablInvariant
                .Where(h => !MechTablLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            MechTablWithoutRussian = await _context.MechTablInvariant
                .Where(h => !MechTablLanguageRussian.Contains(h.Id))
                .ToListAsync();

            var RefrcIndLanguageRussian = _context.RefrcIndLanguage
                .Where(hl => hl.LanguageId == 1)
                .Select(hl => hl.RefrcIndId);

            var RefrcIndLanguageEnglish = _context.RefrcIndLanguage
                .Where(hl => hl.LanguageId == 2)
                .Select(hl => hl.RefrcIndId);

            RefrcIndWithoutEnglish = await _context.RefrcIndInvariant
                .Where(h => !RefrcIndLanguageEnglish.Contains(h.Id))
                .ToListAsync();

            RefrcIndWithoutRussian = await _context.RefrcIndInvariant
                .Where(h => !RefrcIndLanguageRussian.Contains(h.Id))
                .ToListAsync();

        }
    }
}