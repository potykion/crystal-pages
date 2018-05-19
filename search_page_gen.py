import json

from renederer import render_template


def invariant_check():
    global f, table
    with open('page_contexts.json') as f:
        contexts = json.load(f)
    for table in search_tables:
        name = table['table']
        fields = table['invariant_fields']

        table_context = next(
            (context
             for context in contexts
             if context['model'] == name),
            None
        )

        if not table_context:
            print(f'No context for: {table}')
            continue

        invariant_fields = [
            field['name']
            for field in table_context['invariant_fields']
        ]
        only_invariant_fields = all(
            field in invariant_fields
            for field in fields
        )

        if not only_invariant_fields:
            print(name)


def add_extra_data(search_tables):
    with open('1_tables_with_types.json') as f:
        table_with_types = json.load(f)

    for table in search_tables:
        table['type'] = table_with_types[table['table']]

        string_fields = []

        if table['table'] == 'HeatTabl':
            string_fields = ['ZnC']
        elif table['table'] == 'ModfTabl':
            string_fields = ['PointGrp']
        elif table['table'] == 'ElemTabl':
            string_fields = ['NazvAngl']

        table['string_fields'] = string_fields

    return search_tables


if __name__ == '__main__':
    with open("cry_search.json") as f:
        search_tables = json.load(f)

    # invariant_check()

    search_tables = add_extra_data(search_tables)

    for page in ['SearchRequestProcessor.cs', 'SearchRequest.cs', 'Search.cshtml']:
        rendered = render_template(f'{page}_template', search_tables=search_tables)
        with open(f'search/{page}', 'w', encoding='utf-8') as f:
            f.write(rendered)
