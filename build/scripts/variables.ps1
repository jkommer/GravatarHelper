$projectRoot = Resolve-Path("$PSScriptRoot\..\..\")

$testDirectory = Resolve-Path("$projectRoot\tests\")
$artifactDirectory = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("$projectRoot\artifacts\")

$packageOutputDirectory = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("$artifactDirectory\packages\")