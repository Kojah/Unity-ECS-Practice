using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;

[UpdateAfter(typeof(MoveBulletSystem))]
public class TimedDestroySystem : JobComponentSystem
{
    EndSimulationEntityCommandBufferSystem buffer;

    protected override void OnCreate()
    {
        buffer = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    struct CullingJob: IJobForEachWithEntity<LifeTimeData>
    {
        public EntityCommandBuffer.Concurrent commands;
        public float dt;

        public void Execute(Entity entity, int index, ref LifeTimeData lifeTimeData)
        {
            lifeTimeData.lifeLeft -= dt;
            if(lifeTimeData.lifeLeft <= 0f)
            {
                commands.DestroyEntity(index, entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer.Concurrent commandsECB = buffer.CreateCommandBuffer().ToConcurrent();
        var job = new CullingJob
        {
            commands = commandsECB,
            dt = Time.DeltaTime
        };

        var jobHandle = job.Schedule(this, inputDeps);
        buffer.AddJobHandleForProducer(jobHandle);

        jobHandle.Complete();

        return inputDeps;
    }
}
