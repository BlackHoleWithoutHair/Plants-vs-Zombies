using UnityEngine;
using UnityEngine.UI;

public class AdaptController : AbstractController
{
    RectTransform Left, Right;
    public AdaptController() { }
    protected override void Init()
    {
        base.Init();

        GameObject hide = GameObject.Find("MainCanvas").transform.Find("Hide").gameObject;
        hide.SetActive(true);
        Left = hide.transform.Find("Left").GetComponent<RectTransform>();
        Right = hide.transform.Find("Right").GetComponent<RectTransform>();
        Left.sizeDelta = new Vector2((Screen.width / (Screen.height / 600f) - 800) / 2.0f, 600);
        Right.sizeDelta = new Vector2((Screen.width / (Screen.height / 600f) - 800) / 2.0f, 600);
        GameObject.Find("MainCanvas").GetComponent<CanvasScaler>().scaleFactor = Screen.height / 600f;
        Camera.main.orthographicSize = 600f / 2.0f / 60f;
    }
}
