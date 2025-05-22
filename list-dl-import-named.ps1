$repo_folder_root = "./projects/p"
[int]$counter = 0

foreach ($repo_url in Get-Content ./repo-list-v1.txt) {
  if ($repo_url -match $regex) {
    $counter += 1
    if(!$repo_url.StartsWith("#")){
      $name = $repo_url.Split("/")[-1]
      $padCounter = '{0:d3}' -f [int]$counter
      Write-Output "Preparing $counter : cloning $repo_url into $repo_folder_root$padCounter--$name..."
      git clone --depth=1 $repo_url $repo_folder_root$padCounter--$name
      Write-Output "Cloning done. Importing..."
      C:\UnityEditor\2022.3.37f1\Editor\Unity.exe -projectpath ./$repo_folder_root$padCounter--$name -batchmode -quit -logfile - | Write-Output
      Write-Output "Assets imported. Moving to next repo..."
    }
  }
}