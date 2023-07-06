# Copying files to build output

.NET Core

## Kiedy pliki znajdują się w katalogu projektu

```xml
  <ItemGroup>
    <None Update="files\**\*.md">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
```

## Kiedy pliki znajdują się poza katalogiem projektu

Kiedy potrzebna skopiować jeden plik:

```xml
  <Target Name="CopyCustomContent" AfterTargets="AfterBuild">
    <Copy SourceFiles="../data/README.txt" DestinationFolder="$(OutDir)/files" />
  </Target>
```

Kiedy musisz skopiować wiele plików:

```xml
  <ItemGroup>
    <DataFiles Include="../data/**/*.*" />
  </ItemGroup>

  <Target Name="CopyDataContent" AfterTargets="AfterBuild">
    <Copy SourceFiles="@(DataFiles)" DestinationFolder="$(OutDir)/data" SkipUnchangedFiles="true" />
  </Target>
```
