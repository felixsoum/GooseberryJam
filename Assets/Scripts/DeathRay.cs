using UnityEngine;

public class DeathRay : MonoBehaviour
{
    float timer = 0;
    float speed = 2;
    Vector3 scale;

    void Awake()
    {
        scale = transform.localScale;
    }

    void Update()
    {
        timer += Time.deltaTime * speed;
        timer = Mathf.Min(timer, 1);

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.001f, scale.y, scale.z), timer);
    }
}
