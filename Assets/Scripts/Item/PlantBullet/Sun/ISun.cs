using UnityEngine;
using BattleScene;

public class ISun:Item
{
    private bool BeClick;
    private float alpha;
    private float RunSpeed = 10f;
    private SpriteRenderer render;
    private Vector2 IconPosition;
    private BoxCollider2D SunArea;
    private Collider2D[] colliders;

    public ISun(GameObject obj,Vector2 pos) : base(obj, pos) { }
    protected override void Init()
    {
        base.Init();
        IconPosition.Set(-5.98f, 4.51f);
        SunArea=GameObject.Find("SunArea").GetComponent<BoxCollider2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if(!BeClick)
        {
            if(Input.GetMouseButtonDown(0))
            {
                colliders = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
                foreach (Collider2D c in colliders)
                {
                    if (c.gameObject == gameObject)
                    {
                        BeClick = true;
                        AudioUtility.Instance.PlayOneShot("points");
                        break;
                    }
                }
            }
            BeforeClickUpdate();
        }
        else
        {
            AfterClick();
        }
    }
    protected virtual void BeforeClickUpdate() { }
    protected void AfterClick()
    {
        if (Vector2.Distance(transform.position, IconPosition) < 0.01f)
        {
            UIModelCommand.Instance.AddSun(25);
            Remove();
        }
        else
        {
            transform.Translate((IconPosition - (Vector2)transform.position) * RunSpeed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, IconPosition) < 1f)
        {
            alpha = (Vector2.Distance(transform.position, IconPosition) + 0.5f) / 1.5f;
            render.color = new Color(1, 1, 1, alpha);
            transform.localScale = new Vector3(Mathf.Clamp(Vector2.Distance(transform.position, IconPosition), 0.5f, 1f), Mathf.Clamp(Vector2.Distance(transform.position, IconPosition), 0.5f, 1f), 1f);
        }
    }
    public Vector2 GetSunEndPoint()
    {
        return new Vector2(Random.Range(SunArea.bounds.min.x, SunArea.bounds.max.x), Random.Range(SunArea.bounds.min.y, SunArea.bounds.center.y));
    }
    public Vector2 GetSunBirthPoint()
    {
        return new Vector2(Random.Range(SunArea.bounds.min.x, SunArea.bounds.max.x), SunArea.bounds.max.y);
    }
}
