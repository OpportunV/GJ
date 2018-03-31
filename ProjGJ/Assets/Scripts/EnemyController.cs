using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform[] waypoints;
    public GameObject weapon, bulletPrefab;
    public Transform bulletSpawner;
    public float speed = 10f;
    public float detectDist = 5f;

    private Transform player;
    private Vector3 targetWaypoint;
    private int targetIndex = 0;
    private Rigidbody2D rb;
    private IEnumerator currentRoutine;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = LevelManager.instance.player.transform;
        GetNextWaypoint();
        currentRoutine = Patrol(Time.fixedDeltaTime);
        StartCoroutine(currentRoutine);
	}

    void FixedUpdate() {
        Vector3 dirToPlayer = player.position - transform.position;
        if (Vector3.Angle(rb.velocity, dirToPlayer) < 90f && Vector3.Distance(transform.position, player.position) < detectDist) {

            float zAngle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, zAngle), 10f);
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
            Debug.Log(GetComponent<Rigidbody2D>().velocity);
            yield return new WaitForSeconds(delay);
        }
    }

    void Fire() {
        Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
    }

    void GetNextWaypoint() {
        targetIndex = (targetIndex + 1) % waypoints.Length;
        targetWaypoint = new Vector2(waypoints[targetIndex].position.x, transform.position.y);
    }
}
