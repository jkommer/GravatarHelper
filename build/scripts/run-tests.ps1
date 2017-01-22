$projectRoot = Resolve-Path("$PSScriptRoot\..\..\")
$openCover = "c:\OpenCover\OpenCover.Console.exe"

$filter = '-filter:"+[GravatarHelper*]*'

ForEach ($project in (Get-ChildItem -Path $projectRoot\test\ -File -Recurse -Filter *.csproj)) { 
    $targetArgs = "-targetargs: test " + $project.FullName    
    & $openCover -register:user -mergeoutput -output:$projectRoot\opencoverCoverage.xml -returntargetcode -target:"dotnet.exe" $targetArgs $filter
}