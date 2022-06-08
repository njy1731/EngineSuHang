using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPutManager : MonoBehaviour
{
    public CountManager count;
    public GameObject cubeObject;

    private void Start()
    {
        Debug.Log(count.m_cnt);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            float x = Random.Range(-4.5f, 4.5f);
            float z = Random.Range(-4.5f, 4.5f);
            Vector3 spawnVector = new Vector3(x, 0f, z);
            Instantiate(cubeObject).transform.position = spawnVector;
            Debug.Log(count.m_cnt);
        }
    }
}