using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Player : MonoBehaviour
{
    public List<AudioClip> Current_Audio_List;

    public AudioSource Current_Audio_Source;

    public int Current_Index;
    public void Start()
    {
        Current_Audio_Source = gameObject.GetComponent<AudioSource>();
        Current_Index = 3;
    }

    public void Update()
    {
        if(Current_Audio_Source.isPlaying==false)
        {
            Play(Current_Index);
        }
    }

    public void Play(int index)
    {


        Current_Index = index;
        // 1. 检查索引有效性
        if (index < 0 || index >= Current_Audio_List.Count)
        {
            Debug.LogWarning($"音频索引越界: {index}");
            return;
        }

        // 2. 检查是否正在播放同一个音频
        if (Current_Audio_Source.isPlaying && Current_Audio_Source.clip == Current_Audio_List[index])
        {
            Debug.Log("该音频正在播放中，跳过重复播放");
            return;
        }

        Current_Audio_Source.clip = Current_Audio_List[index];
        Current_Audio_Source.Play();
    }
}
