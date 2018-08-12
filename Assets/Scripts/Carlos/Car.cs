using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

  public float speed;
  public float rotationSpeed;
  private float final_speed = 0;
  public  float translation;
  // Update is called once per frame
  void Update () {
    float prueba = Input.GetAxis("Aceleration");
   
    if(prueba>=1)
    {
      final_speed += 0.1f;
      if (final_speed >= speed)
        final_speed = speed;
      // Debug.Log(final_speed);
    }
    else
    {
      final_speed -= 0.1f;
      if (final_speed <= 0)
        final_speed = 0;
    }
   
    translation = Input.GetAxis("Aceleration") * final_speed;
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
    translation *= Time.deltaTime;
    rotation *= Time.deltaTime;
    transform.Translate(0, 0, translation);
    transform.Rotate(0, rotation, 0);
		
	}

  
}
