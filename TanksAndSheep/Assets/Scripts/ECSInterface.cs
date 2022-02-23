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

    public GameObject tonk;
    public GameObject palmTree;
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            Instantiate(tonk, pos, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            var settings = GameObjectConversionSettings.FromWorld(world, null);
            var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(palmTree, settings);
            var instance = entityManager.Instantiate(prefab);
            var position = transform.TransformPoint(new float3(pos.x, 0, pos.z));
            entityManager.SetComponentData(instance, new Translation { Value = position });
            entityManager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });
        }
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
