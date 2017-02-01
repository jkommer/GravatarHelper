. $PSScriptRoot\variables.ps1

$openCover = "c:\OpenCover\OpenCover.Console.exe"

$filter = '-filter:"+[GravatarHelper*]*'

If (Test-Path $artifactDirectory\opencoverCoverage.xml) {
    Remove-Item $artifactDirectory\opencoverCoverage.xml
}

ForEach ($project in (Get-ChildItem -Path $testDirectory -File -Recurse -Filter *.csproj)) { 
    $targetArgs = "-targetargs: test " + $project.FullName    
    & $openCover -register:user -mergeoutput -output:$artifactDirectory\opencoverCoverage.xml -returntargetcode -target:"dotnet.exe" $targetArgs $filter
}