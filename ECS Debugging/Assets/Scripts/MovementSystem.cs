using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities.WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotations, ref SheepData sheepData) =>
            {
                position.Value += 0.01f * math.up();
            }).Schedule(inputDeps);

        return jobHandle;
    }
}
