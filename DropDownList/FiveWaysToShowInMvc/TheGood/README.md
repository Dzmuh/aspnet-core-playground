# Способ третий. The Good (Хороший)

Мы используем модель, которая содержит все необходимые данные. Прямо как в книжке.

```csharp
public class ViewModel
{
	public MovieModel Movie { get; set; }
	public IEnumerable<SelectListItem> Genres { get; set; }
}
```

Теперь задача контроллера — изготовить эту модель из имеющихся данных:

```csharp
public IActionResult Index()
{
    var model = new ViewModel();
    model.Movie = Data.GetMovie();
    model.Genres = from genre in Data.GetGenres()
                    select new SelectListItem
                    {
                        Text = genre.Name,
                        Value = genre.Id.ToString()
                    };

    return View(model);
}
```

**Плюс:** каноническая реализация паттерна MVC (это хорошо не потому, что хорошо, а потому, что другим разработчикам будет проще врубиться в тему).\
**Минусы:** как и в прошлом примере, метод контроллера перегружен: он озабочен «что» и «как»; кроме того, эти же «что» и «как» соединены в одном классе `ViewModel`.\
**Когда использовать:** в небольших и средних формах с одним-тремя списками и другими нестандартными элементами ввода.
