using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class ShootSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        NativeArray<float3> gunPositions = new NativeArray<float3>(GameDataManager.instance.gunLocations, Allocator.TempJob);
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref Translation position, ref Rotation rotation, ref ShipData shipData) =>
            {
                foreach(float3 pos in gunPositions)
                {
                    var instance = EntityManager.Instantiate(shipData.bulletPrefab);
                    EntityManager.SetComponentData(instance, new Translation { Value = position.Value + math.mul(rotation.Value, pos) });
                    EntityManager.SetComponentData(instance, new Rotation { Value = rotation.Value });
                }
            }).Run();

        gunPositions.Dispose();
        return inputDeps;
    }
}
