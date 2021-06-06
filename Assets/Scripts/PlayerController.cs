using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public GameObject clone;
    public GameObject deathEffect;

    private Rigidbody2D rigidBody;
    private Moveable moveable;
    private Moveable recentCloneMoveable;
    private bool fullMoving;

    private int score;
    private float timePerPoint = 1f;
    private float timer;
    public Text scoreText;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        moveable = GetComponent<Moveable>();
        timer = timePerPoint;

        //fullMove(new Vector2(0, 0.5f), 5f);
    }

    void Update()
    {
        if (!(moveable.isMoving() || (recentCloneMoveable != null && recentCloneMoveable.isMoving()))) {
            fullMoving = false;
        }
        if (!fullMoving) {
            fullMove(new Vector2(0, Random.Range(-2f, 2f)), Random.Range(1f, 3f));
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
            SceneManager.LoadScene("MainMenu");
        } 
    }
}
