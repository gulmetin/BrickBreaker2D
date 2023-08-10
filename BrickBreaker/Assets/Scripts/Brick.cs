using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; private set;}
    public int health {get; private set;}
    public int points =120;
    public Sprite[] states;
    GameManager _gameManager;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        this.health = this.states.Length;
        this.spriteRenderer.sprite = this.states[this.health-1];
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void  Hit() {
       
        _gameManager.Score+= points/health;
        this.health--;

        
        if(this.health<=0){
            Destroy(this.gameObject);
        }else{
            this.spriteRenderer.sprite = this.states[this.health-1];
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        Hit();
    }
}
