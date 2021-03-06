using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("카메라 기본 속성")]
    //카메라 위치 캐싱 준비
    private Transform cameraTransform = null;

    //타겟
    public GameObject objTarget = null;

    //플레이어 Transform 캐싱
    private Transform objTargetTransform = null;

    //Camera 3
    public enum CameraTypeState { First, Second, Third }

    //카메라 기본타입 3인칭
    public CameraTypeState cameraState = CameraTypeState.Third;

    [Header("3인칭 카메라")]
    //떨어진 거리
    public float distance = 6.0f;

    //추가 높이 
    public float height = 1.75f;

    //smooth Time
    public float heightDamping = 2.0f;
    public float rotationDamping = 3f;

    [Header("2인칭 카메라")]
    public float rotationSpd = 10f;

    [Header("1인칭 카메라")]
    //마우스로 카메라 조절 디테일 좌표
    public float detailX = 5f;
    public float detailY = 5f;

    //마우스 회전값
    private float rotationX = 0f;
    private float rotationY = 0f;

    //캐싱
    public Transform posFirstTarget = null;

    /// <summary>
    /// 1인칭 카메라 조작
    /// </summary>
    void FirstCamera()
    {
        //마우스 좌표값 가져오기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //카메라 회전 값
        rotationX = cameraTransform.localEulerAngles.y + mouseX * detailX;
        rotationX = (rotationX > 180.0f) ? rotationX - 360.0f : rotationX;
        rotationY = rotationY + mouseY * detailY;
        rotationY = (rotationY > 180.0f) ? rotationY - 360.0f : rotationY;

        cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f);
        cameraTransform.position = posFirstTarget.position;
    }

    /// <summary>
    /// 2인칭 카메라 조작
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
        //타겟이 있나?
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
    /// 3인칭 카메라 기본 동작 함수
    /// </summary>
    void ThirdCamera()
    {
        //현재 타겟 Y축 각도 값
        float objTargetRotationAngle = objTargetTransform.eulerAngles.y;

        //현재 타겟 높이 + 카메라가 위치한 높이 추가
        float objHeight = objTargetTransform.position.y + height;

        //현재 각도 높이
        float nowRotationAngle = cameraTransform.eulerAngles.y;
        float nowHeight = cameraTransform.position.y;

        //현재 각도의 대한 DAMP
        nowRotationAngle = Mathf.LerpAngle(nowRotationAngle, objTargetRotationAngle, rotationDamping * Time.deltaTime);

        //현재 높이에 대한 DAMP
        nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamping * Time.deltaTime);

        //유니티 각도로 변환
        Quaternion nowRotation = Quaternion.Euler(0f, nowRotationAngle, 0f);

        //카메라 위치 포지션 이동
        cameraTransform.position = objTargetTransform.position;
        cameraTransform.position -= nowRotation * Vector3.forward * distance;

        //최종 이동
        cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);

        cameraTransform.LookAt(objTargetTransform);
    }
}
