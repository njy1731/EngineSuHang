using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullDetector : MonoBehaviour
{
    //Trigger�� ���ؼ� ã���� �ϴ� ��ǥ Tag ����
    public string targetTag = string.Empty;

    //Trigger �ȿ� ���Գ�?
    private void OnTriggerEnter(Collider other)
    {
        //Tag�� ã�� Tag�ΰ�?
        //�� ���꺤Ʈ �Ÿ� �˱�
        if(other.CompareTag(targetTag) == true)
        {
            //�˸� �޼���
            gameObject.SendMessageUpwards("OnCKTarget", other.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
