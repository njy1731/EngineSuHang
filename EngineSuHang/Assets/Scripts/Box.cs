using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour
{
    public GameObject PopUPText;

    [Header("몬스터 스폰")]
    public GameObject monsterSpawner = null;
    public List<GameObject> monsters = new List<GameObject>();
    public int spawnMaxCnt = 3;
    float rndPos = 5;

    //[ContextMenu("Open")]
    [Header("스텟 증가량")]
    private int BoxPrice = 15;
    private int HPItem = 12;
    private float StrengthItem = 2;
    private float SpeedItem = 0.5f;

    IEnumerator PopUp()
    {
        PopUPText.SetActive(true);
        yield return new WaitForSeconds(1f);
        PopUPText.SetActive(false);
    }

    void GetBoxOpen()
    {
        int RandomBoxOpened = Random.Range(0, 10);

        if(RandomBoxOpened < 7)
        {
            GetItem();
        }

        else if(RandomBoxOpened < 9)
        {
            SpawnEnemy();
        }

        else
        {
            Blank();
        }
    }

    void GetItem()
    {
        int rdITem = Random.Range(0, 10);

        if (rdITem < 3)
        {
            PlayerCtrl.Instance.HP += HPItem;
            PlayerCtrl.Instance.UpdateHpBar();
            Debug.Log("HP");
        }

        else if (rdITem < 5)
        {
            PlayerCtrl.Instance.STRENGTH += StrengthItem;
            Debug.Log("strength");

        }

        else if(rdITem < 8)
        {
            if(PlayerCtrl.Instance.SPEED == 3f)
            {
                PlayerCtrl.Instance.Coin += 5;
            }
            if (PlayerCtrl.Instance.SPEED == 3f) return;
            PlayerCtrl.Instance.SPEED += SpeedItem;
            Debug.Log("speed");
        }

        else
        {
            PlayerCtrl.Instance.Coin += 10;
            StartCoroutine(PopUp());
            Debug.Log("Coin");
        }
    }

    void SpawnEnemy()
    {
        Debug.Log("적");
        if (monsters.Count > spawnMaxCnt)
        {
            return;
        }

        Vector3 vecSpawn = new Vector3(Random.Range(transform.position.x - rndPos, transform.position.x+ rndPos), 1000f, Random.Range(transform.position.z - rndPos, transform.position.z + rndPos));
        Ray ray = new Ray(vecSpawn, Vector3.down);
        RaycastHit raycastHit = new RaycastHit();
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity) == true)
        {
            vecSpawn.y = raycastHit.point.y;
        }
        GameObject newMonster = Instantiate(monsterSpawner, vecSpawn, Quaternion.identity);
        monsters.Add(newMonster);
    }

    void Blank()
    {
        Debug.Log("꽝");
    }

    public void Open()
    {
        PlayerCtrl.Instance.Coin -= BoxPrice;
        GetBoxOpen();
        GetComponent<Animator>().Play("BoxClip");
    }
    
    public void EndOpen()
    {
        Destroy(gameObject);
    }
}
