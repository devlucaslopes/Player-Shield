using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;

    [Header("Dash Settings")]
    [SerializeField] private float DashForce;
    [SerializeField] private float DashDuration;
    [SerializeField] private float DashCooldown;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private bool _canMove = true;
    private bool _canDash = true;
    private bool _isDashing;
    private float _direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            _canMove = false;
            anim.SetBool("isBlocking", true);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            _canMove = true;
            anim.SetBool("isBlocking", false);
        }

        if (Input.GetButtonDown("Fire3") && _canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove || _isDashing) return;

        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalAxis * Speed, rb.velocity.y);

        if (horizontalAxis > 0)
        {
            _direction = 1;
            spriteRenderer.flipX = false;
        } else if (horizontalAxis < 0)
        {
            _direction = -1;
            spriteRenderer.flipX = true;
        }

        anim.SetBool("isRunning", horizontalAxis != 0);
    }

    public void TakeDamage()
    {
        if (_isDashing) return;

        anim.SetTrigger("tookDamage");
    }

    public void BlockDamage()
    {
        anim.SetTrigger("blocked");
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        trailRenderer.emitting = true;

        float gravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(DashForce * _direction, 0);

        yield return new WaitForSeconds(DashDuration);

        _isDashing = false;
        rb.gravityScale = gravityScale;

        trailRenderer.emitting = false;

        yield return new WaitForSeconds(DashCooldown);

        _canDash = true;
    }
}
