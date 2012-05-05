param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$appstart = $path::Combine($path::GetDirectoryName($project.FileName), "w3c\p3p.xml")
$DTE.ItemOperations.OpenFile($appstart)