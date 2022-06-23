using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieUI : MonoBehaviour
{
    public GameObject OnDieUi = null;
    
    public void OnClickReStart()
    {
        SceneManager.LoadScene(0);
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }
}
