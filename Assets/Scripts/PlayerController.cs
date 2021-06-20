using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public GameObject clone;
    public GameObject deathEffect;

    public float minTimeRange = 1f;
    public float maxTimeRange = 2f;
    public float minMoveRange = 2f;
    public float maxMoveRange = 4f;

    public Transform topBumper;
    public Transform bottomBumper;

    public float waitTime;
    private float waitTimer;

    private int forceDirection;

    private Rigidbody2D rigidBody;
    private Moveable moveable;
    private Moveable recentCloneMoveable;
    private bool fullMoving;

    public static int score = 0;
    private float timePerPoint = 1f;
    private float timer;
    public Text scoreText;

    void Start()
    {
        scoreText.enabled = SettingsManager.intToBool(PlayerPrefs.GetInt("scoreOn"));
        waitTimer = waitTime;
        rigidBody = GetComponent<Rigidbody2D>();
        moveable = GetComponent<Moveable>();
        timer = timePerPoint;
        score = 0;

        //fullMove(new Vector2(0, 0.5f), 5f);
    }

    void Update()
    {
        if (!(moveable.isMoving() || (recentCloneMoveable != null && recentCloneMoveable.isMoving()))) {
            fullMoving = false;
        }
        
        if (!fullMoving) {
            if (waitTimer < 0) {
                int sign = 1;
                if (UnityEngine.Random.Range(-1f, 1f) < 0) {
                    sign = -1;
                }
                if (forceDirection != 0) {
                    sign = forceDirection;
                    forceDirection = 0;
                }
                float newVel = sign * UnityEngine.Random.Range(minMoveRange, maxMoveRange);
                float timeMoved = UnityEngine.Random.Range(minTimeRange, maxTimeRange);
                while (newVel * timeMoved + transform.position.y < bottomBumper.position.y || newVel * timeMoved + transform.position.y > topBumper.position.y) {
                    sign = 1;
                    if (UnityEngine.Random.Range(-1f, 1f) < 0) {
                        sign = -1;
                    }
                    newVel = sign * UnityEngine.Random.Range(minMoveRange, maxMoveRange);
                    timeMoved = UnityEngine.Random.Range(minTimeRange, maxTimeRange);
                }  
                fullMove(new Vector2(0, newVel), timeMoved);
                waitTimer = waitTime;
            } 
            else {
                waitTimer -= Time.deltaTime;
            }
        }
        timer -= Time.deltaTime;
        if (timer <= 0) {
            score ++;
            timer = timePerPoint;
            scoreText.text =  "Score: " + score.ToString();
        }
    }

    public void fullMove(Vector2 velocity, float amountTime)  {
        fullMoving = true;
        cloneMove(velocity, amountTime);
        IEnumerator coroutine = WaitAndMove(amountTime, velocity, amountTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndMove(float waitTime, Vector2 velocity, float amountTime)
    {
        yield return new WaitForSeconds(waitTime);
        moveable.move(velocity, amountTime);
    }

    public void cloneMove(Vector2 velocity, float amountTime) {
        GameObject newClone = Instantiate(clone);
        newClone.transform.position = transform.position;
        recentCloneMoveable = newClone.GetComponent<Moveable>();
        recentCloneMoveable.Start();
        recentCloneMoveable.move(velocity, amountTime);
        Destroy(newClone, amountTime*2);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Block") {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.75f);
            Destroy(gameObject);
            SceneManager.LoadScene("DeathScene");
        } 
        else if(collision.gameObject.tag == "Wall") {
            forceDirection = (int)((transform.position.y - collision.transform.position.y) / Math.Abs(transform.position.y - collision.transform.position.y));
        }
    }
}
