using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destination : MonoBehaviour
{
    public AudioSource victory;
    public Rigidbody Rigidbody_of_player;
    Vector3 position_player;
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<AudioSource>().Play();
        Debug.Log(other.gameObject.name);
        position_player = other.gameObject.transform.position;
    }

    void OnCollisionStay(Collision other)
    {
        other.gameObject.transform.position = position_player;
    }
    void Show()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    void Hide()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
