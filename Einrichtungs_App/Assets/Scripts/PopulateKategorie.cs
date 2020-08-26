using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PopulateKategorie : MonoBehaviour
{
    public GameObject prefab;
    public int numberToCreate;

    private Kategorie Kategorie;
    private List<GameObject> InstantiatedObjects = new List<GameObject>();

    void Start()
    {
        //Populate();
    }
    public void DestroyPrefabs()
    {
        foreach(GameObject gameObject in InstantiatedObjects)
        {
            Destroy(gameObject);
        }
        InstantiatedObjects.Clear();
    }

    public void Populate()
    {
        UIManager manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Kategorie = manager.KategorieClicked;

        Debug.Log("Populate Kategorie" + Kategorie.Name);

        GameObject newObj;

        Debug.Log(Kategorie.gameObjects.Length);

        for (int i = 0; i < Kategorie.gameObjects.Length; i++)
        {
            newObj = Instantiate(prefab, transform);
            newObj.GetComponent<Eintrag>().Name = (Kategorie.gameObjects[i] as GameObject).name;
            newObj.GetComponent<Eintrag>().Width = (Kategorie.gameObjects[i] as GameObject).transform.localScale.x;
            newObj.GetComponent<Eintrag>().Heigth = (Kategorie.gameObjects[i] as GameObject).transform.localScale.y;
            newObj.GetComponent<Eintrag>().Depth = (Kategorie.gameObjects[i] as GameObject).transform.localScale.z;
            newObj.GetComponent<Eintrag>().GameObject = Kategorie.gameObjects[i] as GameObject;
            InstantiatedObjects.Add(newObj);
            GameObject go = Kategorie.gameObjects[i] as GameObject;
            Sprite mySprite = Sprite.Create(RuntimePreviewGenerator.GenerateModelPreview(go.transform,128,128), new Rect(.0f, .0f, 128, 128), new Vector2(.5f, .5f), 100.0f);
            newObj.GetComponent<Eintrag>().Image.sprite = mySprite;

        }
    }
}
