. $PSScriptRoot\variables.ps1

$openCover = "c:\OpenCover\OpenCover.Console.exe"

$filter = '"+[GravatarHelper*]*"'
$excludeByAttribute = '"*.ExcludeFromCodeCoverageAttribute"';

If (Test-Path $artifactDirectory\opencoverCoverage.xml) {
    Remove-Item $artifactDirectory\opencoverCoverage.xml
}

ForEach ($project in (Get-ChildItem -Path $testDirectory -File -Recurse -Filter *.csproj)) { 
    $targetArgs = '"test ' + $project.FullName + '"'

    & $openCover -target:"dotnet.exe" `
                 -targetargs:$targetArgs `
                 -output:$artifactDirectory\opencoverCoverage.xml `
                 -excludebyattribute:$excludeByAttribute `
                 -filter:$filter `
                 -register:user `
                 -mergeoutput `
                 -returntargetcode
}