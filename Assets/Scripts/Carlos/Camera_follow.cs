using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{

  public Transform follow;
  public Vector3 offset;
  public float followSpeed = 10;
  public float lookSpeed = 10;


  public void LookAtTarget()
  {
    Vector3 lookDirection = follow.position - transform.position;
    Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
    transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);
  }

  public void MoveToTarget()
  {
    Vector3 targetPos = follow.position + follow.forward * offset.z + follow.right * offset.x + follow.up * offset.y;
    transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
  }

  private void FixedUpdate()
  {
    LookAtTarget();
    MoveToTarget();
  }
}
