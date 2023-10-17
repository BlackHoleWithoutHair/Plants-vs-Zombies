using UnityEngine;
using BattleScene;
public class Pea : IBullet
{
    private float Speed = 4f;
    public Pea(GameObject obj,Vector2 pos) : base(obj, pos) { }
    protected override void Init()
    {
        base.Init();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        transform.position += Vector3.right * Speed * Time.deltaTime;
        if (transform.position.x > 12f)
        {
            Remove();
        }
    }
    protected override void OnHitEnemy(GameObject obj)
    {
        base.OnHitEnemy(obj);
        Object.Instantiate(ResourcesFactory.GetEffect(EffectType.PeaSplats.ToString()),transform.position,Quaternion.identity);
    }
}
