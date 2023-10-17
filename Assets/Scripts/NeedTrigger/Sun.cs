using BattleScene;
using UnityEngine;
public class Sun : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 BirthPosition;
    private Vector2 TargetPosition;
    private Vector2 IconPosition;
    private SpriteRenderer render;
    private float Speed = 1f;
    private float RunSpeed = 10f;
    private float alpha = 1f;
    private bool BeClick;
    private float CumulativeTime;
    void Start()
    {
        render = transform.GetComponent<SpriteRenderer>();
        BirthPosition.Set(Random.Range(-5.7f, 4.35f), 6.19f);
        TargetPosition.Set(BirthPosition.x, 6.19f - Random.Range(7f, 10f));
        IconPosition.Set(-5.98f, 4.514f);
        transform.position = BirthPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BeClick)
        {
            if (Vector2.Distance(transform.position, TargetPosition) > 0.01f)
            {
                transform.Translate(Vector2.down * Speed * Time.deltaTime);
            }
            else
            {
                CumulativeTime += Time.deltaTime;
                if (CumulativeTime > 4f)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, IconPosition) < 0.01f)
            {
                UIModelCommand.Instance.AddSun(25);
                Destroy(gameObject);
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

    }
    private void OnMouseDown()
    {
        BeClick = true;
        AudioUtility.Instance.PlayOneShot("points");
    }
}
