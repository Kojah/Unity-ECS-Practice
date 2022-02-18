using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class SpawnParallel : MonoBehaviour
{
    public GameObject sheepPrefab;
    const int numSheep = 15000;
    List<GameObject> sheepTransforms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numSheep; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            sheepTransforms.Add(Instantiate(sheepPrefab, pos, Quaternion.identity));
        }
    }

    private void Update()
    {
        foreach(GameObject sheep in sheepTransforms)
        {
            sheep.transform.Translate(0, 0, 0.1f);
            if (sheep.transform.position.z > 50)
            {
                sheep.transform.position = new Vector3(sheep.transform.position.x, 0, -50);
            }
        }
    }
}