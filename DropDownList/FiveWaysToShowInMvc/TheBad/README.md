# Способ второй. The Bad (Плохой)

Как и прежде, модель передаем стандартным образом через контроллер. Все дополнительные данные передаем через ViewData. Метод контроллера у нас вот:

```csharp
public IActionResult Index()
{
    var model = Data.GetMovie();
    ViewData["AllGenres"] = from genre in Data.GetGenres() select new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() };
    return View(model);
}
```

Понятно, что в общем случае мы можем нагромоздить во ViewData все, что угодно. Дальше все это дело мы используем во View:

```csharp
@Html.DropDownListFor(model => model.GenreId, (IEnumerable<SelectListItem>)ViewData["AllGenres"], "choose")
```

**Плюс:** данные для «что» и «как» четко разделены: первые хранятся в модели, вторые — во ViewData.\
**Минусы:** данные-то разделены, а вот метод контроллера «перегружен»: он занимается двумя (а в перспективе — многими) вещами сразу; кроме того, у меня почему-то инстинктивное отношение к ViewData как к «хакерскому» средству решения проблем. Хотя, сторонники динамических языков, возможно, с удовольствием пользуются ViewData.\
**Когда использовать:** в небольших формах с одним-двумя списками.
