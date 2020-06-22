using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Kategorie : MonoBehaviour
{
    public string Name = "Kategorie";
    public Image Image;
    public TextMeshProUGUI Text;

    public Object[] gameObjects;


    private void Start()
    {
        Text.SetText(Name);
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickListener);
    }
    public void OnClickListener()
    {
        UIManager main = GameObject.Find("Canvas").GetComponent<UIManager>();
        main.ShowHideKategorieUI();
        main.KategorieClicked = this;
        main.ShowHideKategorieInfo();

        Debug.Log(this.Name);
    }
    private void Update()
    {
        Text.SetText(Name);
    }
}
