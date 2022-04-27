using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("traps works");
    }
    void OnCollisionEnter(Collision other)
    {
        // diamonds = GameObject.Find("Inner Environment/Diamonds");
        // foreach (Ｔransform child in diamonds.transform)
        // {
        //     child.GetComponent<Diamond>().grasped = null;
        // }

        // if (other.gameObject.GetComponent<Diamond>().grasped != null)
        // {
        //     other.gameObject.GetComponent<Diamond>().grasped = null;
        // }

        // Debug.Log(other.gameObject.GetComponent<Diamond>().grasped);

        GameObject player = other.collider.gameObject;
           Debug.Log(player.name);
        if (player.name == "Player")
        {
            player.transform.position = new Vector3(-23, 91, 19);
            player.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        //Rigidbody_of_player.constraints = RigidbodyConstraints.FreezeAll;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
