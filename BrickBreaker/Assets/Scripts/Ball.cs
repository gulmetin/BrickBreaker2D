using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float _speed =8f;
    Rigidbody2D _rigidbody;
    Vector2 _velocity;
    SpriteRenderer _renderer;

    [Header("Events")]
    public GameEvent onPlayerBallChanged;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        Invoke("Launch",1f);
    }

    private void Launch() {
        _rigidbody.velocity = Vector2.down*_speed;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;

        if(!_renderer.isVisible){
            Destroy(gameObject);
            onPlayerBallChanged.Raise();
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        _velocity=Vector2.Reflect(_velocity,other.contacts[0].normal);
        _rigidbody.velocity = _velocity*_speed;
    }
}
