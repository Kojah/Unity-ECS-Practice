using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        //NativeArray fixes the values from positions in place in memory, allowing it to exist
        //within a job. Regular float3 arrays won't work because they are not blittable 
        NativeArray<float3> waypointPositions = new NativeArray<float3>(GameDataManager.instance.positions, Allocator.TempJob);
        var jobHandle = Entities
               .WithName("MoveSystem")
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tankData) =>
               {
                   float3 heading = waypointPositions[tankData.currentWaypoint] - position.Value;
                   heading.y = 0;
                   quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                   rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * tankData.rotSpeed);
                   position.Value += deltaTime * tankData.speed * math.forward(rotation.Value);

                   if(math.distance(position.Value, waypointPositions[tankData.currentWaypoint]) < 1)
                   {
                       tankData.currentWaypoint++;
                       if(tankData.currentWaypoint >= waypointPositions.Length)
                       {
                           tankData.currentWaypoint = 0;
                       }
                   }
               })
               .Schedule(inputDeps);

        //NativeArrays are unmanaged so I have to dispose of it to avoid a mem leak!
        waypointPositions.Dispose(jobHandle);
        return jobHandle;
    }

}
