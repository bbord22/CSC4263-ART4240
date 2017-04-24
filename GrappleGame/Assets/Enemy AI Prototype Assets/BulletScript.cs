using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public int speed;
    private GameObject player;
    private Transform target;
    //private Rigidbody2D rb2d;
    private Vector3 targetVector;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        ///rb2d = GetComponent<Rigidbody2D>();
        targetVector = target.position;
	}
	
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetVector, step);

        if (transform.position == targetVector)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player") && !other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
