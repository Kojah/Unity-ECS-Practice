using Unity.Entities;

[GenerateAuthoringComponent]
public struct BulletData : IComponentData
{
    public float speed;
    public int waypoint;
    public Entity explosionPrefab;
}
