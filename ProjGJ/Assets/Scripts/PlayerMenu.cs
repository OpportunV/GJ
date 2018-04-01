using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour {

    public SpriteRenderer playerSprite;

    void FixedUpdate() {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0f;

        float zAngle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, zAngle), 10f);

        if (90f < transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 270f) {
            playerSprite.flipX = true;
        } else {
            playerSprite.flipX = false;
        }
    }
}
