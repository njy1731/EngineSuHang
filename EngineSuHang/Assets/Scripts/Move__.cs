using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move__ : MonoBehaviour
{
    private float inputX, inputY;
    private Vector3 inputDirection;
    [SerializeField]
    private float speed = 5, rotationSpeed = 5;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        inputX = Input.GetAxisRaw("Vertical");
        inputY = Input.GetAxisRaw("Horizontal");
        inputDirection = new Vector3(inputX, 0f, inputY);

        Vector3.Normalize(inputDirection);

        Quaternion q_rotation = Quaternion.Euler(0f, Mathf.Atan2(inputDirection.z, inputDirection.x) * Mathf.Rad2Deg, 0f);

        Vector3 euler = q_rotation.eulerAngles;

        if (inputDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, q_rotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
