using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoorOpen : MonoBehaviour {
    // player type = 1 if create new room; player type = 2 if join a room.
    public float playerType = 0;
    public string password = "";
    public string pwdDown = "352";
    public string pwdUp = "718";
    public bool isCorrect = false;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (playerType == 1) {
            if (password == pwdDown) {
                GameObject door = GameObject.Find ("Doors/Start_Door_1");
                door.transform.localEulerAngles = new Vector3 (0.0f, 120.303f, 0.0f);
                GameObject correctsound = GameObject.Find ("CorrectSound");
                correctsound.GetComponent<AudioSource> ().Play ();
                isCorrect = true;
            }
        } else if (playerType == 2) {
            if (password == pwdUp) {
                GameObject door = GameObject.Find ("Props/Start_Gate_2");
                door.transform.position = new Vector3 (0.263925f, 104.064f, 14.31406f);
                GameObject correctsound = GameObject.Find ("CorrectSound");
                correctsound.GetComponent<AudioSource> ().Play ();
                isCorrect = true;
            }
        } else {
            Debug.Log ("Player type: " + playerType);
        }

        if (password.Length >= 3) {
            password = "";
            if (!isCorrect) {
                GameObject wrongsound = GameObject.Find ("WrongSound");
                wrongsound.GetComponent<AudioSource> ().Play ();
            }

        }

    }
}