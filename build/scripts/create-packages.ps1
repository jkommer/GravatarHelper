. $PSScriptRoot\variables.ps1

dotnet pack $projectRoot\src\GravatarHelper\GravatarHelper.csproj -c Release -o $packageOutputDirectory
dotnet pack $projectRoot\src\GravatarHelper.Common\GravatarHelper.Common.csproj -c Release -o $packageOutputDirectory
dotnet pack $projectRoot\src\GravatarHelper.AspNetCore\GravatarHelper.AspNetCore.csproj -c Release -o $packageOutputDirectory