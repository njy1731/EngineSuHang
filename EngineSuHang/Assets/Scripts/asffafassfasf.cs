using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asffafassfasf : MonoBehaviour
{
    //ĳ���� ���� �̵� �ӵ� (�ȱ�)
    public float walkMoveSpd = 2.0f;

    //ĳ���� ���� �̵� �ӵ� (�޸���)
    public float runMoveSpd = 3.5f;

    //ĳ���� ȸ�� �̵� �ӵ� 
    public float rotateMoveSpd = 100.0f;

    //ĳ���� ȸ�� �������� ���� ������ �ӵ�
    public float rotateBodySpd = 2.0f;

    //ĳ���� �̵� �ӵ� ���� ��
    public float moveChageSpd = 0.1f;

    //���� ĳ���� �̵� ���� �� 
    private Vector3 vecNowVelocity = Vector3.zero;

    //���� ĳ���� �̵� ���� ���� 
    private Vector3 vecMoveDirection = Vector3.zero;

    //CharacterController ĳ�� �غ�
    private CharacterController controllerCharacter = null;

    //ĳ���� CollisionFlags �ʱⰪ ����
    private CollisionFlags collisionFlagsCharacter = CollisionFlags.None;

    //ĳ���� �߷°�
    private float gravity = 9.8f;

    //ĳ���� �߷� �ӵ� ��
    private float verticalSpd = 0f;

    private bool stopMove = false;

    void Move()
    {
        if (stopMove == true)
        {
            return;
        }
        Transform CameraTransform = Camera.main.transform;
        //���� ī�޶� �ٶ󺸴� ������ ����� � �����ΰ�.
        Vector3 forward = CameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0.0f;

        //forward.z, forward.x
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        //Ű�Է� 
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //�ɸ��Ͱ� �̵��ϰ��� �ϴ� ���� 
        Vector3 targetDirection = horizontal * right + vertical * forward;

        //���� �̵��ϴ� ���⿡�� ���ϴ� �������� ȸ�� 

        vecMoveDirection = Vector3.RotateTowards(vecMoveDirection, targetDirection, rotateMoveSpd * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
        vecMoveDirection = vecMoveDirection.normalized;
        //ĳ���� �̵� �ӵ�
        float spd = walkMoveSpd;

        //�߷��̵� 
        Vector3 _vecTemp = new Vector3(0f, verticalSpd, 0f);

        // ������ �̵� ��
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
