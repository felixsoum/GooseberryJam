﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public GameObject judgementMark;
    public bool IsMarked { get; set; }

    new Rigidbody2D rigidbody;
    GameController gameController;
    bool isDragged;
    float wanderTimeMin = 1;
    float wanderTimeMax = 5;
    float wanderForce = 2;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke("Wander", Random.Range(wanderTimeMin, wanderTimeMax));
    }

	void OnMouseDown()
    {
        isDragged = true;
        rigidbody.isKinematic = true;
        gameController.DeactivateVision();
    }

    void OnMouseUp()
    {
        isDragged = false;
        rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (isDragged)
        {
            Vector3 pos = Util.ClampToWorld(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }

    void Wander()
    {
        if (!isDragged)
        {
            rigidbody.AddForce(Random.insideUnitCircle * wanderForce, ForceMode2D.Impulse);
        }
        Invoke("Wander", Random.Range(wanderTimeMin, wanderTimeMax));
    }

    public void ShowMark()
    {
        if (IsMarked)
        {
            judgementMark.SetActive(true);
        }
    }

    public void HideMark()
    {
        judgementMark.SetActive(false);
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
    }
}
