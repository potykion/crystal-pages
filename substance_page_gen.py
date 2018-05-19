import json
import os

from renederer import render_template

BASE_DIR = 'generated'


def render_page(page, context, dir_='.'):
    dir_ = os.path.join(BASE_DIR, dir_)
    os.makedirs(dir_, exist_ok=True)

    with open(os.path.join(dir_, page), 'w', encoding='utf-8') as f:
        rendered = render_template(f"{page}_template", **context)
        f.write(rendered)


if __name__ == '__main__':
    with open('page_contexts.json') as f:
        contexts = json.load(f)

    for context in contexts:
        render_page('Index.cshtml', context, context['model_dir'])
        render_page('Index.cshtml.cs', context, context['model_dir'])
        render_page('Edit.cshtml', context, context['model_dir'])
        render_page('Edit.cshtml.cs', context, context['model_dir'])
        render_page('Delete.cshtml', context, context['model_dir'])
        render_page('Delete.cshtml.cs', context, context['model_dir'])
        render_page('Create.cshtml', context, context['model_dir'])
        render_page('Create.cshtml.cs', context, context['model_dir'])
