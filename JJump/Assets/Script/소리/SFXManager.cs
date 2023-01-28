using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip[] Music;
    public AudioSource SFX;

    public static SFXManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        SFX = GetComponent<AudioSource>();
        SFX.volume = 0.05f;
    }
   
    // Update is called once per frame
    
    public void SFXPlay()
    {
        if (Player.isJumping)
        {
            SFX.clip = Music[0];
            SFX.Play();
        }
        if(Player.isJumpRF)
        {
            SFX.clip = Music[1];
            SFX.Play(); 
        }
    }
}
