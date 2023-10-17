using UnityEngine;

public class ZombieHead : MonoBehaviour
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
        if (CumulativeTime > 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
