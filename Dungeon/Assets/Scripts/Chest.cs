using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public int diamondCount = 0;

    // Start is called before the first frame update
    void Start() { }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        if (other.gameObject.transform.parent.name == "Diamonds")
        {
            if (other.gameObject.GetComponent<Diamond>().isCollected == false)
            {
                gameObject.GetComponent<AudioSource>().Play();
                Debug.Log("Add 1 diamond");
                diamondCount++;
                other.gameObject.GetComponent<Diamond>().isCollected = true;
            }
        }
        if (diamondCount == 4)
        {
            Debug.Log("4 diamonds! Victory!");
            GameObject victory_door = GameObject.Find("Inner Environment/Doors/Victory_Door");
            victory_door.GetComponent<AudioSource>().Play();
            GameObject dragon = GameObject.Find("Inner Environment/Dragon");
            dragon.GetComponent<AudioSource>().Play();
            victory_door.transform.localEulerAngles = new Vector3(0.0f, -120.0f, 0.0f);
        }
        
    }
    // Update is called once per frame
    void Update() { }
}