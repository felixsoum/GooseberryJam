using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillIndicator : MonoBehaviour
{
    Vector3 initialPos;

    void Awake()
    {
        initialPos = transform.localPosition;
    }

    void Update()
    {
        float inc = (Mathf.Sin(10 * Time.time) + 1) / 2f;
        transform.localPosition = initialPos + Vector3.up * inc * 0.15f;
    }
}
