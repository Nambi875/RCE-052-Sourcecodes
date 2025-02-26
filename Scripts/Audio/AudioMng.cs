using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMng : MonoBehaviour
{
    static AudioMng instance;
    public static AudioMng Getins
    {
        get
        {
            if (instance == null) instance = new AudioMng();
            return instance;
        }
    }
    //ここからコーディングをする



    //エレベーターサウンド
    public AudioSource openDelaySound;  // エレベーターが動く音
    public AudioSource openSound;       // エレベーターのドアが開く音
    public AudioSource closeSound;      // エレベーターのドアが閉まる音

    
}
