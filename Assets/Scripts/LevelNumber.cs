using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : MonoBehaviour
{
    Text text;
    RectTransform rectTransform;
    Vector3 initialPos;
    float speed = 5;

    void Awake()
    {
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        initialPos = rectTransform.localPosition;
    }

    void Update()
    {
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, initialPos, Time.deltaTime * speed);
    }

    public void SetNumber(int n)
    {
        text.text = n.ToString();
        Vector3 pos = initialPos;
        pos.x += 50;
        rectTransform.localPosition = pos;
    }
}
