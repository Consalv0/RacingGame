using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Audio : MonoBehaviour {
    
    public AudioSource MovementAudio;        
    public AudioClip EngineIdling;           
    public AudioClip EngineAcelerating;          
    public AudioClip EngineStarting;           
    public float PitchRange = 0.2f;           
    private float OriginalPitch;
    public int audios = 0;

    private void Start()
    {
        MovementAudio.clip = EngineStarting;
        //MovementAudio.Play();
    }


    private void Update()
    {
        EngineAudio();
    }


    private void EngineAudio()
    {
        if (Input.GetKey("a"))
        {
            if (MovementAudio.clip == EngineIdling || MovementAudio.clip == EngineStarting)
            {
                MovementAudio.clip = EngineAcelerating;
                MovementAudio.Play();
            }
        }
        else
        {
            if (MovementAudio.clip == EngineAcelerating || MovementAudio.clip == EngineStarting)
            {
                MovementAudio.clip = EngineIdling;
                MovementAudio.Play();
            }
        }
    }
}
