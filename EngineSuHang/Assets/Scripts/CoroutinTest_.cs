using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutinTest_ : MonoBehaviour
{
    public Image img = null;

    void Start()
    {
        img = GetComponent<Image>();
        StartCoroutine(SetFadein());

        //StartCoroutine(hiUnity());
        //StartCoroutine(hiCoroutin());
        //Debug.Log("end");
    }

     IEnumerator SetFadein()
    {
        Color imgColor = img.color;
        for(int i = 0; i < 100; i++)
        {
            imgColor.a = imgColor.a - 0.01f;
            img.color = imgColor;
            yield return new WaitForSeconds(0.01f);
        }
    }

    //IEnumerator hiUnity()
    //{
    //    Debug.LogError("Hi! ");
    //    yield return new WaitForSeconds(1.0f);
    //    Debug.LogError("Unity");
    //}

    //IEnumerator hiCoroutin()
    //{
    //    Debug.LogError("Hi! ");
    //    yield return new WaitForSeconds(1.0f);
    //    Debug.LogError("Coroutin");
    //}
}
