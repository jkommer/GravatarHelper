GravatarHelper
==============

A simple ASP.NET MVC helper for Gravatar providing extension methods to HtmlHelper and UrlHelper. 

## Installation

To install GravatarHelper, run the following command in the Package Manager Console

	:::console
	Install-Package GravatarHelper

## Requirements

  * MVC3
  
  * .NET 4 Framework
  
  * NuGet

## Example Usages

Create a Gravatar img tag for "MyEmailAddress@example.com", 80 pixels large with "My Gravatar" as title  and "Gravatar" as alt text.

	:::text
	@Html.Gravatar("MyEmailAddress@example.com", 80, new { Title = "My Gravatar", Alt = "Gravatar" })
	
Create a Gravatar link for "MyEmailAddress@example.com", 80 pixels large with "Identicon" as the default image.

	:::text
	<a href="@Url.Gravatar("MyEmailAddress@example.com", 80, GravatarHelper.DefaultImageIdenticon)">Your Gravatar</a>
	
Create a Gravatar Profile link for "MyEmailAddress@example.com".
	
	:::text
	<a href="@Url.GravatarProfile("MyEmailAddress@example.com")">Your Gravatar Profile</a>

## License

GravatarHelper is licensed under the [CC0 1.0 Universal (CC0 1.0) Public Domain Dedication][1]
	
## Changelog

### x.x.x
  
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