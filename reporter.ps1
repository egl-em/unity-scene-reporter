# Example that installs a package with the necessary scripts
#."C:\tools\unity\2022.3.37f1\Editor\Unity.exe" -projectPath "C:\arquivo\10-19 Praticas\11 Projetos\11.20 Repos de empregos\cegep--em\test-empty-project" -importPackage "C:\arquivo\10-19 Praticas\11 Projetos\11.20 Repos de empregos\cegep--em\unity-scene-reporter\SceneReporter.unitypackage" -logFile log.txt -quit
#
# Example without installing the package with the scripts
."C:\tools\unity\2022.3.37f1\Editor\Unity.exe" -projectPath "C:\arquivo\10-19 Praticas\11 Projetos\11.20 Repos de empregos\cegep--em\unity-scene-reporter" -importPackage "C:\arquivo\10-19 Praticas\11 Projetos\11.20 Repos de empregos\cegep--em\unity-scene-reporter\SceneReporter.unitypackage" -executeMethod "SceneReporter.CreateReport" -logFile log.txt -quit