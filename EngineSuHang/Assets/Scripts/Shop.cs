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
        //PlayerCtrl.Instance.HpText.gameObject.SetActive(false);
        //PlayerCtrl.Instance.StrengthText.gameObject.SetActive(false);
        //PlayerCtrl.Instance.SpeedText.gameObject.SetActive(false);
        UIShop.gameObject.SetActive(true);
        if(isShop == true)
        Time.timeScale = 0;
    }

    public void OnClickESCButton()
    {
        isShop = false;
        //PlayerCtrl.Instance.HpText.gameObject.SetActive(true);
        //PlayerCtrl.Instance.StrengthText.gameObject.SetActive(true);
        //PlayerCtrl.Instance.SpeedText.gameObject.SetActive(true);
        UIShop.gameObject.SetActive(false);
        if(isShop == false)
        Time.timeScale = 1;
    }
}
