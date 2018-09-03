using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

  public Collider[] points;
  private Collider currentPoint;
  private Collider nextPoint;
  private int pastPoint = 0;


  private void Start()
  {

    nextPoint = points[0];
  }
  private void OnTriggerEnter(Collider other)
  {
    for (int i = 0; i < points.Length; i++)
    {
      if (other == points[i] && other == nextPoint)
      {
        Debug.Log(i);
        currentPoint = nextPoint;
        if (i < points.Length - 1)
          nextPoint = points[i + 1];
        pastPoint += 1;
      }
    }
  }
  private void Update()
  {
    if (pastPoint == points.Length)
    {
      Debug.Log("Ganaste");
    }
  }


}
