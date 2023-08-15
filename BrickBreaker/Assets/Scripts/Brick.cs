using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer {get; private set;}
    public int health {get; private set;}
    public Sprite[] states;

    [Header("Events")]
    public GameEvent onScoreChanged;
    public GameEvent onCheckLevel;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        this.health = this.states.Length;
        this.spriteRenderer.sprite = this.states[this.health-1];
    }

    private void  Hit() {
       
        onScoreChanged.Raise();
        this.health--;

        
        if(this.health<=0){
            Destroy(this.gameObject);
            onCheckLevel.Raise();
        }else{
            this.spriteRenderer.sprite = this.states[this.health-1];
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision) {
        Hit();
    }
}
