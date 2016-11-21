using UnityEngine;
using System.Collections;

public class FireballCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collider other)
    {
        Debug.Log("EnteredCollisionWith " + other.tag);
        if (other.tag == "Fireball")
        {
            GetComponent<OurBehaviorTree>().winCount++;
        }
    }
}
