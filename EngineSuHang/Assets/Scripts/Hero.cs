using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero heroKing;

    //���� �̸�
    public string heroName = "";

    //�� ����
    public bool isKing = false;

    private void Start()
    {
        if (isKing)
        {
            heroKing = this;
        }
    }

    private void Update()
    {
        //Debug.Log("My Name is " + heroName + "HeroKing " + heroKing);
    }
}
