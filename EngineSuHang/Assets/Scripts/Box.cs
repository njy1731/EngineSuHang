using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    //[ContextMenu("Open")]
    [Header("Ω∫≈› ¡ı∞°∑Æ")]
    private int BoxCost = 25;
    private int HpPotion = 15;
    private float DumbBell = 2;
    private float RunningShoes = 0.5f;

    void GetItem()
    {
        int rd = Random.Range(0, 10);

        if (rd < 3)
        {
            PlayerCtrl.Instance.HP += HpPotion;
            Debug.Log("HP");
        }

        else if (rd < 6)
        {
            PlayerCtrl.Instance.STRENGTH += DumbBell;
            Debug.Log("strength");

        }

        else 
        {
            PlayerCtrl.Instance.SPEED += RunningShoes;
            Debug.Log("speed");
        }
    }

    public void Open()
    {
        PlayerCtrl.Instance.Coin -= BoxCost;
        GetItem();
        Debug.Log("Get Item!");
        GetComponent<Animator>().Play("BoxClip");
    }
    
    public void EndOpen()
    {
        Destroy(gameObject);
    }
}
