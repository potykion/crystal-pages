import jinja2
from jinja2 import FileSystemLoader

env = jinja2.Environment(
    loader=FileSystemLoader("templates"),
    trim_blocks=True,
    lstrip_blocks=True
)


def render_template(template_name, **context):
    template = env.get_template(template_name)
    return template.render(context)
