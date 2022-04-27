using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class Diamond : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject {

    public Hand grasped;
    public NetworkId Id { get; set; }
    // NetworkId INetworkObject.Id => new NetworkId(1001);
    public bool isCollected;

    private NetworkContext context;

    void IGraspable.Grasp (Hand controller) {
        Debug.Log ("Clicked diamond");
        GameObject player = GameObject.Find ("Player");
        float dis = Vector3.Distance (player.transform.position, transform.position);
        Debug.Log ("diamond distance is " + dis);
        if (dis < 4.0)
            grasped = controller;
    }

    void INetworkComponent.ProcessMessage (ReferenceCountedSceneGraphMessage message) {
        var msg = message.FromJson<Message> ();
        transform.position = msg.position;
        transform.rotation = msg.rotation;
    }

    void IGraspable.Release (Hand controller) {
        grasped = null;
    }

    // Start is called before the first frame update
    void Start () {
        context = NetworkScene.Register (this);
    }

    struct Message {
        public Vector3 position;
        public Quaternion rotation;
    }

    // Update is called once per frame
    void Update () {
        if (grasped != null) {
            transform.position = grasped.transform.position;
            transform.rotation = grasped.transform.rotation;
            Message message;
            message.position = transform.position;
            message.rotation = transform.rotation;
            context.SendJson (message);
        }
    }

    public void Awake () {
        string newID = transform.position.ToString () + transform.rotation.ToString ();
        // string newID = System.DateTime.Now.ToFileTime().ToString();
        Id = new NetworkId ((uint) newID.GetHashCode ());
        isCollected = false;
        Debug.Log (isCollected);
        Debug.Log (Id);
    }

}