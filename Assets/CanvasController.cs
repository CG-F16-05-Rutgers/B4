using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public Canvas speechbubble;
    public Text speechtext;
   // public Text texts;

	// Use this for initialization
	void Start () {
       speechbubble.enabled = false;
	}
	
	// Update is called once per frame
    public void enableSpeech()
    {
        speechbubble.enabled = true;
    }

    public void disableSpeech()
    {
        speechbubble.enabled = false;
        speechtext.text = "";
        
    }

    public void setText(string m)
    {
        speechtext.text = m;
    }

}
