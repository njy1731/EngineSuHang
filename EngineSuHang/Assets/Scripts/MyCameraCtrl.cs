using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraCtrl : MonoBehaviour
{
    Vector3 reverseDistance;
    public GameObject obj;
    //�̰� �뵵 �𸣰���
    protected Vector3 velocity = Vector3.zero;

    [Header("�⺻��ġ")]
    [Range(0, 1)]
    public float SmoothTime = 0.1f; // ī�޶� �ε巴�� ������ �ð�
    float xmove, ymove;
    public float distance = 3;
    protected float mouse;

    void Update()
    {
        Ioaa();
        SmoothCamera(distance, obj);
    }

    /// <summary>
    /// �̰� �߿���
    /// </summary>
    /// <param name="pos">���� ���Ͻ���</param>
    /// <param name="obj">�ٶ󺼰�ü��</param>
    protected void SmoothCamera(float pos, GameObject obj)
    {
        // ī�޶� 3��Ī�̸� ������Ʈ�� ���� �������ִµ� �װ� ���� 0,0,0�϶� ���� ��������
        reverseDistance = new Vector3(0.0f, pos / 3, -pos);
        // ���� ���� ( ������ ��, ������, ����?, �ð� )
        transform.position = Vector3.SmoothDamp(transform.position,
                            //���⼭ �����̼ǰ� �� ������ ���ñ⸦ ���ϴ� ���� -> �� ������ ���ư��� �׶� �׸�ŭ �����ӵ� ���ϴ°��� �׳� �ֿ�� �����ϵ簡
                            obj.transform.position + (transform.rotation * reverseDistance),
                            ref velocity, SmoothTime);
        //obj.transform.position + ((transform.rotation * reverseDistance) * plus);
    }

    /// <summary>
    /// ���� ������
    /// </summary>
    public void Ioaa()
    {
        mouse = Input.GetAxis("Mouse ScrollWheel");
        xmove += Input.GetAxis("Mouse X");
        ymove -= Input.GetAxis("Mouse Y");
        distance -= mouse;
        transform.rotation = Quaternion.Euler(ymove * 2, xmove * 2, 0);
    }


}
