using UnityEngine;

public class Bugs {

    public event System.Action OnDisabilitiesChange;

    [System.Serializable]
    public enum Disabilities {
        WalkLeft, WalkRight, SingleJump, PressButtons,
        MoveBoxes, HealthBar, SolidBlocks, CameraFollow, BackgroundVisile,
        TreesVisible, PlayerVisible, BulletproofBlocks, ShotsStraight,
        HitEffects
    }

    public bool[] disabilities;

    public string[] messages;

    public int DefaultJumps {
        get {
            int tmpJumps = 0;
            tmpJumps += (disabilities[(int)Disabilities.SingleJump]) ? 1 : 0;
            return tmpJumps;
        }
    }

    public void SetMessages(string[] msgs) {
        messages = msgs;
    }

    public Bugs() {
        disabilities = new bool[(int)Disabilities.HitEffects];
        for (int i = 0, length = disabilities.Length; i < length; i++) {
            disabilities[i] = true;
        }
    }

    public void SetDisability(Disabilities disability, bool value) {
        disabilities[(int)disability] = value;
        OnDisabilitiesChange();
    }

}