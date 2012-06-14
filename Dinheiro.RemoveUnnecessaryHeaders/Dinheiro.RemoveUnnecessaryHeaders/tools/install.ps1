param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$projectPath = $path::GetDirectoryName($project.FileName)
$webconfig = $path::Combine($projectPath, "web.config")
$startup = $path::Combine($projectPath, "App_Start\RemoveUnnecessaryHeaders.cs")
$DTE.ItemOperations.OpenFile($webconfig)
$DTE.ItemOperations.OpenFile($startup)