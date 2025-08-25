using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipsPlayer: MonoBehaviour
{
    [Tooltip("����0: �Ǫ��訣�쪱�a, 1: �Ǫ�����, 2: �Ǫ�����, 3: �Ǫ����`")]
    public List<AudioSource> audioSources;
    private bool alreadyPlayIntro = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            audioSources[0].Play();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            audioSources[1].Play();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            audioSources[2].Play();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            audioSources[3].Play();
        }
    }

    public void PlayAudioClip(int num)
    {
        if(num == 0)
        {
            if (!alreadyPlayIntro)
            {
                audioSources[num].Play();
                alreadyPlayIntro = true;
            }
            else if(alreadyPlayIntro)
            {
                Debug.Log("AlreadyPlayIntro");
                return;
            }
        }
        else
        {
            audioSources[num].Play();
        }
    }
}
