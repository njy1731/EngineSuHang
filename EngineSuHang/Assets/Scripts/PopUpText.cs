using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public GameObject PopText;

    public void POPup()
    {
        PopText.gameObject.SetActive(true);
    }
}
