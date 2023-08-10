using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float _speed =8f;
    Rigidbody2D _rigidbody;
    Vector2 _velocity;
    GameManager _gameManager;
    SpriteRenderer _renderer;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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
            _gameManager.Balls--;
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        _velocity=Vector2.Reflect(_velocity,other.contacts[0].normal);
        _rigidbody.velocity = _velocity*_speed;
    }
}
