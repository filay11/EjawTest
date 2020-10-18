using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class LoadScript : MonoBehaviour
{
    private string nameOfAssetBundle = "prefabs";
    private string nameOfJSONfile = "prefabNames";
    private string pathToJSONfile;    

    private List<GameObject> prefabs = new List<GameObject>(); 

    [System.Serializable]
    public class Data
    {
        public List<string> PrefabNames = new List<string>();
    }

    [SerializeField]
    Data data;

    
    void Start()
    {   
        pathToJSONfile = Path.Combine(Application.dataPath, $"Resources/{nameOfJSONfile}.json");        
        data = LoadJSON.LoadingJSON<Data>(pathToJSONfile);
                       
        StartCoroutine(LoadAssetsAsyncAndFirstJSONfileInit(nameOfAssetBundle, prefabs, data));        
    }

    private IEnumerator LoadAssetsAsyncAndFirstJSONfileInit(string assetBundleName, List<GameObject> assetsPrefab, Data data)
    {
        string filePath = Path.Combine(Application.dataPath, $"AssetBundles/{assetBundleName}");        
                
        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return assetBundleCreateRequest;

        AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;
        
        AssetBundleRequest asset = assetBundle.LoadAllAssetsAsync<GameObject>();
        yield return asset;

        for (int i = 0; i < asset.allAssets.Length; i++)
        {
            GameObject g = asset.allAssets[i] as GameObject;
            assetsPrefab.Add(g);
            yield return null;
        }

        // first initializing JSONfile
        if (data == null)
        {
            data = new Data();
            foreach (var p in assetsPrefab)
            {
                data.PrefabNames.Add(p.name);
                yield return null;
            }
            SaveJSON.SaveFileJSON(pathToJSONfile, data);
        }

        assetBundle.Unload(false);
    }   

    public void InstantiateRandomPrefab(Vector3 pos)
    {
        string name = data.PrefabNames[Random.Range(0, data.PrefabNames.Count)];
        if (prefabs == null)
        {            
            return;
        }

        foreach (var i in prefabs)
        {
            if (i.name == name)
            {
                GameObject obj = Instantiate(i, pos, Quaternion.identity);
                obj.name = name;

                // load data to geometry object
                var objModel = obj.GetComponent<GeometryObjectModel>();
                objModel.objectData = Resources.Load<GeometryObjectData>($"ObjectsData/{obj.name}/{obj.name}ObjectData");

                if (objModel.objectData.ClicksData.Count == 0)
                {
                    objModel.objectData.ClicksData = new List<ClickColorData>(Resources.LoadAll<ClickColorData>($"ClickColors/{obj.name}"));                    
                }                
                break;
            }            
        }
    }
}
