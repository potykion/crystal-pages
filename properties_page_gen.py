from collections import defaultdict

from renederer import render_template

properties = [
    "HeatTabl", "DensTabl", "HardTabl", "SuspTabl",
    "PlavTabl", "CuryTabl", "ModfTabl", "ElemTabl",
    "HeatExpn0", "HeatExpn1", "Dielectr", "DielDiss",
    "PzElTabl", "MechTabl", "Elastic1", "RefrcInd",
    "ConstSel", "ElOpTabl", "NlOpTabl", "EsOpTabl",
    "DecrTabl", "AcOpTabl", "Wavepure", "SistTabl"
]

if __name__ == '__main__':

    for page in ['Properties.cs']:
        rendered = render_template(
            f'{page}_template',
            properties=properties
        )
        with open(f'search/{page}', 'w', encoding='utf-8') as f:
            f.write(rendered)
