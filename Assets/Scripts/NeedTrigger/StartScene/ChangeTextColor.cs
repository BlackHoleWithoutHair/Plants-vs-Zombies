using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private Color OriginColor;
    public Color TargetColor;
    private TextMeshProUGUI m_Text;
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        if (m_Text == null)
        {
            Debug.Log("text is null");
        }
        OriginColor = m_Text.color;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        m_Text.color = TargetColor;
    }

    public void OnPointerExit(PointerEventData data)
    {
        m_Text.color = OriginColor;
    }
}
