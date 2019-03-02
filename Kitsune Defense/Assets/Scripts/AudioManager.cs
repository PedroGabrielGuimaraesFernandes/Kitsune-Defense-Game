using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    
    [Header("Sons e musicas do game")]
    public Sound[] sounds;
    [Header("Musica a Tocar")]
    public int[] sceneMusic;

    public static AudioManager instance;

    private int num;

    //public bool changeMusic;

     public int actualScene;

    [SerializeField]
    private int playingMusic = 17;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            string OutputMixer = s.mixerName;
            s.source.outputAudioMixerGroup = s.mixer.FindMatchingGroups(OutputMixer)[0];

            //s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            
        }

    }

    private void Update()
    {

        if(SceneManager.GetActiveScene().buildIndex != actualScene)
        {

            actualScene = SceneManager.GetActiveScene().buildIndex;
            if (sceneMusic[actualScene] != playingMusic && sceneMusic[actualScene] != 2) {
                playingMusic = sceneMusic[actualScene];
                Stop("Music01");
                Stop("Music02");
                if (sceneMusic[actualScene] == 0)
                {
                    Play("Music01");
                    Debug.Log("Play(Music01);");
                } else if (sceneMusic[actualScene] == 1)
                {
                    Play("Music02");
                    Debug.Log("Play(Music01);");
                }
            }

        }

        //funciona
        /*if (numb != num)
        {
            Debug.Log("Diferente");
            numb = num;
        } else
        {
            Debug.Log("Igual");
            numb = 5;
        }*/

    }

    public void Play(string name)
    {
        // checa se um som com aquele nome esta na lista
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found ");
            return;
        }
        // toca o som
        s.source.Play();

        /*
            Lembrar em caso de multiplos objetos
            GameObject.FindGameObjectWithTag("tag do objeto").GetComponent<AudioManager>().Stop("nome do som");
             */
        /*
         para tocar um som
        FindObjectOfType<AudioManager>().Play("Nome Do Som");
        */
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found ");
            return;
        }

        s.source.Stop();
    }

    public void ChangeMusic()
    {


        if (num == 0)
        {

            FindObjectOfType<AudioManager>().Stop("Music01");
            num++;
        }
        else if(num == 1)
        {
            FindObjectOfType<AudioManager>().Stop("Music02");
            num--;
        }
    }

}
