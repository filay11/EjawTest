using System.IO;
using UnityEngine;

public static class LoadJSON
{
    static public T LoadingJSON<T>(string path)
    {
        if (File.Exists(path))
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }
        else return default(T);
    }    
}
