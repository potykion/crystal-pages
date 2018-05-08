import json
import os

import jinja2
from jinja2 import FileSystemLoader

env = jinja2.Environment(
    loader=FileSystemLoader("templates")
)


def render_page(page, context, dir_='.'):
    template = env.get_template(f"{page}_template")
    with open(os.path.join(dir_, page), 'w', encoding='utf-8') as f:
        rendered = template.render(context)
        f.write(rendered)


if __name__ == '__main__':
    with open('page_contexts.json') as f:
        contexts = json.load(f)

    model_dir = 'Heat'
    dir_ = f'generated/{model_dir}'
    os.makedirs(dir_, exist_ok=True)

    context = next(
        context
        for context in contexts
        if context['model'] == 'HeatTabl'
    )
    context['model_dir'] = model_dir

    render_page('Index.cshtml', context, dir_)
    render_page('Index.cshtml.cs', context, dir_)
