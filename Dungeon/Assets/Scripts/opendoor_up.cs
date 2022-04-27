using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class opendoor_up : MonoBehaviour, INetworkComponent, INetworkObject {

    public NetworkId Id { get; set; }
    // NetworkId INetworkObject.Id => new NetworkId(1001);
    private NetworkContext context;
    public bool isTriggered;
    public bool previousFlag;

    void INetworkComponent.ProcessMessage (ReferenceCountedSceneGraphMessage message) {
        var msg = message.FromJson<Message> ();
        isTriggered = msg.isOpened;
        previousFlag = isTriggered;
        if (isTriggered) {
            gameObject.GetComponent<AudioSource> ().Play ();
            GameObject up_door = GameObject.Find ("Inner Environment/Doors/Trigger_Door_Up");
            up_door.transform.localEulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
            previousFlag = isTriggered;
        } else {
            gameObject.GetComponent<AudioSource> ().Play ();
            GameObject up_door = GameObject.Find ("Inner Environment/Doors/Trigger_Door_Up");
            up_door.transform.localEulerAngles = new Vector3 (0.0f, 116.45f, 0.0f);
            previousFlag = isTriggered;
        }
    }

    // Start is called before the first frame update
    void Start () {
        context = NetworkScene.Register (this);
    }

    struct Message {
        public bool isOpened;
    }

    void OnCollisionEnter (Collision other) {
        previousFlag = isTriggered;
        isTriggered = true;
        gameObject.GetComponent<AudioSource> ().Play ();
        GameObject up_door = GameObject.Find ("Inner Environment/Doors/Trigger_Door_Up");
        up_door.transform.localEulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
    }

    void OnCollisionExit (Collision other) {
        previousFlag = isTriggered;
        isTriggered = false;
        gameObject.GetComponent<AudioSource> ().Play ();
        GameObject up_door = GameObject.Find ("Inner Environment/Doors/Trigger_Door_Up");
        up_door.transform.localEulerAngles = new Vector3 (0.0f, 116.45f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
        if (isTriggered != previousFlag) {
            // Debug.Log ("Flag: ");
            // Debug.Log (isTriggered);
            // Debug.Log ("previous flag: ");
            // Debug.Log (previousFlag);
            previousFlag = isTriggered;
            Message message;
            message.isOpened = isTriggered;
            context.SendJson (message);
        }
    }

    public void Awake () {
        string newID = transform.position.ToString () + transform.rotation.ToString ();
        // string newID = System.DateTime.Now.ToFileTime().ToString();
        Id = new NetworkId ((uint) newID.GetHashCode ());
        isTriggered = false;
        previousFlag = isTriggered;
        Debug.Log (Id);
    }
}