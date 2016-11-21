using UnityEngine;
using System.Collections;

public class Derp : MonoBehaviour {

    public GameObject Updater;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter()
    {
        Updater.GetComponent<OurBehaviorTree>().winCount++;
    }
}
