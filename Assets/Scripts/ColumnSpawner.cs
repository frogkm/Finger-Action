using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public BoardManager boardManager;

    private void OnTriggerExit2D(Collider2D collider) {
        boardManager.makeBlock(new Vector2(collider.transform.position.x + boardManager.blockSize, collider.transform.position.y));
    }
}
