using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UIManager : MonoBehaviour
{
    public GameObject KategoriesUI;
    public GameObject KategorieInfo;
    public GameObject MenuButton;
    public GameObject PlaceButton;
    public ARAnchorManager ARAnchorManager;

    public Kategorie KategorieClicked;
    public PlacementIndicator PlacementIndicator;

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
    private void Update()
    {
        if (!PlacementIndicator.intersected)
        {
            if (ARAnchorManager.anchorPrefab != null
           && !PlaceButton.activeSelf
           && !KategorieInfo.activeSelf
           && !KategoriesUI.activeSelf)
            {
                PlaceButton.SetActive(true);
            }
        }
        else
        {
            PlaceButton.SetActive(false);
        }
        
       
    }
}
