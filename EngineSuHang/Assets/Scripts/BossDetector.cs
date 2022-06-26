using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetector : MonoBehaviour
{
    public string targetTag = string.Empty;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) == true)
        {
            gameObject.SendMessageUpwards("OnCKTarget", other.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}
