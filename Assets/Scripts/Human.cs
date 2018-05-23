using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public bool IsMarked { get; set; }
    public Animator animator;
    public SpriteRenderer humanSprite;
    public SpriteRenderer spiritSprite;
    public GameObject deathEffectPrefab;
    public Collider2D feetCollider;

    new Rigidbody2D rigidbody;
    GameController gameController;
    bool isDragged;
    float wanderTimeMin = 1;
    float wanderTimeMax = 5;
    float wanderForce = 2;
    bool isAlive = true;
    float killForce = 50;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke("Wander", Random.Range(wanderTimeMin, wanderTimeMax));
    }

	void OnMouseDown()
    {
        if (!isAlive)
        {
            return;
        }
        isDragged = true;
        rigidbody.isKinematic = true;
        gameController.DeactivateVision();
        animator.SetTrigger("PickUp");
    }

    void OnMouseUp()
    {
        if (!isAlive)
        {
            return;
        }
        isDragged = false;
        rigidbody.isKinematic = false;
        rigidbody.velocity = Vector3.zero;
        animator.SetTrigger("Drop");
    }

    void Update()
    {
        if (isAlive)
        {
            animator.SetBool("IsWalking", rigidbody.velocity.magnitude > 0.075f);
            Vector3 pos = transform.position;
            pos.z = pos.y;
            transform.position = pos;

            Vector3 scale = transform.localScale;
            if (rigidbody.velocity.x > 0)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            else if (rigidbody.velocity.x < 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
        }
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
        if (!isAlive)
        {
            return;
        }
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

    public void Kill()
    {
        isAlive = false;
        ShowMark();
        rigidbody.drag = 0;
        rigidbody.AddForce(Vector3.up * killForce, ForceMode2D.Force);
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        feetCollider.enabled = false;
        Invoke("Die", 5);
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
