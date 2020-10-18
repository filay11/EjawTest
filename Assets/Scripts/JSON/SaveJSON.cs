using System.IO;
using UnityEngine;

public static class SaveJSON
{
    static public void SaveFileJSON(string path, object obj)
    {
        File.WriteAllText(path, JsonUtility.ToJson(obj));
    }    
}
