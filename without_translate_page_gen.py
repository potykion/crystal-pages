from collections import defaultdict

from common.renderer import render_template

tables = {
    "HeadTabl",
    "Bibliogr",
    "DecrTabl",
    "ElOpTabl",
    "MnOpTabl",
    "EsOpTabl",
    "SuspTabl",
    "CuryTabl",
    "MechTabl",
    "PlavTabl",
    "NlOpTabl",
    "DensTabl",
    "DielDiss",
    "GrafTabl",
    "HeatExpn",
    "Elastic1",
    "HeatTabl",
    "ElemTablNew",
    "HardTabl",
    "AcOpTabl",
    "PzElTabl",
    "Dielectr",
    "ModfTabl",
    "ElemTabl",
    "RefrcInd",
    "ConstSel",
}

pks = defaultdict(lambda: "Id")
pks.update({"Bibliogr": "Bknumber", "HeadTabl": "HeadClue"})

if __name__ == "__main__":
    for page in ["WithoutTranslate.cshtml.cs", "WithoutTranslate.cshtml"]:
        rendered = render_template(f"{page}_template", tables=tables, pks=pks)
        with open(f"generated/{page}", "w", encoding="utf-8") as f:
            f.write(rendered)
