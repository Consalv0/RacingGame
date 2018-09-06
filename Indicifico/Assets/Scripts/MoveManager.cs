using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveManager : MonoBehaviour {
    public SimpleCarController car1, car2;
    public float startTime = 5;

    private float currentTime = 0;
    // Use this for initialization

	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if ( Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("Menu-Inicio");
        }

            if (car1 != null && car2 != null)
            if (currentTime < startTime)
            {
                Debug.Log("tiempo");
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                CambiarBool();

            }
    
	}

    void CambiarBool()
    {
        car1.CanMove = true;
        car2.CanMove = true;
    }
}

