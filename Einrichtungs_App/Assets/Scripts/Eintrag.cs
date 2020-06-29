using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Eintrag : MonoBehaviour
{
    public Image Image;
    public GameObject GameObject;
    public TextMeshProUGUI tmpName;
    public TextMeshProUGUI tmpWidth;
    public TextMeshProUGUI tmpHeight;
    public TextMeshProUGUI tmpDepth;
    public string Name;
    public float Width;
    public float Heigth;
    public float Depth;
    // Start is called before the first frame update
    void Awake()
    {
        tmpName.SetText(Name);
        tmpWidth.SetText(Width.ToString());
        tmpHeight.SetText(Heigth.ToString());
        tmpDepth.SetText(Depth.ToString());
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClickListener);
    }

    public void OnClickListener()
    {
        UIManager main = GameObject.Find("Canvas").GetComponent<UIManager>();
        main.ShowHideKategorieInfo();
        main.ShowHideMenuButton();
        FindObjectOfType<ARAnchorManager>().anchorPrefab = GameObject;
        FindObjectOfType<PlacementIndicator>().gameObject.transform.GetChild(0).localScale = new Vector3(GameObject.transform.localScale.x, 0, GameObject.transform.localScale.y);
    
    }

    // Update is called once per frame
    void Update()
    {
        tmpName.SetText(Name);
        tmpWidth.SetText(Width.ToString());
        tmpHeight.SetText(Heigth.ToString());
        tmpDepth.SetText(Depth.ToString());
    }
}
