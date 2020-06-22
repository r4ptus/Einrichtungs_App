using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject KategoriesUI;
    public GameObject KategorieInfo;
    public GameObject MenuButton;
    public GameObject PlaceButton;

    public Kategorie KategorieClicked;


    public void ShowHideKategorieUI()
    {
        KategoriesUI.SetActive(!KategoriesUI.activeSelf);
    }
    public void ShowHideKategorieInfo()
    {
        KategorieInfo.SetActive(!KategorieInfo.activeSelf);
        if (KategorieInfo.activeSelf)
        {
            PopulateKategorie populateKategorie = KategorieInfo.GetComponentInChildren<PopulateKategorie>();
            populateKategorie.Populate();
        }
        else if (!KategorieInfo.activeSelf)
        {
            PopulateKategorie populateKategorie = KategorieInfo.GetComponentInChildren<PopulateKategorie>();
            populateKategorie.DestroyPrefabs();
        }
            
    }
    public void ShowHideMenuButton()
    {
        MenuButton.SetActive(!KategorieInfo.activeSelf);
    }
    public void ShowHidePlaceButton()
    {
        PlaceButton.SetActive(!KategorieInfo.activeSelf);
    }
}
