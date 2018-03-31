using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 10f;

    private LevelManager lm;
    private Rigidbody2D rb;

	void Start () {
        lm = LevelManager.instance;
        rb = GetComponent<Rigidbody2D>();
        if (lm.bugs.disabilities[(int)Bugs.Disabilities.ShotsStraight]) {
            rb.gravityScale = 0;
        }
        rb.velocity = transform.right * speed;

        Destroy(gameObject, 5f);
	}
	
}
