using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TrackCameraSelection : MonoBehaviour {

    private float horizontalAxis;
    public int player;
    public bool InOutStage = true;
    public GameObject track1;
    public GameObject track2;

    // Use this for initialization
    void Start () {
        this.transform.LookAt(track1.transform);
	}
	
	// Update is called once per frame
	void Update () {
        horizontalAxis = Input.GetAxis("Turn" + (player+1));
        if (horizontalAxis > 0.8F)
        {
            InOutStage = true;
            this.transform.LookAt(track1.transform);
        }
        else if (horizontalAxis < -0.8F)
        {
            InOutStage = false;
            this.transform.LookAt(track2.transform);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("wadsads");
            if (InOutStage)
            {
                PlayerPrefs.SetInt("track", 1);
                CarSelector.whattrack = 0;
            }
            else
            {
                CarSelector.whattrack = 1;
                PlayerPrefs.SetInt("track", 0);
            }
            SceneManager.LoadScene(2);
        }
    }
}
