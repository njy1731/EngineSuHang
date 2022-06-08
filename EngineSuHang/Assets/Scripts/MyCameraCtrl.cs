using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraCtrl : MonoBehaviour
{
    Vector3 reverseDistance;
    public GameObject obj;
    //이거 용도 모르겠음
    protected Vector3 velocity = Vector3.zero;

    [Header("기본수치")]
    [Range(0, 1)]
    public float SmoothTime = 0.1f; // 카메라가 부드럽게 움직일 시간
    float xmove, ymove;
    public float distance = 3;
    protected float mouse;

    void Update()
    {
        Ioaa();
        SmoothCamera(distance, obj);
    }

    /// <summary>
    /// 이게 중요함
    /// </summary>
    /// <param name="pos">대충 디스턴스임</param>
    /// <param name="obj">바라볼객체임</param>
    protected void SmoothCamera(float pos, GameObject obj)
    {
        // 카메라가 3인칭이면 오브젝트로 부터 떨어져있는데 그게 각도 0,0,0일때 기준 포지션임
        reverseDistance = new Vector3(0.0f, pos / 3, -pos);
        // 대충 인자 ( 움직일 놈, 기준점, 몰루?, 시간 )
        transform.position = Vector3.SmoothDamp(transform.position,
                            //여기서 로테이션과 저 리버스 뭐시기를 곱하는 이유 -> 내 각도가 돌아가면 그때 그만큼 움직임도 변하는거임 그냥 왜우셈 이해하든가
                            obj.transform.position + (transform.rotation * reverseDistance),
                            ref velocity, SmoothTime);
        //obj.transform.position + ((transform.rotation * reverseDistance) * plus);
    }

    /// <summary>
    /// 대충 조작임
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
