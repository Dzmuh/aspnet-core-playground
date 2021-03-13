# Способ первый. The Ugly (Уродливый)

Мы можем передавать данные о фильме через контроллер, а список жанров наш вью будет извлекать сам. Понятно, что это нарушение всех мыслимых принципов, но чисто теоретически такая возможность имеется.

Имеем такие классы моделей:

```csharp
public class MovieModel {
    public string Title { get; set; }
    public int GenreId { get; set; }
}

public class GenreModel {
    public int Id { get; set; }
    public string Name { get; set; }
}
```

Метод контроллера, как в руководствах для начинающих:

```csharp
public ActionResult TheUgly(){
    var model = Data.GetMovie();
    return View(model);
}
```

Здесь **Data** — это просто статический класс, который нам выдает данные. Он придуман исключительно для простоты обсуждения, и я бы не советовал использовать что-либо подобное в реальной жизни:

```csharp
public static class Data {
    public static MovieModel GetMovie() {
        return new MovieModel {Title = "Santa Barbara", GenreId = 1};
    }
}
```

Приведем теперь наш ужасный `View`, точнее, ту его часть, которая касается списка жанров. По сути, наш код должен вытащить все жанры и преобразовать их в элементы типа `SelectListItem`.

```csharp
var selectList = from genre in Data.GetGenres() select new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() };

@Html.DropDownListFor(model => model.GenreId, selectList, "choose")
```

Что же здесь ужасного? Дело в том, что главное достоинство Asp.Net MVC, на мой взгляд, состоит в том, что у нас есть четкое разделение обязанностей (separation of concerns, или SoC).
В частности, именно контроллер отвечает за передачу данных во вью.
Разумеется, это не догма, а просто хорошее правило.
Нарушая его, Вы рискуете наворотить кучу ненужного кода в Ваших представлениях, и разобраться через год, что к чему, будет очень непросто.

**Плюс:** простой контроллер.\
**Минус:** в представление попадает код, свойственный контроллеру; грубое нарушение принципов модели MVC.\
**Когда использовать:** если надо быстро набросать демку.
