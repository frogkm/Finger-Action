using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorAfterTouch : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }

}
