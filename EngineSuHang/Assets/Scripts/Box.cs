using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    //[ContextMenu("Open")]

    public void Open()
    {
        GetComponent<Animator>().Play("BoxClip");
    }
    
    public void EndOpen()
    {
        Destroy(gameObject);
    }
}
