using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class IoT : MonoBehaviour, ITrackableEventHandler {	
	public GameObject imageTarget;
	private TrackableBehaviour mTrackableBehaviour;
	private bool targetFound = false;
	public GameObject hide;
	string value;
    public string ledState = "off";
    private Material m_Material;


    void Start () {	
		mTrackableBehaviour = imageTarget.GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}
        //Fetch the Renderer from the GameObject
        m_Material = hide.GetComponent<Renderer>().material;
    }

	void Update () {
		if (targetFound) {
			hide.SetActive (true);
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit (); // exit app if back/esc button is pressed
			}
            if (Input.GetKeyDown("1"))
            {
                ledState = "on";
            }

            if (Input.GetKeyDown("0"))
            {
                ledState = "off";
            }

            
            if (ledState == "off")
           {
                //Led off
                m_Material.color = Color.black;
            }

            if (ledState == "on")
            {
                //Led on
                m_Material.color = Color.red;
            }
        }
		if (!targetFound) {
			hide.SetActive (false);
		}
	}

	void SelectionUI() {

	}

	public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			targetFound = true; //when target is found
		} else {
			targetFound = false; //when target is lost
		}
	}

}
