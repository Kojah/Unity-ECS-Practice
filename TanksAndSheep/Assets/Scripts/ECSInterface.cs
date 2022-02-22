using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSInterface : MonoBehaviour
{
    World world; 


    // Start is called before the first frame update
    void Start()
    {
        world = World.DefaultGameObjectInjectionWorld;
        Debug.Log($"All entities{world.GetExistingSystem<MoveSystem>().EntityManager.GetAllEntities().Length}");

        EntityManager entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;
        EntityQuery query = entityManager.CreateEntityQuery(ComponentType.ReadOnly <SheepComponent>());
        Debug.Log($"All sheep {query.CalculateEntityCount()}");
    }
}
