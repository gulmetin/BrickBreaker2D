using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
     public float moveSpeed = 10f;
    private Rigidbody2D rb;
    public float horizontalInput {get;private set;}
    public float maxBounceAngle = 75f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
    }
    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

}
