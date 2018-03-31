using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private int jumps, defaultJumps;
    private bool needToJump = false;
    private float horizontalMovement;
    private float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;

    private LevelManager lm;

    public float jumpVelocity, moveVelocity;
    
	void Start () {
        lm = LevelManager.instance;
        lm.bugs.OnDisabilitiesChange += OnDisabilitiesChange;
        defaultJumps = lm.bugs.DefaultJumps;

        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0) {
            needToJump = true;
        }
        horizontalMovement = Input.GetAxis("Horizontal");
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.WalkLeft]) {
            horizontalMovement = Mathf.Max(0, horizontalMovement);
        }
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.WalkRight]) {
            horizontalMovement = Mathf.Min(0, horizontalMovement);
        }
    }

    void FixedUpdate() {
        if (needToJump) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumps--;
            needToJump = false;
        }

        if (rb.velocity.y < 0f) {
            rb.velocity += Vector2.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } else if (!Input.GetKey(KeyCode.Space)) {
            rb.velocity += Vector2.up * Physics.gravity.y * lowJumpMultiplier * Time.fixedDeltaTime;
        }

        rb.velocity = new Vector2(horizontalMovement * moveVelocity, rb.velocity.y);
    }

    private void OnDisabilitiesChange() {
        defaultJumps = lm.bugs.DefaultJumps;
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.CompareTag("Platform")) {
            jumps = defaultJumps;
        }
    }

}
