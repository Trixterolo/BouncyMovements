using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 reflection = Vector2.Reflect(collision.contacts[0].normal, Vector2.right);
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            velocity = reflection.normalized * velocity.magnitude;
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}
