﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class MoveBulletSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        JobHandle jobHandle = Entities
            .WithName("MoveBulletSystem")
            .ForEach((ref Translation position, ref Rotation rotation, ref BulletData bulletData) =>
            {
                position.Value += deltaTime * 100f * math.forward(rotation.Value);
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        Entities.WithoutBurst().WithStructuralChanges().ForEach((Entity entity, ref Translation position, ref Rotation rotation, ref BulletData bulletData, ref LifeTimeData lifeTimeData) =>
        {
            float distanceToTarget = math.length(GameDataManager.instance.wps[bulletData.waypoint] - position.Value);

            if(distanceToTarget < 27)
            {
                lifeTimeData.lifeLeft = 0;
            }
        }).Run();

        return jobHandle;
    }
}
