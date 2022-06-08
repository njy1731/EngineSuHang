using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectCtrl : MonoBehaviour
{
    //�浹ü �ݰ�
    public float radius = 5f;
    //�浹ü �Ŀ�
    public float power = 200f;
    //�浹ü ���� �ø��Ÿ�
    public float flyingSize = 3f;

    private void Start()
    {
        //Skill Effect ��ġ
        Vector3 posskillEffect = transform.position;
        //posSkillEffect ��ġ�� �߽����� radius �ݰ濡 �ִ� ��� collider ������ ������
        Collider[] colliders = Physics.OverlapSphere(posskillEffect, radius);
        //��� collider �˻�
        foreach(Collider collider in colliders)
        {
            //�÷��̾� Tag�� �������� ���� �������� ����.
            if(collider.gameObject.CompareTag("PlayerAtk") == true)
            {
                continue;
            }
            //RigidBody Component ���� ����
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if(rigidbody != null)
            {
                //rigidbody �ȿ� �ִ� ���� ����� ����Ѵ�.
                rigidbody.AddExplosionForce(power, posskillEffect, radius, flyingSize);
            }
        }
    }
}
