using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed =10f;
    Rigidbody2D _rigidbody;
    Vector2 _velocity;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.down*_speed;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        _velocity=Vector2.Reflect(_velocity,other.contacts[0].normal);
        _rigidbody.velocity = _velocity*_speed;
    }
}
