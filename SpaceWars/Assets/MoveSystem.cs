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
        NativeArray<float3> waypointPositions = new NativeArray<float3>(GameDataManager.instance.wps, Allocator.TempJob);

        JobHandle jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation, ref ShipData shipData) =>
            {
                float distance = math.distance(position.Value, waypointPositions[shipData.currentWP]);
                if(distance < 60)
                {
                    shipData.approach = false;
                }
                else if(distance > 300)
                {
                    shipData.approach = true;
                }


                float3 heading; 
                
                if(shipData.approach)
                {
                    heading = waypointPositions[shipData.currentWP] - position.Value;
                } else
                {
                    heading = position.Value - waypointPositions[shipData.currentWP];
                }
                quaternion targetDirection = quaternion.LookRotation(heading, math.up());
                rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * shipData.rotationSpeed);
                position.Value += deltaTime * shipData.speed * math.forward(rotation.Value);

                /*if(math.distance(position.Value, waypointPositions[shipData.currentWP]) < 10)
                {
                    shipData.currentWP++;
                    if(shipData.currentWP >= waypointPositions.Length)
                    {
                        shipData.currentWP = 0;
                    }
                }*/
            })
            .Schedule(inputDeps);

        waypointPositions.Dispose(jobHandle);
        return jobHandle;
    }
}
