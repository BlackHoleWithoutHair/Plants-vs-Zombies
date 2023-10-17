using UnityEngine;

public class SunController : AbstractController
{
    float CumulativeTime;
    float WaitTime;
    public SunController() { }
    protected override void Init()
    {
        base.Init();
        WaitTime = 10f;
    }
    protected override void OnAfterRunUpdate()
    {
        base.OnAfterRunUpdate();
        if (CumulativeTime > WaitTime)
        {
            WaitTime = Random.Range(9f, 12f);
            CumulativeTime = 0;
            ItemFactory.Instance.GetBullet(BulletType.DropSun, Vector2.zero).AddToController();
        }
        else
        {
            CumulativeTime += Time.deltaTime;
        }
    }
}
