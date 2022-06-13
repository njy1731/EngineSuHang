using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    private PlayerCtrl player;
    //[ContextMenu("Open")]
    private int HpPotion = 15;
    private float DumbBell = 2;
    private float RunningShoes = 0.5f;

    void GetItem()
    {
        int rd = Random.Range(0, 10);

        if (rd < 3)
        {
            player.HP += HpPotion;
            Debug.Log("HP");

        }

        else if (rd < 6)
        {
            player.STRENGTH += DumbBell;
            Debug.Log("strength");

        }

        else 
        {
            player.SPEED += RunningShoes;
            Debug.Log("speed");
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerCtrl>();
    }

    public void Open()
    {
        GetItem();
        Debug.Log("Get Item!");
        GetComponent<Animator>().Play("BoxClip");
    }
    
    public void EndOpen()
    {
        Destroy(gameObject);
    }
}
