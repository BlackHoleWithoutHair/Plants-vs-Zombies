public class BaseShareAttr
{
    public int MaxHp;
    public float AttackInterval;
    public int Damage;
}
[System.Serializable]
public class ZombieShareAttr : BaseShareAttr
{
    public ZombieType Type;
    public int CriticalHp;
    public float MoveSpeed;
    public float AttackDistance;
}
[System.Serializable]
public class PlantShareAttr : BaseShareAttr
{
    public PlantType Type;
    public float Spend;
    public float PlantCoolTime;
}