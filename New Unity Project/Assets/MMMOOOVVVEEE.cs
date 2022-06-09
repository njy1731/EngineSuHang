using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMOOOVVVEEE : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _angleSmooth = 15f;

    private Rigidbody rigid = null;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hori, 0f, verti).normalized * _speed;

        rigid.velocity = dir;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _angleSmooth * Time.deltaTime);

    }
}