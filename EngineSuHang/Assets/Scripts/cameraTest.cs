using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTest : MonoBehaviour
{
    //Ÿ��
    public Transform objTarget = null;

    //Ʈ��ŷ Ÿ��
    public float smoothTime = 0.2f;

    //������ �ӵ��� �ʿ�
    private Vector3 VeclastMoveSpd;

    //��ǥ ��ġ
    private Vector3 PosObjTarget;

    //ī�޶�
    private Camera cam;

    //ī�޶� 3���� ���
    public enum ZoomState { zoomIn, zoomOut, tracking }

    //�� ����
    private float zoomSize = 5f;

    //�� ���� ��
    private const float zoomOut = 14.5f;
    private const float zoomIn = 5f;
    private const float tracking = 10f;

    //������ ���� �ӵ�
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
    /// ī�޶� Ÿ�ٿ� ���߾� �����̴� �Լ�
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
