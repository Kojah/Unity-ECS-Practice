using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float3 targetLocation = new float3(0, 0, 0);
        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   float3 heading = targetLocation - position.Value;
                   heading.y = 0;

                   rotation.Value = quaternion.LookRotation(heading, math.up());
                   position.Value += deltaTime * 0.5f * math.forward(rotation.Value);
               })
               .Schedule(inputDeps);

        return jobHandle;
    }

}
