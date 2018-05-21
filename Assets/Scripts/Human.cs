using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    new Rigidbody2D rigidbody;
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
    }

    void OnMouseUp()
    {
        isDragged = false;
        rigidbody.isKinematic = false;
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

}
