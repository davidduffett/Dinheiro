param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$projectPath = $path::GetDirectoryName($project.FileName)
$webconfig = $path::Combine($projectPath, "web.config")
$p3pxml = $path::Combine($projectPath, "w3c\p3p.xml")
$DTE.ItemOperations.OpenFile($webconfig)
$DTE.ItemOperations.OpenFile($p3pxml)