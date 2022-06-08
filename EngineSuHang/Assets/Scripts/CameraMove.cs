using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("ī�޶� �⺻ �Ӽ�")]
    //ī�޶� ��ġ ĳ�� �غ�
    private Transform cameraTransform = null;

    //Ÿ��
    public GameObject objTarget = null;

    //�÷��̾� Transform ĳ��
    private Transform objTargetTransform = null;

    //Camera 3
    public enum CameraTypeState { First, Second, Third }

    //ī�޶� �⺻Ÿ�� 3��Ī
    public CameraTypeState cameraState = CameraTypeState.Third;

    [Header("3��Ī ī�޶�")]
    //������ �Ÿ�
    public float distance = 6.0f;

    //�߰� ���� 
    public float height = 1.75f;

    //smooth Time
    public float heightDamping = 2.0f;
    public float rotationDamping = 3f;

    [Header("2��Ī ī�޶�")]
    public float rotationSpd = 10f;

    [Header("1��Ī ī�޶�")]
    //���콺�� ī�޶� ���� ������ ��ǥ
    public float detailX = 5f;
    public float detailY = 5f;

    //���콺 ȸ����
    private float rotationX = 0f;
    private float rotationY = 0f;

    //ĳ��
    public Transform posFirstTarget = null;

    /// <summary>
    /// 1��Ī ī�޶� ����
    /// </summary>
    void FirstCamera()
    {
        //���콺 ��ǥ�� ��������
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //ī�޶� ȸ�� ��
        rotationX = cameraTransform.localEulerAngles.y + mouseX * detailX;
        rotationX = (rotationX > 180.0f) ? rotationX - 360.0f : rotationX;
        rotationY = rotationY + mouseY * detailY;
        rotationY = (rotationY > 180.0f) ? rotationY - 360.0f : rotationY;

        cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f);
        cameraTransform.position = posFirstTarget.position;
    }

    /// <summary>
    /// 2��Ī ī�޶� ����
    /// </summary>
    void SecondCamera()
    {
        cameraTransform.RotateAround(objTargetTransform.position, Vector3.up, rotationSpd * Time.deltaTime);

        cameraTransform.LookAt(objTargetTransform);
    }

    private void LateUpdate()
    {
        if (objTarget == null)
        {
            return;
        }

        if (objTargetTransform == null)
        {
            objTargetTransform = objTarget.transform;
        }

        switch (cameraState)
        {
            case CameraTypeState.First:
                FirstCamera();
                break;
            case CameraTypeState.Second:
                SecondCamera();
                break;
            case CameraTypeState.Third:
                ThirdCamera();
                break;
            default:

                break;

        }
    }

    void Start()
    {
        //Ÿ���� �ֳ�?
        cameraTransform = GetComponent<Transform>();

        if (objTarget != null)
        {
            objTargetTransform = objTarget.transform;
        }
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 3��Ī ī�޶� �⺻ ���� �Լ�
    /// </summary>
    void ThirdCamera()
    {
        //���� Ÿ�� Y�� ���� ��
        float objTargetRotationAngle = objTargetTransform.eulerAngles.y;

        //���� Ÿ�� ���� + ī�޶� ��ġ�� ���� �߰�
        float objHeight = objTargetTransform.position.y + height;

        //���� ���� ����
        float nowRotationAngle = cameraTransform.eulerAngles.y;
        float nowHeight = cameraTransform.position.y;

        //���� ������ ���� DAMP
        nowRotationAngle = Mathf.LerpAngle(nowRotationAngle, objTargetRotationAngle, rotationDamping * Time.deltaTime);

        //���� ���̿� ���� DAMP
        nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamping * Time.deltaTime);

        //����Ƽ ������ ��ȯ
        Quaternion nowRotation = Quaternion.Euler(0f, nowRotationAngle, 0f);

        //ī�޶� ��ġ ������ �̵�
        cameraTransform.position = objTargetTransform.position;
        cameraTransform.position -= nowRotation * Vector3.forward * distance;

        //���� �̵�
        cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);

        cameraTransform.LookAt(objTargetTransform);
    }
}
