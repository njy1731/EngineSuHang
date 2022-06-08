using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public int m_cnt
    {
        get
        {
            Object[] objs = FindObjectsOfType<Object>();
            return objs.Length;
        }
    }
}