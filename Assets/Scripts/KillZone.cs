using UnityEngine;

public class KillZone : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        float time = Time.time % 2;
        Color color = spriteRenderer.color;
        color.a = Mathf.Pow(1 - Mathf.Clamp01(time), 2);
        spriteRenderer.color = color;
        spriteRenderer.transform.localScale = Vector3.one * (1 + time * 0.25f);
    }
}
