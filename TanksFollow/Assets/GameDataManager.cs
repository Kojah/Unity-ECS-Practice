using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    public Transform player;
    public Transform[] waypoints;
    public float3[] positions;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        positions = new float3[waypoints.Length];
        for(int i = 0; i < waypoints.Length; i++)
        {
            positions[i] = waypoints[i].position;
        }
    }
}
