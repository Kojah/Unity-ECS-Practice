using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class SpawnParallel : MonoBehaviour
{
    public GameObject sheepPrefab;
    const int numSheep = 15000;
    List<Transform> sheepTransforms = new List<Transform>();

    struct MoveJob: IJobParallelForTransform
    {
        public void Execute(int index, TransformAccess transform)
        {
            transform.position += 0.1f * (transform.rotation * new Vector3(0,0,1));
            if(transform.position.z > 50)
            {
                transform.position = new Vector3(transform.position.x, 0, -50);
            }
        }
    }

    MoveJob moveJob;
    JobHandle moveHandle;
    TransformAccessArray transforms;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numSheep; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            sheepTransforms.Add(Instantiate(sheepPrefab, pos, Quaternion.identity).transform);
        }
        transforms = new TransformAccessArray(sheepTransforms.ToArray());
    }

    private void Update()
    {
        moveJob = new MoveJob { };
        moveHandle = moveJob.Schedule(transforms);
    }

    private void LateUpdate()
    {
        moveHandle.Complete();
    }
    //Jobs stuff has to be disposed of, does not dispose of on its own!
    private void OnDestroy()
    {
        transforms.Dispose();
    }
}