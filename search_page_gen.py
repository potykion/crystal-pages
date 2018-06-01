import json

from common.renderer import render_template


def add_extra_data(search_tables):
    with open("data/1_tables_with_types.json") as f:
        table_with_types = json.load(f)

    for table in search_tables:
        table["type"] = table_with_types[table["table"]]

        string_fields = []

        if table["table"] == "HeatTabl":
            string_fields = ["ZnC"]
        elif table["table"] == "ModfTabl":
            string_fields = ["PointGrp"]
        elif table["table"] == "ElemTabl":
            string_fields = ["NazvAngl"]

        table["string_fields"] = string_fields

        table["range_fields"] = table.get("range_fields", table["invariant_fields"])

    return search_tables


if __name__ == "__main__":
    with open("data/search_tables.json") as f:
        search_tables = json.load(f)

    search_tables = add_extra_data(search_tables)

    for page in ["SearchRequestProcessor.cs", "SearchRequest.cs", "Search.cshtml"]:
        rendered = render_template(f"{page}_template", search_tables=search_tables)
        with open(f"generated/{page}", "w", encoding="utf-8") as f:
            f.write(rendered)
