# crystal-pages

Скрипты для создания страниц в проекте [Crystal](https://github.com/potykion/crystal).

**Для запуска скриптов необходимо установить зависимости из requirements.txt**.

# Описание директорий

## common 

Модули, которые используются скриптами.

## data 

Данные необходимые для генерации страниц.

### 1_tables_with_types.json

Таблицы и их типы.
    
### page_contexts.json:
    
```java
// объект списка - page_contexts.json 
{
  // название таблицы без суффикса
  "model": "AcOpTabl",
  // языковые столбцы
  "language_fields": [
    {
      "name": "E",
      "raw": false
    },
  ],
  // инвариантные столбцы
  "invariant_fields": [
    {
      "name": "WaveLeng",
      "raw": false
    },
  ],
  // наличие ссылок
  "has_references": true,
  // наличие сингонии
  "has_sing": true,
  // директория в которую сохраняются страницы (=TableUrl в Properties)
  "model_dir": "AcoustoOptical",
  // айди свойства
  "model_id": 28
},
```

### search_tables.json

```java
{
  // название таблицы без суффикса
  "table": "DensTabl",
  // инвариантные столбцы
  "invariant_fields": [
    "Density"
  ],
  // обозначение столбца в форме поиска
  "range_fields": [
    "d"
  ],
  // человеко-читаемое название столбца
  "verbose": "\u041f\u043b\u043e\u0442\u043d\u043e\u0441\u0442\u044c"
},
```

## generated 

Сгенерированные страницы.

## templates 

Шаблоны для генерации страниц, синтаксис - [Jinja2](http://jinja.pocoo.org/docs/2.10/).

# Описание скриптов

## properties_page_gen.py

Генератор страницы со свойствами веществ (Substances/Edit/{SystemUrl}).

Шаблоны: 
- Properties.cs_template

## search_page_gen.py

Генератор страницы поиска веществ.

Шаблоны: 
- Search.cshtml_template
- SearchRequest.cs_template
- SearchRequestProcessor.cs_template

## substance_page_gen.py

Генератор страниц свойств веществ.

Шаблоны:
- Create.cshtml.cs_template
- Create.cshtml_template
- Delete.cshtml.cs_template
- Delete.cshtml_template
- Edit.cshtml.cs_template
- Edit.cshtml_template
- Index.cshtml.cs_template
- Index.cshtml_template

## without_translate_page_gen.py

Генератор страницы с записями без перевода.

Шаблоны:
- WithoutTranslate.cshtml.cs_template
- WithoutTranslate.cshtml_template