using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    //캐싱
    public Camera cam = null;

    //obj 캐싱준비
    public GameObject obj = null;

    //100m 선
    public float distanceRaser = 100f;

    //이동할 위치변수
    private Transform moveTarget;

    //위치변수까지 거리
    private float distanceMoveTarget;

    //layerMask 설정
    public LayerMask layerTarget;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 posStart = transform.position;
        //Vector3 camStart = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Vector3 posTarget = transform.forward;

        Ray ray = new Ray(posStart, posTarget);

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);
        RaycastHit InfoRaycast;

        if (Input.GetMouseButtonDown(0))
        {
            //RayCast (시작점, 타겟점, 방향, 거리)
            if(Physics.Raycast(ray, out InfoRaycast, distanceRaser, layerTarget))
            {
                //InfoRaycast.point : 충돌체 위치
                //InfoRaycast.normal : 충돌체가 바라보는 각도
                //InfoRaycast.distance : 거리

                //충돌체가 있으면
                Debug.Log("걸림");
                Debug.Log(InfoRaycast.collider.gameObject.name);

                //충돌체 오브젝트 가져오기
                GameObject objTarget = InfoRaycast.collider.gameObject;
                //충돌 오브젝트 색상을 변경
                objTarget.GetComponent<Renderer>().material.color = Color.cyan;
                //이동 위치 변수에 오브젝트 넣기
                moveTarget = InfoRaycast.transform;
                //이동 위치 변수의 거리
                distanceMoveTarget = InfoRaycast.distance;
            }
            else
            {
                Debug.Log("없음");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //이동 타겟이 존재하면 이동 타겟의 색상을 돌려줌
            if(moveTarget != null)
            {
                moveTarget.GetComponent<Renderer>().material.color = Color.white;
            }

            //이동 타겟을 리셋
            moveTarget = null;
        }

        if(moveTarget != null)
        {
            moveTarget.position = ray.origin + ray.direction * distanceMoveTarget;
        }
    }
}
