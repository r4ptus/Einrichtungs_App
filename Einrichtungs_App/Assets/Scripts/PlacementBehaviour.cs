using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//löst das delegate aus
public class PlacementBehaviour : MonoBehaviour
{
    public delegate void OnButtonClickDelegate();
    public static OnButtonClickDelegate buttonClickDelegate;
    
    public void OnButtonClick()
    {
        buttonClickDelegate();
    }

   

}
