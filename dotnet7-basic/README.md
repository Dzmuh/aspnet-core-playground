# ASP.NET Core 7

Рассмотрю новшества ASP.NET Core 7.

## Инструментарий

При разработке задействованы:

* [Central Package Management (CPM)](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)

## Проекты

### WebApp01_Empty

С версии .NET 6 можно создавать проект с минимальной моделью размещения.

В этой модели содержимое файла `Program.cs` сведено до:

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```

В [этом проекте](WebApp01_Empty/README.md) я рассмотрю нюансы новой модели размещения.
