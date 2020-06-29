using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PopulateList : MonoBehaviour
{
    public GameObject prefab;
    AssetBundle cubeBundle;
    AssetBundle rectBundle;
    bool asset_bundles_loaded = false;
    bool calledOnce = false;

    Object[] cubes;
    Object[] rects;

    Dictionary<string, Object[]> Kategories = new Dictionary<string, Object[]>();
    List<UnityWebRequest> UnityWebRequests = new List<UnityWebRequest>();

    void Start()
    {
        StartCoroutine(load_asset_bundles());
    }
    void Update()
    {
        foreach(UnityWebRequest request in UnityWebRequests)
        {
            if (!request.isDone)
                break;

            asset_bundles_loaded = true;
        }
        // Wait for all asset bundles to be loaded
        if (asset_bundles_loaded && !calledOnce)
        {
            // Once loaded use the assets in asset bundles
            // To get the data in  a bundle, you must do your_bundle.LoadAsset<type_of_your_asset>("name_of_the_asset_case_sensitive");
            //GameObject asset_loaded_prefab = (GameObject)cubeBundle.LoadAsset<GameObject>("Hammer");
            //GameObject instance = (GameObject)GameObject.Instantiate(asset_loaded_prefab);

            cubes = cubeBundle.LoadAllAssets();
            rects = rectBundle.LoadAllAssets();

            Kategories.Add("Cubes", cubes);
            Kategories.Add("Rects", rects);


            Populate();

            calledOnce = true;
        }
    }
    IEnumerator load_asset_bundles()
    {
        // Reuse this line to load different asset bundles of different names
        StartCoroutine(load_sub_asset_bundle("cube"));
        StartCoroutine(load_sub_asset_bundle("rect"));

        yield return true;
    }

    IEnumerator load_sub_asset_bundle(string bundle_name)
    {
        // This coroutine loads a single asset bundle at a time
        string uri;
        string path_to_use;

#if UNITY_ANDROID && !UNITY_EDITOR
             // This is the path to require an asset bundle in Assets/StreamingAssets on Android
             path_to_use = Path.Combine("jar:file://" + Application.dataPath + "!assets/", bundle_name);
             uri = path_to_use;
#else
        // This is the same path but for your computer to recognize
        path_to_use = Application.dataPath;
        uri = path_to_use + "/" + bundle_name;
#endif

        // Ask for the bundle
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        UnityWebRequests.Add(request);
        yield return request.SendWebRequest();
        switch (bundle_name)
        {
            case "cube":
                cubeBundle = DownloadHandlerAssetBundle.GetContent(request);
                break;
            case "rect":
                rectBundle = DownloadHandlerAssetBundle.GetContent(request);
                break;
            default:
                break;
        }
    }
    public void Populate()
    {
        GameObject newObj;

        foreach(KeyValuePair<string,Object[]> pair in Kategories)
        {
            newObj = Instantiate(prefab,transform);
            newObj.GetComponent<Kategorie>().Name = pair.Key;
            newObj.GetComponent<Kategorie>().gameObjects = pair.Value;

            GameObject go = newObj.GetComponent<Kategorie>().gameObjects[0] as GameObject;
            Sprite mySprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(go.transform,128,128),new Rect(.0f,.0f, 128, 128),new Vector2(.5f,.5f),100.0f);
            newObj.GetComponent<Kategorie>().Image.sprite = mySprite;
        }
    }
}
