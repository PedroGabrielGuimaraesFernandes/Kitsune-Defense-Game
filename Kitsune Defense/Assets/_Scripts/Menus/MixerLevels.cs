using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerLevels : MonoBehaviour {

	public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public float musicLev;
	public float sfxLev;

    private void Start()
    {
        
            musicLev = PlayerPrefs.GetFloat("musicLev");
            sfxLev = PlayerPrefs.GetFloat("sfxLev");
            //print(PlayerPrefs.GetFloat("musicLev"));
            //print(PlayerPrefs.GetFloat("sfxLev"));
        SetMusicLev(musicLev);
        SetSFXLev(sfxLev);
    }

    public void SetMusicLev(float musicLev){
		musicMixer.SetFloat ("musicVol", musicLev);
        PlayerPrefs.SetFloat("musicLev", musicLev);
        //PlayerPrefs.Save();
	}

	public void SetSFXLev(float sfxLev){
		sfxMixer.SetFloat ("sfxVol", sfxLev);
        PlayerPrefs.SetFloat("sfxLev", sfxLev);
        //PlayerPrefs.Save();
    }


}
