using UnityEngine;

public class DropSun:ISun
{
    private Vector2 EndPos;
    private bool isStop;
    private float Timer;
    public DropSun(GameObject obj,Vector2 pos) : base(obj, pos) { }
    protected override void OnEnter()
    {
        base.OnEnter();
        transform.position = GetSunBirthPoint();
        EndPos = GetSunEndPoint();
    }
    protected override void BeforeClickUpdate()
    {
        base.BeforeClickUpdate();
        if(!isStop)
        {
            transform.position += Vector3.down * 2f * Time.deltaTime;
            if (transform.position.y < EndPos.y)
            {
                isStop = true;
            }
        }
        else
        {
            Timer += Time.deltaTime;
            if(Timer>4)
            {
                Remove();
            }
        }

    }
}
