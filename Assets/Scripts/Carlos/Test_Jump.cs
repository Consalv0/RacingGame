using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Jump : MonoBehaviour {

  Rigidbody rb;
  public float timeLeft;
  public float force;
  private float timep;
	// Use this for initialization
	void Start () {
    timep = timeLeft;
    force *= 10000;
    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
    Debug.Log(timep);
    timeLeft -= Time.deltaTime;
    if (Timer(timeLeft))
    {
      Debug.Log("activate");
      rb.AddForce(new Vector3(0,force,0), ForceMode.Impulse);
      timeLeft = timep;
    }
	}

  bool Timer(float time)
  {
    if (time < 0)
      return true;
    else
      return false;
  }

  
}
