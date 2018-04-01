using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    #region Singleton
    public static LevelManager instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    public Bugs bugs = new Bugs();

    public GameObject player;

    string[] messages =
    {
        "Oops! Something went wrong. It looks like MoveLeft() method isn't declared in this scope.",
        "A Unity.MonoBehaviour.PlayerController.WalkRight() caused an unexpected error. Press Ok to get another one.",
        "Error. An Input method GetAxisRaw got wrong parameter Jump and will be terminated. Good luck!",
        "Object Button not found. Process will stop. Press Button to cancel",
        "Method Move() stopped with an error: tag 'Box' does not allowed. No more fight.",
        "Due to the content rating constarints health bar disabled. Or we just hadn't code it.",
        "Component Collider2D not recognized. Press any key to do nothing...",
        "Damn! Cameramen() quits with a message: ZZzz(-.-) I'm tired",
        "Background disabled. Don't worry, we don't have anyways.",
        "An System.Collections.dll refused access to GameObject.Tree due to lack of nature resources.",
        "Wrong object reference. Object of type ObjectType<Player.SpriteRenderer> not found. It seems like you have NoBody to play with",
        "Error! BulletPrefab do not response to the OnCollisionEnter() method.",
        "Physics2D disable ShootBulletForward() and returned <Oh Gravity, Thou Art a Heartless Bitch>",
        "Warning! Blood disabling is enabled. Do nothing to do nothing. Please contact the manufacturer for cynical humiliation."
    };
    public string[] punishment;

	void Start () {
        bugs.SetMessages(messages);
	}
}
