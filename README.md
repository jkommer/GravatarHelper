GravatarHelper
==============

[![Build status](https://ci.appveyor.com/api/projects/status/s5c2q3ubjyaih8rl/branch/master?svg=true)](https://ci.appveyor.com/project/jkommer/gravatarhelper/branch/master)
[![codecov](https://codecov.io/gh/jkommer/GravatarHelper/branch/master/graph/badge.svg)](https://codecov.io/gh/jkommer/GravatarHelper)
[![license](https://img.shields.io/github/license/jkommer/gravatarhelper.svg)](https://creativecommons.org/publicdomain/zero/1.0/)

| NuGet Package              | Statistics                                                                                                                                         |
| -------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------|
| GravatarHelper             | [![GravatarHelper](https://buildstats.info/nuget/GravatarHelper)](https://www.nuget.org/packages/GravatarHelper/)                                  |
| GravatarHelper.Common      | [![GravatarHelper.Common](https://buildstats.info/nuget/GravatarHelper.Common)](https://www.nuget.org/packages/GravatarHelper.Common/)             |
| GravatarHelper.AspNetCore  | [![GravatarHelper.AspNetCore](https://buildstats.info/nuget/GravatarHelper.AspNetCore)](https://www.nuget.org/packages/GravatarHelper.AspNetCore/) |

A set of libraries providing Gravatar support to .NET and .NET Core .

## Installation

To install GravatarHelper, run the following command in the Package Manager Console

**ASP.NET MVC 3 / 4 / 5:**

```console
Install-Package GravatarHelper
```

**ASP.NET Core MVC:**

```console
Install-Package GravatarHelper.AspNetCore
```

and add the following to your `_ViewImports.cshtml` file:

```csharp
@using GravatarHelper.Common
@addTagHelper *, GravatarHelper.AspNetCore
```

**Non-MVC:**

```console
Install-Package GravatarHelper.Common
```

## Example Usages

### GravatarHelper

Create a Gravatar img tag for "MyEmailAddress@example.com", 80 pixels large with "My Gravatar" as title  and "Gravatar" as alt text.

```csharp
@Html.Gravatar("MyEmailAddress@example.com", 80, new { Title = "My Gravatar", Alt = "Gravatar" })
```

Create a Gravatar link for "MyEmailAddress@example.com", 80 pixels large with "Identicon" as the default image.

```csharp
<a href="@Url.Gravatar("MyEmailAddress@example.com", 80, GravatarHelper.DefaultImageIdenticon)">Your Gravatar</a>
```

Create a Gravatar Profile link for "MyEmailAddress@example.com".
  
```csharp 
<a href="@Url.GravatarProfile("MyEmailAddress@example.com")">Your Gravatar Profile</a>
```

### GravatarHelper.AspNetCore

```Html
<img gravatar-email="MyEmailAddress@example.com" gravatar-size="80" alt="My Gravatar" />
```

### Troubleshooting

### GravatarHelper

The NuGet package should by default add two using statements to the /views/web.config file allowing Gravatar extension methods to be used throughout all view files. If this fails then you can manually add the following two namespaces to the /views/web.config file:

```xml
<system.web.webPages.razor>
  <pages>
    <namespaces>
      <add namespace="GravatarHelper.Common" />
      <add namespace="GravatarHelper.Extensions" />
    </namespaces>
  </pages>
</system.web.webPages.razor>
```

Or alternatively add the namespace directly to the relevant view files:

```csharp
@using GravatarHelper.Common
@using GravatarHelper.Extensions
```

## License

GravatarHelper is licensed under the [CC0 1.0 Universal (CC0 1.0) Public Domain Dedication][1]

[1]: http://creativecommons.org/publicdomain/zero/1.0/
