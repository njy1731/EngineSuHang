using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asffafassfasf : MonoBehaviour
{
    //캐릭터 직선 이동 속도 (걷기)
    public float walkMoveSpd = 2.0f;

    //캐릭터 직선 이동 속도 (달리기)
    public float runMoveSpd = 3.5f;

    //캐릭터 회전 이동 속도 
    public float rotateMoveSpd = 100.0f;

    //캐릭터 회전 방향으로 몸을 돌리는 속도
    public float rotateBodySpd = 2.0f;

    //캐릭터 이동 속도 증가 값
    public float moveChageSpd = 0.1f;

    //현재 캐릭터 이동 백터 값 
    private Vector3 vecNowVelocity = Vector3.zero;

    //현재 캐릭터 이동 방향 벡터 
    private Vector3 vecMoveDirection = Vector3.zero;

    //CharacterController 캐싱 준비
    private CharacterController controllerCharacter = null;

    //캐릭터 CollisionFlags 초기값 설정
    private CollisionFlags collisionFlagsCharacter = CollisionFlags.None;

    //캐릭터 중력값
    private float gravity = 9.8f;

    //캐릭터 중력 속도 값
    private float verticalSpd = 0f;

    private bool stopMove = false;

    void Move()
    {
        if (stopMove == true)
        {
            return;
        }
        Transform CameraTransform = Camera.main.transform;
        //메인 카메라가 바라보는 방향이 월드상에 어떤 방향인가.
        Vector3 forward = CameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0.0f;

        //forward.z, forward.x
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        //키입력 
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //케릭터가 이동하고자 하는 방향 
        Vector3 targetDirection = horizontal * right + vertical * forward;

        //현재 이동하는 방향에서 원하는 방향으로 회전 

        vecMoveDirection = Vector3.RotateTowards(vecMoveDirection, targetDirection, rotateMoveSpd * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
        vecMoveDirection = vecMoveDirection.normalized;
        //캐릭터 이동 속도
        float spd = walkMoveSpd;

        //중력이동 
        Vector3 _vecTemp = new Vector3(0f, verticalSpd, 0f);

        // 프레임 이동 양
        Vector3 moveAmount = (vecMoveDirection * spd * Time.deltaTime) + _vecTemp;

    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }
}
