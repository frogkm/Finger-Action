using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public SpawnBlocks spawnBlocks;

    private void OnTriggerExit2D(Collider2D collider) {
        spawnBlocks.makeBlock(new Vector2(collider.transform.position.x + spawnBlocks.blockSize, collider.transform.position.y));
    }
}
