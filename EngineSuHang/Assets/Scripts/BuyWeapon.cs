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
    private PlayerCtrl player;

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

    void Start()
    {
        player = Obj.GetComponent<PlayerCtrl>();
    }

    public void OnDagger()
    {
        if (isDagger == false)
        {
            if (player.Coin >= DaggerPrice)
            {
                player.STRENGTH += DaggerDMG;
                player.Coin -= DaggerPrice;
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
            if (player.Coin >= KatanaPrice)
            {
                player.STRENGTH += KatanaDMG;
                player.Coin -= KatanaPrice;
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
            if (player.Coin >= GreatSwordPrice)
            {
                player.STRENGTH += GreatSwordDMG;
                player.Coin -= GreatSwordPrice;
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
            if (player.Coin >= TomahawkPrice)
            {
                player.STRENGTH += TomahawkDMG;
                player.Coin -= TomahawkPrice;
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
            if (player.Coin >= ZweihanderPrice)
            {
                player.STRENGTH += ZweihanderDMG;
                player.Coin -= ZweihanderPrice;
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
