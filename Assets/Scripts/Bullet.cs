using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float Speed;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * Speed;

        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (collision.tag)
        {
            case "Player":
                player.TakeDamage();
                break;
            case "Shield":
                player.BlockDamage();
                break;
        }

        Destroy(gameObject);
    }
}
