using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

public class ParticleSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        JobHandle jobHandle = Entities
            .WithName("ParticleSystem")
            .ForEach((ref NonUniformScale scale, ref ParticleData particleData) =>
            {
                particleData.timeAlive += deltaTime;
                scale.Value += particleData.timeAlive * 0.8f;
                //more complex particle system physics can be done here, you just need position and rotation passed in too
            })
            .Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }
}
