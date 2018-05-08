import json
import os

import jinja2
from jinja2 import FileSystemLoader

BASE_DIR = 'generated'

env = jinja2.Environment(
    loader=FileSystemLoader("templates")
)


def render_page(page, context, dir_='.'):
    dir_ = os.path.join(BASE_DIR, dir_)
    os.makedirs(dir_, exist_ok=True)

    template = env.get_template(f"{page}_template")
    with open(os.path.join(dir_, page), 'w', encoding='utf-8') as f:
        rendered = template.render(context)
        f.write(rendered)


if __name__ == '__main__':
    with open('page_contexts.json') as f:
        contexts = json.load(f)

    for context in contexts:
        render_page('Index.cshtml', context, context['model_dir'])
        render_page('Index.cshtml.cs', context, context['model_dir'])
