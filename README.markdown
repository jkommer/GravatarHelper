GravatarHelper
==============

[![NuGet Badge](https://buildstats.info/nuget/GravatarHelper)](https://www.nuget.org/packages/GravatarHelper/)
[![Build status](https://ci.appveyor.com/api/projects/status/s5c2q3ubjyaih8rl/branch/master?svg=true)](https://ci.appveyor.com/project/jkommer/gravatarhelper/branch/master)
[![codecov](https://codecov.io/gh/jkommer/GravatarHelper/branch/master/graph/badge.svg)](https://codecov.io/gh/jkommer/GravatarHelper)

A simple ASP.NET MVC helper for Gravatar providing extension methods to HtmlHelper and UrlHelper. 

## Installation

To install GravatarHelper, run the following command in the Package Manager Console

```console
Install-Package GravatarHelper
```

## Requirements

  * MVC3, MVC4 or MVC5
  
  * .NET 4 Framework or higher

## Example Usages

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

## License

GravatarHelper is licensed under the [CC0 1.0 Universal (CC0 1.0) Public Domain Dedication][1]
  
## Changelog

### 3.3.2

  * Fix config transformation indentation.

### 3.3.1

  * Expose forceSecureUrl through the static helpers.

### 3.3.0

  * Add forceSecureUrl - this can be used to force the use of https gravatar urls over http.

### 3.2.3

  * Add MVC5 to the package description.
  
  * Use NuGet to manage the MVC dependancy.
  
### 3.2.2

  * No longer include a "." in the URL when no extension has been requested.

### 3.2.1

  * Increase MaxImageSize to match new Gravatar maximum size.
  
  * Add a new constant for the blank default image.

### 3.2.0
  
  * Fix the position of the optional file extension to be in the correct place within the URL.
  
  * Make GravatarHelper.MinImageSize and GravatarHelper.MaxImageSize public.
  
  * Reference MVC3 assemblies instead of MVC2.

  * Introduced unit tests.
  
  * Introduced StyleCop.
  
### 3.1.0
  
  * Added support for profile data formats to profile url creation methods. (By David Tischler)
  
  * Added methods to generate Gravatar profile urls. (By David Tischler)

### 3.0.0
  
  * Installs as a seperate library instead of in-project .cs files.

### 2.0.0

  * Now uses semantic versioning for version numbers.
  
  * Added generic HTML attribute support instead of just CSS class and alt text. 
  
  * No longer uses default parameter values.
  
  * Moved the Gravatar URL methods from HtmlHelper to UrlHelper.

### 1.1.0

  * Added class and alt text tag support.

### 1.0.1

  * Implement Web.Config transformations to support @Html.GravatarImage() automatically.
  
  * No longer require a DefaultImage to be specified.

### 1.0.0

  * Initial release.
  
[1]: http://creativecommons.org/publicdomain/zero/1.0/
