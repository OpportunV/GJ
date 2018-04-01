using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 10f;

    private LevelManager lm;
    private Rigidbody2D rb;

	void Awake() {
        lm = LevelManager.instance;
        rb = GetComponent<Rigidbody2D>();
	}

    private void Start() {
        if (lm.bugs.disabilities[(int)Bugs.Disabilities.ShotsStraight]) {
            rb.gravityScale = 0;
        }
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.BulletproofBlocks]) {
            return;
        }
        if (collision.CompareTag("Platform")) {
            Destroy(gameObject);
        }
    }

}
