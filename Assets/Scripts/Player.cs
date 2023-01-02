using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool _canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            _canMove = false;
            anim.SetBool("isBlocking", true);
        }

        if (Input.GetButtonUp("Fire3"))
        {
            _canMove = true;
            anim.SetBool("isBlocking", false);
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;

        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalAxis * Speed, rb.velocity.y);

        if (horizontalAxis > 0)
        {
            spriteRenderer.flipX = false;
        } else if (horizontalAxis < 0)
        {
            spriteRenderer.flipX = true;
        }

        anim.SetBool("isRunning", horizontalAxis != 0);
    }

    public void TakeDamage()
    {
        anim.SetTrigger("tookDamage");
    }

    public void BlockDamage()
    {
        anim.SetTrigger("blocked");
    }
}
