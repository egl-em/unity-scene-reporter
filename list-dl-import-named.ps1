$repo_folder_root = "private_tests/projects/p"
$list_path = "private_tests/repo-list.txt"
$unity_editor_path = "C:\tools\unity\2022.3.37f1\Editor\Unity.exe"
$unitypackage_path = "C:\arquivo\10-19 Praticas\11 Projetos\11.20 Repos de empregos\cegep--em\unity-scene-reporter\SceneReporter.unitypackage"
$curDir = (Get-Location).Path

[int]$counter = 0

foreach ($repo_url in Get-Content $list_path ) {
  if ($repo_url -match $regex) {
    $counter += 1
    if (!$repo_url.StartsWith("#")) {
      $name = $repo_url.Split("/")[-1]
      $padCounter = '{0:d3}' -f [int]$counter
      Write-Output "[USC-LIST] Preparing $counter repo : cloning $repo_url into $repo_folder_root$padCounter--$name..."
      git clone --depth=1 $repo_url $repo_folder_root$padCounter--$name
      Write-Output "[USC-LIST] Cloning done. Building Library and importing reporter..."
      .$unity_editor_path -batchmode -projectpath ./$repo_folder_root$padCounter--$name -importPackage $unitypackage_path -quit -logfile "log-import.txt" | Write-Output
      Write-Output "[USC-LIST] Library built and reporter imported. Creating report..."
      .$unity_editor_path -batchmode -projectpath ./$repo_folder_root$padCounter--$name -executeMethod "SceneReporter.CreateReport" -quit -logfile "log-report.txt" | Write-Output
      Write-Output "[USC-LIST] Scene report created."
    }
  }
}