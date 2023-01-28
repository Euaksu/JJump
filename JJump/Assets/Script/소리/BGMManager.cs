using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance; 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
       
    }
   
    //메인 bgm
    public AudioClip MainBGM;
    public AudioSource main; 

    //SFX 효과 
    public AudioClip[] Music;
    public AudioSource SFX;
    

    void Start()
    {
        //게임 시작할때 음악 출력
        SFX = GetComponent<AudioSource>();  
        SFX.playOnAwake= true;
        SFX.volume = 0.05f;
        SFX.clip = Music[0];
        SFX.loop = false;
        SFX.Play();
     
       
    }

    // Update is called once per frame
    void Update()
    {
        //반복해서 bgm 출력
       if (!main.isPlaying && !GameManager.GameOver && !GameManager.GameClear) 
        {
            main.clip = MainBGM;
            main.volume = 0.03f;
            main.Play();
        }
    }
   public void SFXPlay()
    {   

        //게임 오버 상황
        if (GameManager.GameOver)
        {
            main.Stop();
            SFX.clip = Music[1];
            SFX.Play();
        }
        //게임 클리어 상황
        if(GameManager.GameClear)
        {
            main.Stop();
            SFX.clip = Music[2];
            SFX.Play();
        }
        
    }
}
