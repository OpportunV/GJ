using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private int jumps, defaultJumps;
    private bool needToJump = false;
    private float horizontalMovement;
    private float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;
    public GameObject errorMesagePrefab;

    private LevelManager lm;

    public float jumpVelocity, moveVelocity;
    
	void Start () {
        Time.timeScale = 1f;
        lm = LevelManager.instance;
        lm.bugs.OnDisabilitiesChange += OnDisabilitiesChange;
        defaultJumps = lm.bugs.DefaultJumps;
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.R)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
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
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.SolidBlocks]) {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject obj in platforms) {
                if (obj.layer == 9) {
                    var tileCol = obj.GetComponent<TilemapCollider2D>();
                    if (tileCol != null) {
                        tileCol.usedByEffector = false;
                    }
                }
            }
        }
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.TreesVisible]) {
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            foreach (GameObject tree in trees) {
                tree.SetActive(false);
            }
        }
        if (!lm.bugs.disabilities[(int)Bugs.Disabilities.BackgroundVisile]) {
            GameObject[] bgs = GameObject.FindGameObjectsWithTag("Background");
            foreach (GameObject bg in bgs) {
                bg.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Harmful")) {
            GameObject temp;
            int nextLvl = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            if (nextLvl >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings) {
                temp = Instantiate(errorMesagePrefab, transform.position, Quaternion.identity);
                temp.GetComponent<ErrorMessageController>().SetText("Wow. It seems you're won. What's a miracle!");
                Invoke("LoadMenu", 2f);
                return;
            }
            Time.timeScale = 0f;
            temp = Instantiate(errorMesagePrefab, transform.position, Quaternion.identity);
            temp.GetComponent<ErrorMessageController>().SetText("Press \"R\" to restart");
            Destroy(gameObject, 0.2f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.CompareTag("Platform")) {
            jumps = defaultJumps;
        }
    }

}
