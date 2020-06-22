using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PopulateList : MonoBehaviour
{
    public GameObject prefab;
    string[] directories;
    string ResourcesPath;

    void Awake()
    {
        ResourcesPath = Application.dataPath + "/Resources";
        directories = Directory.GetDirectories(ResourcesPath, "*", SearchOption.AllDirectories);

        Populate();
    }
    void Update()
    {
        
    }
    public void Populate()
    {
        GameObject newObj;

        for(int i = 0; i < directories.Length; i++)
        {
            newObj = Instantiate(prefab,transform);
            newObj.GetComponent<Kategorie>().Name = directories[i].Replace(ResourcesPath+"\\","");
            newObj.GetComponent<Kategorie>().gameObjects = Resources.LoadAll(newObj.GetComponent<Kategorie>().Name);
            GameObject go = newObj.GetComponent<Kategorie>().gameObjects[0] as GameObject;
            Sprite mySprite = Sprite.Create(AssetPreview.GetAssetPreview(go),new Rect(.0f,.0f, AssetPreview.GetMiniThumbnail(go).width, AssetPreview.GetMiniThumbnail(go).height),new Vector2(.5f,.5f),100.0f);
            newObj.GetComponent<Kategorie>().Image.sprite = mySprite;
        }
    }
}
