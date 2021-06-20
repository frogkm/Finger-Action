using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

    
    public Transform divider;
    public int numColliderCircles;
    public AudioClip blockBreakSound;
    private TouchController touchController;
    private AudioSource audioSource;

    void Start()
    {
        setSettings();
        touchController = GetComponent<TouchController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void setSettings() {
        GetComponent<FBSManger>().enabled = SettingsManager.intToBool(PlayerPrefs.GetInt("fpsOn"));
    }


    private GameObject[] getBlocks() {
        return GameObject.FindGameObjectsWithTag("Block");
    }

    //Get colliders in a circle shape, and perform actions on them
    private void handleTouchCircle(Vector2 position, float radius) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D collider in colliders) {
            if (collider.tag == "Block") {
                if (!SettingsManager.intToBool(PlayerPrefs.GetInt("muteOn"))) {
                    audioSource.PlayOneShot(blockBreakSound);
                }
                GameObject temp = collider.gameObject; 
                if (SettingsManager.intToBool(PlayerPrefs.GetInt("blockFall"))) {
                    Destroy(collider);
                    Rigidbody2D rg = temp.GetComponent<Rigidbody2D>();
                    SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();
                    sr.color = new Color(255, 0, 0);
                    sr.sortingOrder ++;
                    rg.isKinematic = false;
                    rg.gravityScale = 3f;
                    rg.velocity = Vector2.zero;
                    rg.AddForce(new Vector2(Random.Range(-100f, 100f), 100f));
                    Destroy(temp, 5f);
                }
                else {
                    Destroy(temp);
                }
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
        
    }
}
