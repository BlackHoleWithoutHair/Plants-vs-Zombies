using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    private float CumulativeTime;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CumulativeTime += Time.deltaTime;
        if (CumulativeTime > 3f)
        {
            Destroy(gameObject);
        }
    }
}
