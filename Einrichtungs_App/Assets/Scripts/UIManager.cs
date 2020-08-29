using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

//logic für die UI
public class UIManager : MonoBehaviour
{
    public GameObject KategoriesUI;
    public GameObject KategorieInfo;
    public GameObject MenuButton;
    public GameObject PlaceButton;
    public GameObject ButtonMenu;
    public GameObject DeselectButton;
    public Text SelectedItemText;
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
        if (!PlacementIndicator.intersected && !PlacementIndicator.selected)
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
        //Button Menu
        
        if(PlacementIndicator.selected && !ButtonMenu.activeSelf)
        {
            Debug.Log("Selected");
            ButtonMenu.SetActive(true);
            SelectedItemText.text = PlacementIndicator.lastSelectedObject.name;
        }
        else if(!PlacementIndicator.selected && ButtonMenu.activeSelf)
        {
            Debug.Log("Not Selected");
            ButtonMenu.SetActive(false);
        }
        
    }
    public void OnClickDeselect()
    {
        PlacementIndicator.selected = false;
        PlacementIndicator.lastSelectedObject = null;     
    }
}
