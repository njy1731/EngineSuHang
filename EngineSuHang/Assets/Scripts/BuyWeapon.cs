using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    private int DaggerDMG = 4;
    private int KatanaDMG = 6;
    private int GreatSwordDMG = 8;
    private int TomahawkDMG = 10;
    private int ZweihanderDMG = 13;

    [Header("무기 추가 능력치")]
    private float DaggerStat = 0.5f;
    private int KatanaStat = 15;
    private int TomahawkStat1 = 10;
    private float TomahawkStat2 = 0.7f;
    private int ZweihanderStat = 20;

    [Header("무기 가격")]
    private int DaggerPrice = 85;
    private int KatanaPrice = 120;
    private int GreatSwordPrice = 150;
    private int TomahawkPrice = 190;
    private int ZweihanderPrice = 225;

    [Header("무기 체크")]
    private bool isDagger = false;
    private bool isKatana = false;
    private bool isGreatSword = false;
    private bool isTomahawk = false;
    private bool isZweihander = false;
    private bool isPurchase = false;
    private bool isExit = false;

    public Image Purchase = null;

    public void OnPurchase()
    {
        if (isDagger)
        {
            if (PlayerCtrl.Instance.Coin >= DaggerPrice)
            {
                Purchase.gameObject.SetActive(false);
                PlayerCtrl.Instance.STRENGTH += DaggerDMG;
                PlayerCtrl.Instance.SPEED += DaggerStat;
                PlayerCtrl.Instance.Coin -= DaggerPrice;
                katanaButton.interactable = true;
                greatswordButton.interactable = true;
                tomahawkButton.interactable = true;
                zweihanderButton.interactable = true;
                isDagger = true;
            }
        }

        if (isKatana)
        {
            if (PlayerCtrl.Instance.Coin >= KatanaPrice)
            {
                Purchase.gameObject.SetActive(false);
                PlayerCtrl.Instance.STRENGTH += KatanaDMG;
                PlayerCtrl.Instance.HP += KatanaStat;
                PlayerCtrl.Instance.Coin -= KatanaPrice;
                daggerButton.interactable = true;
                greatswordButton.interactable = true;
                tomahawkButton.interactable = true;
                zweihanderButton.interactable = true;
                isKatana = true;
            }
        }

        if (isGreatSword)
        {
            if (PlayerCtrl.Instance.Coin >= GreatSwordPrice)
            {
                Purchase.gameObject.SetActive(false);
                PlayerCtrl.Instance.STRENGTH += GreatSwordDMG;
                PlayerCtrl.Instance.Coin -= GreatSwordPrice;
                daggerButton.interactable = true;
                katanaButton.interactable = true;
                tomahawkButton.interactable = true;
                zweihanderButton.interactable = true;
                isGreatSword = true;
            }
        }

        if (isTomahawk)
        {
            if (PlayerCtrl.Instance.Coin >= TomahawkPrice)
            {
                Purchase.gameObject.SetActive(false);
                PlayerCtrl.Instance.STRENGTH += TomahawkDMG;
                PlayerCtrl.Instance.HP += TomahawkStat1;
                PlayerCtrl.Instance.SPEED += TomahawkStat2;
                PlayerCtrl.Instance.Coin -= TomahawkPrice;
                daggerButton.interactable = true;
                katanaButton.interactable = true;
                greatswordButton.interactable = true;
                zweihanderButton.interactable = true;
                isTomahawk = true;
            }
        }

        if (isZweihander)
        {
            if (PlayerCtrl.Instance.Coin >= ZweihanderPrice)
            {
                Purchase.gameObject.SetActive(false);
                PlayerCtrl.Instance.STRENGTH += ZweihanderDMG;
                PlayerCtrl.Instance.HP += ZweihanderStat;
                PlayerCtrl.Instance.Coin -= ZweihanderPrice;
                daggerButton.interactable = true;
                katanaButton.interactable = true;
                greatswordButton.interactable = true;
                tomahawkButton.interactable = true;
                isZweihander = true;
            }
        }
    }

    public void OnExitButton()
    {
        Purchase.gameObject.SetActive(false);
        isPurchase = false;
        if (isDagger)
        {
            isDagger = false;
        }
        if (isKatana)
        {
            isKatana = false;
        }
        if (isGreatSword)
        {
            isGreatSword = false;
        }
        if (isTomahawk)
        {
            isTomahawk = false;
        }
        if (isZweihander)
        {
            isZweihander = false;
        }
    }
    
    public void OnDagger()
    {
        if (isDagger == false)
        {
            katanaButton.interactable = false;
            greatswordButton.interactable = false;
            tomahawkButton.interactable = false;
            zweihanderButton.interactable = false;
            if (PlayerCtrl.Instance.Coin >= DaggerPrice)
            {
                Purchase.gameObject.SetActive(true);
                isPurchase = true;
                isDagger = true;
            }
            
        }

        else if (isDagger == true)
        {
            daggerButton.interactable = false;
            daggerText.text = string.Format("Sold");
        }
    }

    public void OnKatana()
    {
        if (isKatana == false)
        {
            daggerButton.interactable = false;
            greatswordButton.interactable = false;
            tomahawkButton.interactable = false;
            zweihanderButton.interactable = false;
            if (PlayerCtrl.Instance.Coin >= KatanaPrice)
            {
                Purchase.gameObject.SetActive(true);
                isPurchase = true;
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
            daggerButton.interactable = false;
            katanaButton.interactable = false;
            tomahawkButton.interactable = false;
            zweihanderButton.interactable = false;
            if (PlayerCtrl.Instance.Coin >= GreatSwordPrice)
            {
                Purchase.gameObject.SetActive(true);
                isPurchase = true;
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
            daggerButton.interactable = false;
            katanaButton.interactable = false;
            greatswordButton.interactable = false;
            zweihanderButton.interactable = false;
            if (PlayerCtrl.Instance.Coin >= TomahawkPrice)
            {
                Purchase.gameObject.SetActive(true);
                isPurchase = true;
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
            daggerButton.interactable = false;
            katanaButton.interactable = false;
            greatswordButton.interactable = false;
            tomahawkButton.interactable = false;
            if (PlayerCtrl.Instance.Coin >= ZweihanderPrice)
            {
                Purchase.gameObject.SetActive(true);
                isPurchase = true;
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
