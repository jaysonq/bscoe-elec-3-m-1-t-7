using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {
    Rigidbody _rigidbody;
    [SerializeField] Vector3 initialpos;
    [SerializeField] GameObject Rocket;
    GameObject touchdown;
    bool isPlaying;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        initialpos = new Vector3(-0.1798952f, 4.512645f, -1.240017f);
        Rocket = GameObject.Find("rocketship");
        touchdown = GameObject.Find("touchdown");
        isPlaying = true;
	}
    void EnableRigidcody()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.detectCollisions = true;
    }
    void DisableRigigbody()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.detectCollisions = false;


    }

    // Update is called once per frame
    void Update () {
        if (isPlaying == true)
        {
            ProcessInput();
        }
        else {
            Rocket.transform.position = initialpos;
            Rocket.transform.rotation = Quaternion.identity;
            touchdown.GetComponent<Renderer>().material.color = Color.red;
            EnableRigidcody();

        }
	}
    public void ProcessInput() {
        if (Input.GetKey(KeyCode.Space))
        {
            print("space");
            _rigidbody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward*2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*2);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "obs1" || collision.gameObject.name == "obs2" ||
            collision.gameObject.name == "wall left" || collision.gameObject.name == "wall right" ||
            collision.gameObject.name == "ceilling" || collision.gameObject.name == "terrain") {
            Rocket.transform.position = initialpos;
            Rocket.transform.rotation = Quaternion.identity;
            _rigidbody.angularVelocity = new Vector3(0f,0f,0f);
            touchdown.GetComponent<Renderer>().material.color = Color.red;

        }
        if (collision.gameObject.name == "touchdown") {
            touchdown.GetComponent<Renderer>().material.color = Color.green;
            DisableRigigbody();
        }
    }


}
