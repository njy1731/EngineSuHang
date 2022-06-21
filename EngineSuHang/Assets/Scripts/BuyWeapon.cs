using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWeapon : MonoBehaviour
{
    [Header("무기 구매 버튼")]
    [SerializeField]
    private Button daggerButton;
    [SerializeField]
    private Button katanaButton;
    [SerializeField]
    private Button greatswordButton;
    [SerializeField]
    private Button tomahawkButton;
    [SerializeField]
    private Button zweihanderButton;

    [Header("무기 구매 텍스트")]
    [SerializeField]
    private Text daggerText;
    [SerializeField]
    private Text katanaText;
    [SerializeField]
    private Text greatswordText;
    [SerializeField]
    private Text tomahawkText;
    [SerializeField]
    private Text zweihanderText;

    [Header("Default")]
    [SerializeField]
    private GameObject Obj;

    [Header("무기 공격력")]
    private int DaggerDMG = 8;
    private int KatanaDMG = 12;
    private int GreatSwordDMG = 15;
    private int TomahawkDMG = 20;
    private int ZweihanderDMG = 25;

    [Header("무기 가격")]
    private int DaggerPrice = 85;
    private int KatanaPrice = 120;
    private int GreatSwordPrice = 155;
    private int TomahawkPrice = 190;
    private int ZweihanderPrice = 225;

    [Header("무기 체크")]
    private bool isDagger = false;
    private bool isKatana = false;
    private bool isGreatSword = false;
    private bool isTomahawk = false;
    private bool isZweihander = false;


    public void OnDagger()
    {
        if (isDagger == false)
        {
            if (PlayerCtrl.Instance.Coin >= DaggerPrice)
            {
                PlayerCtrl.Instance.STRENGTH += DaggerDMG;
                PlayerCtrl.Instance.Coin -= DaggerPrice;
                isDagger = true;
            }
        }

        else
        {
            daggerButton.interactable = false;
            daggerText.text = string.Format("Sold");
        }
    }

    public void OnKatana()
    {
        if (isKatana == false)
        {
            if (PlayerCtrl.Instance.Coin >= KatanaPrice)
            {
                PlayerCtrl.Instance.STRENGTH += KatanaDMG;
                PlayerCtrl.Instance.Coin -= KatanaPrice;
                isKatana = true;
            }
        }

        else
        {
            katanaButton.interactable = false;
            katanaText.text = string.Format("Sold");
        }
    }

    public void OnGreatSword()
    {
        if (isGreatSword == false)
        {
            if (PlayerCtrl.Instance.Coin >= GreatSwordPrice)
            {
                PlayerCtrl.Instance.STRENGTH += GreatSwordDMG;
                PlayerCtrl.Instance.Coin -= GreatSwordPrice;
                isGreatSword = true;
            }
        }

        else
        {
            greatswordButton.interactable = false;
            greatswordText.text = string.Format("Sold");
        }
    }

    public void OnTomahawk()
    {
        if (isTomahawk == false)
        {
            if (PlayerCtrl.Instance.Coin >= TomahawkPrice)
            {
                PlayerCtrl.Instance.STRENGTH += TomahawkDMG;
                PlayerCtrl.Instance.Coin -= TomahawkPrice;
                isTomahawk = true;
            }
        }

        else
        {
            tomahawkButton.interactable = false;
            tomahawkText.text = string.Format("Sold");
        }
    }

    public void OnZweihander()
    {
        if (isZweihander == false)
        {
            if (PlayerCtrl.Instance.Coin >= ZweihanderPrice)
            {
                PlayerCtrl.Instance.STRENGTH += ZweihanderDMG;
                PlayerCtrl.Instance.Coin -= ZweihanderPrice;
                isZweihander = true;
            }
        }

        else
        {
            zweihanderButton.interactable = false;
            zweihanderText.text = string.Format("Sold");
        }
    }
}
