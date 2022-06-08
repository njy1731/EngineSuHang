using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectCtrl : MonoBehaviour
{
    //충돌체 반경
    public float radius = 5f;
    //충돌체 파워
    public float power = 200f;
    //충돌체 위로 올릴거리
    public float flyingSize = 3f;

    private void Start()
    {
        //Skill Effect 위치
        Vector3 posskillEffect = transform.position;
        //posSkillEffect 위치를 중심으로 radius 반경에 있는 모든 collider 정보를 얻어오자
        Collider[] colliders = Physics.OverlapSphere(posskillEffect, radius);
        //모든 collider 검색
        foreach(Collider collider in colliders)
        {
            //플레이어 Tag는 제외하자 나를 날릴수는 없다.
            if(collider.gameObject.CompareTag("PlayerAtk") == true)
            {
                continue;
            }
            //RigidBody Component 존재 여부
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if(rigidbody != null)
            {
                //rigidbody 안에 있는 폭박 기능을 사용한다.
                rigidbody.AddExplosionForce(power, posskillEffect, radius, flyingSize);
            }
        }
    }
}
