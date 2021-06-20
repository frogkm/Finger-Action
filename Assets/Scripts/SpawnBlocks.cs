using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    public GameObject block;
    public Transform spawner;
    public float blockSize;
    private int numYBlocks;
    public float blockSpeed;
    public float roundChangeTime = 10f;
    private float gameTimer;

    void Start()
    {
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

    private GameObject[] getBlocks() {
        return GameObject.FindGameObjectsWithTag("Block");
    }

    void Update() {
        gameTimer += Time.deltaTime;
        if (gameTimer >= roundChangeTime) {
            blockSpeed = blockSpeed * 1.2f;
            Debug.Log("Speed up");
            foreach (GameObject block in getBlocks()) {
                block.GetComponent<Rigidbody2D>().velocity = new Vector2(-blockSpeed, 0);
            }
            gameTimer = 0;
        }
    }
}
