using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject BoxSpawner = null;
    [SerializeField]
    private List<GameObject> boxs = new List<GameObject>();
    [SerializeField]
    private int maxCnt = 50;

    float randomPos = 125f;

    void Spawn()
    {
        if(boxs.Count > maxCnt)
        {
            return;
        }
        
        Vector3 boxSpawnPoint = new Vector3(Random.Range(-randomPos, randomPos), 1000f, Random.Range(-randomPos, randomPos));
        Ray ray = new Ray(boxSpawnPoint, Vector3.down);
        RaycastHit raycastHit = new RaycastHit();
        if(Physics.Raycast(ray, out raycastHit, Mathf.Infinity) == true)
        {
            boxSpawnPoint.y = raycastHit.point.y;
        }

        GameObject newBox = Instantiate(BoxSpawner, boxSpawnPoint, Quaternion.identity);

        boxs.Add(newBox);
    }

    void Start()
    {
        InvokeRepeating("Spawn", 2f, 5f);
    }
}
