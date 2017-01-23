$projectRoot = Resolve-Path("$PSScriptRoot\..\..\")
$packageOutputDirectory = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath("$projectRoot\artifacts\packages\")