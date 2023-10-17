using UnityEngine;
using UnityEngine.EventSystems;
using MainMenuScene;
public class OnButtonEnter : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        AudioUtility.Instance.PlayOneShot("bleep");
    }
}
