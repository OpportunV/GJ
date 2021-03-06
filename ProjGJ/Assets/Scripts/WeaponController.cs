﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform bulletSpawner;
    public GameObject bulletPrefab;

    public SpriteRenderer playerSprite;

    private float angVel = 10f;
    private bool needToShoot = false;
    private LevelManager lm;
	
	void Start () {
        lm = LevelManager.instance;
        lm.bugs.OnDisabilitiesChange += OnDisabilityChange;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            needToShoot = true;
        }
    }

    void FixedUpdate () {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0f;

        float zAngle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, zAngle), angVel);

        if (playerSprite.enabled) {
            if (90f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 270f) {
                playerSprite.flipX = true;
            } else {
                playerSprite.flipX = false;
            }
        }

        if (needToShoot) {
            Destroy(Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation), 5f);
            needToShoot = false;
        }
    }

    void OnDisabilityChange() {
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.PlayerVisible]) {
            if (playerSprite == null) {
                return;
            }
            playerSprite.enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}
