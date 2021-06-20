using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    private float moveTimer;
    private Rigidbody2D rigidBody;
    private Vector2 moveVel = Vector2.zero;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (moveTimer > 0) {
            moveTimer -= Time.deltaTime;
        }
        else if(moveTimer < 0) {
            rigidBody.velocity = Vector2.zero;
        }
    }

    public void move(Vector2 velocity, float amountTime) {
        moveTimer = amountTime;
        rigidBody.velocity = velocity;
    }

    public bool isMoving(){
        return moveTimer > 0;
    }
}
