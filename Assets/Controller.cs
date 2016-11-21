using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public GameObject activeObs;
    public float speed;// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 fwd = activeObs.transform.forward;
            fwd.Normalize();
            activeObs.GetComponent<Rigidbody>().AddForce(fwd * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 fwd = -activeObs.transform.forward;
            fwd.Normalize();
            activeObs.GetComponent<Rigidbody>().AddForce(fwd * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 fwd = activeObs.transform.right;
            fwd.Normalize();
            activeObs.GetComponent<Rigidbody>().AddForce(fwd * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 fwd = -activeObs.transform.right;
            fwd.Normalize();
            activeObs.GetComponent<Rigidbody>().AddForce(fwd * speed);
        }
    }
}
