using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour {

  public GameObject car;
  public RenderTexture rt;
  public Camera cam;
  public GameObject multiplayerView;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKeyDown(KeyCode.I))
      AddCar();
	}

  private void AddCar()
  {
    Instantiate(car);
  }
  void EnableView()
  {
    Instantiate(multiplayerView);
  }
}
