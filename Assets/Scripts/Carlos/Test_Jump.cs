using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Jump : MonoBehaviour {

  Rigidbody rb;
  public float timeLeft;
  public float force;
  private float timep;
  public char player;
  // Use this for initialization
  void Start () {
    timep = timeLeft;
    force *= 10000;
    rb = GetComponent<Rigidbody>();
	}
  private void Update()
  {
    if(!Timer(timeLeft))
    timeLeft -= Time.deltaTime;
  }

  // Update is called once per frame
  void FixedUpdate () {
    //Debug.Log(timep);
 
    if (Timer(timeLeft)&& Input.GetAxis("jump"+player)==1)
    {
      Debug.Log("activate");
      rb.AddForce(new Vector3(0,force, 0), ForceMode.Impulse);
      timeLeft = timep;
    }
    if(timeLeft<0)
    {
      timeLeft = -1;
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
