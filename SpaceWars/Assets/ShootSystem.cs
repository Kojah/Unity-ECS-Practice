using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class ShootSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref Translation position, ref Rotation rotation, ref ShipData shipData) =>
            {
                var instance = EntityManager.Instantiate(shipData.bulletPrefab);
                EntityManager.SetComponentData(instance, new Translation { Value = position.Value });
                EntityManager.SetComponentData(instance, new Rotation { Value = rotation.Value });
            }).Run();

        return inputDeps;
    }
}
