using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTest : MonoBehaviour
{
    //타겟
    public Transform objTarget = null;

    //트랙킹 타임
    public float smoothTime = 0.2f;

    //마지막 속도가 필요
    private Vector3 VeclastMoveSpd;

    //목표 위치
    private Vector3 PosObjTarget;

    //카메라
    private Camera cam;

    //카메라 3가지 모드
    public enum ZoomState { zoomIn, zoomOut, tracking }

    //줌 변수
    private float zoomSize = 5f;

    //줌 상태 값
    private const float zoomOut = 14.5f;
    private const float zoomIn = 5f;
    private const float tracking = 10f;

    //마지막 줌인 속도
    private float lastZoomSpd;

    //zoomState = ZoomState.zoomIn;
    private ZoomState zoomState 
    {
        set
        {
            switch (value)
            {
                case ZoomState.zoomOut:
                    zoomSize = zoomOut; 
                    break;
                case ZoomState.zoomIn:
                    zoomSize = zoomIn;
                    break;
                case ZoomState.tracking:
                    zoomSize = tracking;
                    break;
                default:

                    break;
            }
        }
    }

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (zoomSize)
            {
                case zoomOut:
                    zoomState = ZoomState.zoomIn;
                    break;
                case zoomIn:
                    zoomState = ZoomState.tracking;
                    break;
                case tracking:
                    zoomState = ZoomState.zoomOut;
                    break;
            }
        }
    }

    /// <summary>
    /// 카메라가 타겟에 맞추어 움직이는 함수
    /// </summary>
    private void Moving()
    {
        PosObjTarget = objTarget.transform.position;

        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, PosObjTarget, ref VeclastMoveSpd, smoothTime);

        transform.position = smoothPos;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, zoomSize, ref lastZoomSpd, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        Moving();
        Zoom();
    }

}
