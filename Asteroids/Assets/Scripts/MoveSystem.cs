using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class NewBehaviourScript : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float speed = 10.0f;
        float3 targetLocation = new float3(0, 0, 0);

        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
                float rotSpeed = deltaTime * speed * 1 / math.distance(position.Value, targetLocation);
                position.Value = math.mul(quaternion.AxisAngle(new float3(0, 1, 0), rotSpeed), position.Value - targetLocation) + targetLocation;
            })
            .Schedule(inputDeps);

        return jobHandle; ;
    }
}
