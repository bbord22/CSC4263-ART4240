using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankAI : MonoBehaviour {
    private int state;
    private int facing;
    private GameObject player;
    private Transform playerT;
    private float distFromPlayer;
    private Rigidbody2D rigidBody;
    public float AggroRadius;
    public float maxSpeed;

	void Start () {
        state = 0;
        facing = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.transform;
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        checkPlayerDist();

        if (state == 1)
        {
            facePlayer();
            rigidBody.velocity = new Vector2(0, 0);
        }
        else
        {
            rigidBody.velocity = new Vector2(facing * maxSpeed, rigidBody.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlatformEdge")
        {
            flip();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = 2;
            player = null;
            playerT = null;
            Destroy(other.gameObject);
			StartCoroutine ("EndGame");
        }
    }

	IEnumerator EndGame(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Scenes/GameOver");
	}


    void facePlayer()
    {
        if (transform.position.x < playerT.position.x && facing == -1)
        {
            flip();
        }
        else if (transform.position.x > playerT.position.x && facing == 1)
        {
            flip();
        }
    }

    void checkPlayerDist()
    {
        distFromPlayer = Vector2.Distance(playerT.position, transform.position);

        if (distFromPlayer < AggroRadius)
        {
            state = 1;
        }
        else
        {
            state = 0;
        }
    }

    void flip()
    {
        facing = facing * -1; //change which way it is labeled as facing
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
