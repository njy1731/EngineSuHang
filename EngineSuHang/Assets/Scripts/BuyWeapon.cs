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
    
    public void OnDagger()
    {
        if (isDagger == false)
        {
            if (PlayerCtrl.Instance.Coin >= DaggerPrice)
            {
                daggerText.text = string.Format("Sold");
                PlayerCtrl.Instance.STRENGTH += DaggerDMG;
                PlayerCtrl.Instance.SPEED += DaggerStat;
                PlayerCtrl.Instance.Coin -= DaggerPrice;
                daggerButton.interactable = false;
                isDagger = true;
            }
        }
    }

    public void OnKatana()
    {
        if (isKatana == false)
        {
            if (PlayerCtrl.Instance.Coin >= KatanaPrice)
            {
                katanaText.text = string.Format("Sold");
                PlayerCtrl.Instance.STRENGTH += KatanaDMG;
                PlayerCtrl.Instance.HP += KatanaStat;
                PlayerCtrl.Instance.Coin -= KatanaPrice;
                katanaButton.interactable = false;
                isKatana = true;
            }
        }
    }

    public void OnGreatSword()
    {
        if (isGreatSword == false)
        {
            if (PlayerCtrl.Instance.Coin >= GreatSwordPrice)
            {
                greatswordText.text = string.Format("Sold");
                PlayerCtrl.Instance.STRENGTH += GreatSwordDMG;
                PlayerCtrl.Instance.Coin -= GreatSwordPrice;
                greatswordButton.interactable = false;
                isGreatSword = true;
            }
        }
    }

    public void OnTomahawk()
    {
        if (isTomahawk == false)
        {
            if (PlayerCtrl.Instance.Coin >= TomahawkPrice)
            {
                tomahawkText.text = string.Format("Sold");
                PlayerCtrl.Instance.STRENGTH += TomahawkDMG;
                PlayerCtrl.Instance.HP += TomahawkStat1;
                PlayerCtrl.Instance.SPEED += TomahawkStat2;
                PlayerCtrl.Instance.Coin -= TomahawkPrice;
                tomahawkButton.interactable = false;
                isTomahawk = true;
            }
        }
    }

    public void OnZweihander()
    {
        if (isZweihander == false)
        {
            
            if (PlayerCtrl.Instance.Coin >= ZweihanderPrice)
            {
                zweihanderText.text = string.Format("Sold");
                PlayerCtrl.Instance.STRENGTH += ZweihanderDMG;
                PlayerCtrl.Instance.HP += ZweihanderStat;
                PlayerCtrl.Instance.Coin -= ZweihanderPrice;
                zweihanderButton.interactable = false;
                isZweihander = true;
            }
        }
    }
}
