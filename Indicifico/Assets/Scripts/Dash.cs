using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {
    public GameObject carro;
    float veloriginal;
    float velcondash;
    float velauxiliar;
    bool dash;

	// Use this for initialization
	void Start () {
        veloriginal = 30;
        velcondash = veloriginal*3f;
        velauxiliar = 0;
        dash = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f"))
        {
            dash = true;
            StartCoroutine(Countdown(1));
        }
        if (!dash)
        {
            carro.transform.position = carro.transform.position + new Vector3(veloriginal * Time.deltaTime, 0, 0);
        }
        else
        {
            carro.transform.position = carro.transform.position + new Vector3(velcondash * Time.deltaTime, 0, 0);
        }
	}

    IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        dash = false;
    }
}