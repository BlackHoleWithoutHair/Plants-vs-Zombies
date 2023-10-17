using UnityEngine;

public class TriggerDetection:MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerCenter.Instance.NotisfyObserver(TriggerType.OnTriggerEnter, gameObject, collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerCenter.Instance.NotisfyObserver(TriggerType.OnTriggerExit, gameObject ,collision.gameObject);
    }
}
