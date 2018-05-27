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
    public BoxCollider2D feetCollider;

    new Rigidbody2D rigidbody;
    GameController gameController;
    bool isDragged;
    float wanderTimeMin = 1;
    float wanderTimeMax = 5;
    float wanderForce = 2;
    public bool IsAlive { get; private set; }
    float killForce = 100;

    void Awake()
    {
        IsAlive = true;
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke("Wander", Random.Range(wanderTimeMin, wanderTimeMax));
    }

	void OnMouseDown()
    {
        if (!IsAlive || gameController.IsGameOver)
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
        if (!IsAlive || gameController.IsGameOver)
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
        if (IsAlive)
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
        if (!IsAlive)
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
        spiritSprite.color = new Color(1, 1, 1, 1);
    }

    public void HideMark()
    {
        humanSprite.enabled = true;
        spiritSprite.color = new Color(1, 1, 1, 0);
    }

    public void SetGameController(GameController gc)
    {
        gameController = gc;
    }

    public void Kill()
    {
        IsAlive = false;
        ShowMark();
        rigidbody.drag = 0;
        rigidbody.AddForce(Vector3.up * killForce, ForceMode2D.Force);
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        feetCollider.enabled = false;
        Invoke("Die", 2);
    }

    public Vector3 GetFeetPosition()
    {
        return transform.position + new Vector3(feetCollider.offset.x, feetCollider.offset.y, 0);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Mark()
    {
        IsMarked = true;
        spiritSprite.GetComponent<Animator>().SetTrigger("Mark");
    }
}
