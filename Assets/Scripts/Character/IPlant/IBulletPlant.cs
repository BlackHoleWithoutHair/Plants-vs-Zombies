using UnityEngine;
using BattleScene;
public abstract class IBulletPlant:IPlant
{
    protected float Timer;
    protected float FireInterval;
    public IBulletPlant(GameObject obj) : base(obj) { }
    protected override void OnCharacterUpdate()
    {
        base.OnCharacterUpdate();
        Timer += Time.deltaTime;
    }
    protected override void OnCharacterDieStart()
    {
        base.OnCharacterDieStart();
        ShouldBeRemove = true;
    }
    protected override bool FireCondition()
    {
        return Timer > FireInterval;
    }
    protected override void OnFire()
    {
        base.OnFire();
        Timer = 0;
    }
}
