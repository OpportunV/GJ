using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform[] waypoints;
    public GameObject weapon, bulletPrefab;
    public Transform bulletSpawner;
    public SpriteRenderer enemySprite;
    public float speed = 10f;
    public float detectDist = 5f;
    public float firerate = 1f;
    public bool isFolowing = true;
    public bool alwaysFire = false;

    public Bugs.Disabilities currentDisability;
    public GameObject errorMesagePrefab;

    private Transform player;
    private Vector3 targetWaypoint;
    private int targetIndex = 0;
    private Rigidbody2D rb;
    private IEnumerator currentRoutine;
    private float fireTime = 0f;
    private Vector2 dirToWaypoint, dirToPlayer;
    private LevelManager lm;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        lm = LevelManager.instance;
        player = lm.player.transform;
        GetNextWaypoint();
        currentRoutine = Patrol(Time.fixedDeltaTime);
        StartCoroutine(currentRoutine);
        Physics2D.queriesStartInColliders = false;
	}

    void FixedUpdate() {
        dirToPlayer = player.position - transform.position;
        dirToWaypoint = targetWaypoint - transform.position;
        if (alwaysFire) {
            Fire();
        }

        if (dirToWaypoint.x < 0f) {
            enemySprite.flipX = true;
            weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, Quaternion.Euler(0f, 0f, -180f), 10f);
        } else {
            enemySprite.flipX = false;
            weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, Quaternion.Euler(0f, 0f, 0f), 10f);
        }

        if (PlayerFound()) {
            float zAngle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, Quaternion.Euler(0f, 0f, zAngle), 10f);
            if (isFolowing) {
                targetWaypoint = player.position;
            }
            Fire();
        }
        
    }

    IEnumerator Patrol(float delay) {
        while (true) {
            if (Mathf.Abs(transform.position.x - targetWaypoint.x) < 0.1f) {
                GetNextWaypoint();
                yield return new WaitForSeconds(5 * delay);
            }
            rb.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.fixedDeltaTime);
            yield return new WaitForSeconds(delay);
        }
    }

    bool PlayerFound() {
        if (Vector3.Angle(dirToWaypoint, dirToPlayer) < 90f && Vector3.Distance(transform.position, player.position) < detectDist) {

            if (!lm.bugs.disabilities[(int)Bugs.Disabilities.BulletproofBlocks]) {
                return true;
            }
            
            RaycastHit2D hit = Physics2D.Raycast(bulletSpawner.position, dirToPlayer, 100f);
            if (hit) {
                if (hit.collider.CompareTag("Player")) {
                    return true;
                }
            }
            
        }
        return false;
    }

    void Fire() {
        if (Time.time > fireTime) {
            Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
            fireTime = Time.time + 1f / firerate;
        }
    }

    void GetNextWaypoint() {
        targetIndex = (targetIndex + 1) % waypoints.Length;
        targetWaypoint = new Vector2(waypoints[targetIndex].position.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Harmful")) {
            lm.bugs.SetDisability(currentDisability, false);
            if (player != null) {
                GameObject temp = Instantiate(errorMesagePrefab, player.position, Quaternion.identity);
                temp.GetComponent<ErrorMessageController>().SetText(lm.bugs.messages[(int)currentDisability]);
            }
            Destroy(gameObject);
        }
    }
}
