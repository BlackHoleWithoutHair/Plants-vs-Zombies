using UnityEngine;

public abstract class IOneTimePlant:IPlant
{
    protected float FireWaitTime;
    protected float Timer;
    public IOneTimePlant(GameObject obj) : base(obj) { }
    protected override void OnCharacterUpdate()
    {
        base.OnCharacterUpdate();
        Timer += Time.deltaTime;
    }
    protected override bool FireCondition()
    {
        return Timer > FireWaitTime;
    }
}
