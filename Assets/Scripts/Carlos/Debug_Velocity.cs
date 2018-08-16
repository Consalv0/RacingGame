using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Debug_Velocity : MonoBehaviour {

  public Text text;
  public SimpleCarController car;
  public Rigidbody rb;

  private void Start()
  {
    car.GetComponent<SimpleCarController>();
    rb.GetComponent<Rigidbody>();
  }

  private void Update()
  {
    text.text = "Vector velocidad: " + rb.velocity + "   magnitud: " + rb.velocity.magnitude+ "                          motor force: "+car.motorForce;
  }
}
