using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float timer;
    float speed = 3;
    float sign = -1;
    float minimumScale = 0.01f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime * sign * speed;
        timer = Mathf.Clamp(timer, 0, 1);
        transform.localScale = Vector3.Lerp(Vector3.one * minimumScale, Vector3.one, timer);
        spriteRenderer.enabled = timer > 0;
    }

    public void Activate()
    {
        sign = 1;
    }

    public void Deactivate()
    {
        sign = -1;
    }
}
