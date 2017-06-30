using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ShaderHotReloader
{
    private static string WATCH_PATH = Application.dataPath + "/Resources/Shaders";
    private static string WATCH_EXTENTION = "*.shader";

    private static bool _shouldReload = false;

    static ShaderHotReloader()
    {
        var watcher = new FileSystemWatcher(WATCH_PATH, WATCH_EXTENTION);
        watcher.NotifyFilter = NotifyFilters.LastWrite;
        watcher.IncludeSubdirectories = true;
        watcher.EnableRaisingEvents = true;
        watcher.Changed += delegate
        {
            _shouldReload = true;
        };

        EditorApplication.update += delegate
        {
            if(_shouldReload)
            {
                AssetDatabase.Refresh();
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                _shouldReload = false;
            }
        };
    }
}
