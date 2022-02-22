using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.UI;
using TMPro;

public class ECSInterface : MonoBehaviour
{
    World world;

    [SerializeField] TextMeshProUGUI sheepTextField;
    [SerializeField] TextMeshProUGUI tonkTextField;
    EntityManager entityManager;
    // Start is called before the first frame update
    void Start()
    {
        sheepTextField.SetText("");
        tonkTextField.SetText("");
        world = World.DefaultGameObjectInjectionWorld;
        Debug.Log($"All entities{world.GetExistingSystem<MoveSystem>().EntityManager.GetAllEntities().Length}");

        entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;
        //EntityQuery query = entityManager.CreateEntityQuery(ComponentType.ReadOnly <SheepComponent>());
        //Debug.Log($"All sheep {query.CalculateEntityCount()}");
    }

    public void CountSheep()
    {
        
        EntityQuery query = entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepComponent>());
        sheepTextField.SetText($"{query.CalculateEntityCount()}");
    }

    public void CountTonks()
    {

        EntityQuery query = entityManager.CreateEntityQuery(ComponentType.ReadOnly<TankComponent>());
        tonkTextField.SetText($"{query.CalculateEntityCount()}");
    }
}
