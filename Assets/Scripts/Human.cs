using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public bool IsMarked { get; set; }
    public Animator animator;
    public SpriteRenderer humanSprite;
    public SpriteRenderer spiritSprite;

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
        animator.SetTrigger("PickUp");
    }

    void OnMouseUp()
    {
        isDragged = false;
        rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
        animator.SetTrigger("Drop");
    }

    void Update()
    {
        animator.SetBool("IsWalking", rigidbody.velocity.magnitude > 0.075f);
        Vector3 pos = transform.position;
        pos.z = pos.y;
        transform.position = pos;
        
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
        humanSprite.enabled = false;
        spiritSprite.enabled = true;
    }

    public void HideMark()
    {
        humanSprite.enabled = true;
        spiritSprite.enabled = false;
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Mark()
    {
        IsMarked = true;
        spiritSprite.color = Color.red;
    }
}
