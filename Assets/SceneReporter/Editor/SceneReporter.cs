using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Text;
using UnityEditor.SearchService;
using System.IO;
using UnityEngine.UI;
using System;


[ExecuteInEditMode]
public class SceneReporter
{
    static SceneReporter()
    {
        Debug.Log("Creating report on startup");
        CreateReport();
    }

    public static void CreateReport()
    {
        var projectPath = Directory.GetParent(Application.dataPath);
        var buildScenes = EditorBuildSettings.scenes;
        var report = new StringBuilder();
        int sceneBuildIndex = 0;

        report.Append("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n<meta charset=\"UTF-8\" />\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n<title>Scenes report for "+ projectPath+"</title>\r\n<style>body {\r\nfont-family: system-ui, -apple-system, BlinkMacSystemFont, \"Segoe UI\", Roboto, Oxygen, Ubuntu, Cantarell, \"Open Sans\", \"Helvetica Neue\", sans-serif;\r\nbackground-color: #F2F0E5;\r\ncolor: #100F0F;\r\n}\r\n.comp {\r\nbackground-color: rgb(223, 180, 49);\r\npadding: 3px;\r\nmargin: 3px;\r\nborder-radius: 2px;\r\n}\r\nli {\r\nmargin: 12px auto;\r\n} .go_name{font-weight:700;}</style>\r\n</head><body>");
        report.Append($"<h1>{projectPath}</h1>");

        foreach (var scene in buildScenes)
        {
            UnityEngine.SceneManagement.Scene openedScene = EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);
            report.Append($"<h2>[Scene {sceneBuildIndex}] {scene.path.Replace("/", "\\")}</h2>");
            var rootGos = openedScene.GetRootGameObjects().ToList();
            RecursiveListGOs(rootGos, report);
            EditorSceneManager.CloseScene(openedScene, false);
        }
        report.Append("</body>\r\n</html>");
        Debug.Log(report.ToString());
        File.WriteAllText(Path.Combine(Application.dataPath, "scenes-report.html"), report.ToString());
    }

    public static void RecursiveListGOs(List<GameObject> gos, StringBuilder report)
    {
        report.Append("<ul>");
        foreach (var go in gos)
        {
            if (go.transform.childCount == 0)
            {
                report.Append($"<li><span class='go_name'>{go.name}</span> {GetComponentList(go)}</li>");
            }
            else
            {
                report.Append($"<li><details open><summary><span class='go_name'>{go.name}</span> {GetComponentList(go)}</summary>");
                var childGos = new List<GameObject>();
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    childGos.Add(go.transform.GetChild(i).gameObject);
                }
                RecursiveListGOs(childGos, report);
                report.Append($"</li>");
            }
        }
        report.Append("</ul>");
    }

    public static string GetComponentList(GameObject obj)
    {
        string res = "";
        var count = obj.GetComponentCount();
        if (count == 0)
        {
            return res;
        }
        for (int i = 0; i < count; i++)
        {
            var ft = obj.GetComponentAtIndex(i).GetType().ToString();
            var last = ft.Split(".")[^1];   
            res += $"<span class='comp'>{last}</span>";
        }
        return res;
    }
}


//public static void Test()
//{
//    Debug.Log("mensagem");
//}
// 
//public static string GetGameObjectPath(GameObject obj)
//{
//    string path = "/" + obj.name;
//    while (obj.transform.parent != null)
//    {
//        obj = obj.transform.parent.gameObject;
//        path = "/" + obj.name + path;
//    }
//    return path;
//}
