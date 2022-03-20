using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

[UpdateAfter(typeof(MoveBulletSystem))]
public class TimedDestroySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float dt = Time.DeltaTime;
        Entities.WithoutBurst().WithStructuralChanges()
            .ForEach((Entity entity, ref LifeTimeData lifetimeData) =>
            {
                lifetimeData.lifeLeft -= dt;
                if (lifetimeData.lifeLeft <= 0f)
                    EntityManager.DestroyEntity(entity);
            })
        .Run();

        return inputDeps;
    }
}
