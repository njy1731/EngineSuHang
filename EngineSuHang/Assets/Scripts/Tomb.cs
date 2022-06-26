using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tomb : MonoBehaviour
{
    [Header("몬스터 스폰")]
    public GameObject bossSpawner = null;
    public List<GameObject> Boss = new List<GameObject>();
    public int spawnMaxCnt = 1;
    float rndPos = 5;

    void SpawnBoss()
    {
        Debug.Log("보스 스폰");
        if (this.Boss.Count > spawnMaxCnt)
        {
            return;
        }

        Vector3 vecSpawn = new Vector3(Random.Range(transform.position.x - rndPos, transform.position.x + rndPos), 1000f, Random.Range(transform.position.z - rndPos, transform.position.z + rndPos));
        Ray ray = new Ray(vecSpawn, Vector3.down);
        RaycastHit raycastHit = new RaycastHit();
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity) == true)
        {
            vecSpawn.y = raycastHit.point.y;
        }
        GameObject Boss = Instantiate(bossSpawner, vecSpawn, Quaternion.identity);
        this.Boss.Add(Boss);
    }

    void BossSpawn()
    {
        SpawnBoss();
        GetComponent<Animator>().Play("TombAnimation");
    }

    public void EndSpawn()
    {
        Destroy(gameObject);
    }
}
