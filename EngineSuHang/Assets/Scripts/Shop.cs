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
        UIShop.gameObject.SetActive(true);
        Time.timeScale = 0;
        isShop = true;
    }

    public void OnClickESCButton()
    {
        UIShop.gameObject.SetActive(false);
        Time.timeScale = 1;
        isShop = false;
    }
}
