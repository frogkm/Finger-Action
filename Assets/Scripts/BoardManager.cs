using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public GameObject block;
    public Transform divider;
    public Transform spawner;
    public float blockSize;
    public float blockSpeed;
    public int numColliderCircles;
    public float roundChangeTime = 10f;

    private int numYBlocks;
    private TouchController touchController;

    private float gameTimer;

    void Start()
    {
        touchController = GetComponent<TouchController>();
        numYBlocks = (int)(11 / blockSize);
        spawner.localScale = new Vector2(blockSize, spawner.localScale.y);
        spawnColumn();
    }

    public void spawnColumn() {
        for (int j = 0; j < numYBlocks; j++) {
            makeBlock(new Vector2(spawner.position.x, (j - (numYBlocks / 2)) * blockSize));
        }
    }

    public GameObject makeBlock(Vector2 spawnPos) {
        GameObject temp = Instantiate(block);
        temp.transform.localScale = new Vector2(blockSize / 1f, blockSize / 1f);
        temp.transform.position = spawnPos;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(-blockSpeed, 0);
        return temp;
    }

    //Get colliders in a circle shape, and perform actions on them
    private void handleTouchCircle(Vector2 position, float radius) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D collider in colliders) {
            if (collider.tag == "Block") {
                GameObject temp = collider.gameObject;
                Destroy(collider);
                Rigidbody2D rg = temp.GetComponent<Rigidbody2D>();
                SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();
                sr.color = new Color(255, 0, 0);
                sr.sortingOrder ++;
                rg.isKinematic = false;
                rg.gravityScale = 1f;
                rg.velocity = Vector2.zero;
                rg.AddForce(new Vector2(Random.Range(-100f, 100f), 100f));
                Destroy(temp, 5f);

            }
                
        }
    }

    void Update()
    {
        if(touchController.fingerDown() && touchController.getFingerPos().x >= divider.position.x){
            handleTouchCircle(touchController.getFingerPos(), 0.5f);
        }       
        
        if (touchController.fingerStillDown() && touchController.getFingerPos().x >= divider.position.x && touchController.getLastFingerPos().x >= divider.position.x) {
            Vector2 movement = touchController.getFingerMovement();
            for (int i=0; i < numColliderCircles; i++){
                Vector2 circlePos = ((movement / numColliderCircles) * i) + touchController.getLastFingerPos();
                handleTouchCircle(circlePos, 0.5f);
            }
        }  
        gameTimer += Time.deltaTime;

        if (gameTimer >= roundChangeTime) {
            Debug.Log("Speed up");
            blockSpeed = blockSpeed * 1.2f;
            gameTimer = 0;
        }
    }
}
