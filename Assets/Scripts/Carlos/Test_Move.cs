using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Move : MonoBehaviour {

 // public float speed;
  public Axle frontAxle;
  [Space]
  public Axle backAxle;
  [Space]
  public float maxMotorTorque;
  public float maxSteeringAngle;

  private void Awake()
  {
    //maxMotorTorque *= 10000000000;
  }
  public void FixedUpdate()
  {
    float motorTorque = maxMotorTorque;
    motorTorque *= 90000;
    float steeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");

    backAxle.UpdateRotationAndTorque(steeringAngle, motorTorque);
    frontAxle.UpdateRotationAndTorque(steeringAngle, motorTorque);
  }
  // Update is called once per frame
  //void Update()
  //{

  //  float translation = speed;
    
  //  translation *= Time.deltaTime;
   
  //  transform.Translate(0, 0, translation);
   

  //}


}
