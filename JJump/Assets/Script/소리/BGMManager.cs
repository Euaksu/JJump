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
   
    //���� bgm
    public AudioClip MainBGM;
    public AudioSource main; 

    //SFX ȿ�� 
    public AudioClip[] Music;
    public AudioSource SFX;
    

    void Start()
    {
        //���� �����Ҷ� ���� ���
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
        //�ݺ��ؼ� bgm ���
       if (!main.isPlaying && !GameManager.GameOver && !GameManager.GameClear) 
        {
            main.clip = MainBGM;
            main.volume = 0.03f;
            main.Play();
        }
    }
   public void SFXPlay()
    {   

        //���� ���� ��Ȳ
        if (GameManager.GameOver)
        {
            main.Stop();
            SFX.clip = Music[1];
            SFX.Play();
        }
        //���� Ŭ���� ��Ȳ
        if(GameManager.GameClear)
        {
            main.Stop();
            SFX.clip = Music[2];
            SFX.Play();
        }
        
    }
}
