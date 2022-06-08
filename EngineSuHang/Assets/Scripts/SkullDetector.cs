using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDetector : MonoBehaviour
{
    //Trigger를 통해서 찾고자 하는 목표 Tag 설정
    public string targetTag = string.Empty;

    //Trigger 안에 들어왔냐?
    private void OnTriggerEnter(Collider other)
    {
        //Tag가 찾던 Tag인가?
        //두 오브벡트 거리 알기
        if(other.CompareTag(targetTag) == true)
        {
            //알림 메세지
            gameObject.SendMessageUpwards("OnCKTarget", other.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
