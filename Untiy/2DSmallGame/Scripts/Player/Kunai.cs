using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Kunai : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    public float speed = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();

        transform.localScale = player.transform.localScale;
        rb.AddForce(speed * transform.localScale.x * Vector3.right, ForceMode2D.Impulse);

        Destroy(this.gameObject, 5.0f);
    }

    private void Update()
    {
        float leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (transform.position.x < leftBoundary || transform.position.x > rightBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
