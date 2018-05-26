using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFade : MonoBehaviour
{
    Image image;
    float timer;
    float speed = 2;

    void Awake()
    {
        image = GetComponent<Image>();
        Flash();
    }

    void Update()
    {
        timer -= Time.deltaTime * speed;
        timer = Mathf.Max(timer, 0);
        image.color = new Color(1, 1, 1, Mathf.Sqrt(timer));
    }

    public void Flash()
    {
        timer = 1;
        image.color = new Color(1, 1, 1, 1);
    }
}
