using UnityEngine;

public class IAttribute
{
    public BaseShareAttr ShareAttr;
    public Vector2 groundPosition;
    public float Hp;
    protected IAttribute(BaseShareAttr attr)
    {
        ShareAttr = attr;
    }
    public void SetShareAttr(BaseShareAttr shareAttr)
    {
        ShareAttr = shareAttr;
    }
}
public class ZombieAttribute : IAttribute
{
    public new ZombieShareAttr ShareAttr;
    public ZombieAttribute(ZombieShareAttr attr) : base(attr)
    {
        ShareAttr = base.ShareAttr as ZombieShareAttr;
    }
}
public class PlantAttribute : IAttribute
{
    public new PlantShareAttr ShareAttr;
    public PlantAttribute(PlantShareAttr attr) : base(attr)
    {
        ShareAttr = base.ShareAttr as PlantShareAttr;
    }
}
