using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject UIShop = null;
    private bool isShop = false;

    public void OnClickShop()
    {
        isShop = true;
        UIShop.gameObject.SetActive(true);
        if(isShop == true)
        Time.timeScale = 0;
    }

    public void OnClickESCButton()
    {
        isShop = false;
        UIShop.gameObject.SetActive(false);
        if(isShop == false)
        Time.timeScale = 1;
    }
}
