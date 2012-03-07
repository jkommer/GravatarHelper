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
	@Html.GravatarImage("MyEmailAddress@example.com", 80, new { Title = "My Gravatar", Alt = "Gravatar" })	
	
Create a Gravatar clickable url for "MyEmailAddress@example.com", 80 pixels large with "Identicon" as the default image.
	
	:::text
	<a href="@Url.Gravatar("MyEmailAddress@example.com", 80, GravatarHelper.DefaultImageIdenticon)">Your Gravatar</a>
	
## Changelog

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