using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public enum SoundOuts { Master, Sfx, Music };

    public float masterVol;
    public float sfxVol;
    public float musicVol;
    int playingTrackIndex;

    Transform audioListener;
    Transform playerTransform;
    GameObject songToPlay;

    AudioLibrary audioLibrary;
    MusicManager musicManager;
    Dictionary<string, AudioSource> musicListInstance;

    public static AudioManager instance;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioLibrary = GetComponent<AudioLibrary>();
            musicManager = GetComponent<MusicManager>();
            musicListInstance = new Dictionary<string, AudioSource>();

            foreach (MusicManager.Track gm in musicManager.tracks)
            {
                try
                {
                    AudioSource stp = new GameObject(gm.clip.name).AddComponent<AudioSource>();

                    /*
                     * Esses objetos stp: abreviação para Song To Play
                     * não deverão ser destruídos quando trocar de cena;
                     * Eles são as instancias de AudioSource que terão o volume alterado
                     * de acordo com as opções do usuário
                     * 
                     * Para isso será utilizado o método DontDestroyOnLoad() da Unity,
                     * que faz o trabalho de manter o objeto (GameObject) "Vivo" atravéz das cenas.
                     * 
                     * */
                    DontDestroyOnLoad(stp);

                    stp.clip = gm.clip;

                    stp.volume = musicVol * masterVol;

                    musicListInstance.Add(gm.trackName, stp);


                }
                catch (Exception e)
                {
                    Debug.Log("Something horrible was happened!");
                    Debug.Log(e.Message);
                }

            }

            audioListener = FindObjectOfType<AudioListener>().transform;

            if (FindObjectOfType<PlayerController>() != null) {
                playerTransform = FindObjectOfType<PlayerController>().transform;
            }

            masterVol = PlayerPrefs.GetFloat("MasterVolume", 1);
            sfxVol = PlayerPrefs.GetFloat("SfxVolume", 1);
            musicVol = PlayerPrefs.GetFloat("MusicVolume", 1);

        }

    }

    void Update()
    {
        if (playerTransform != null) {
            audioListener.position = playerTransform.position;
        }
    }

    public void SetVolume(float vol, SoundOuts outs)
    {
        switch (outs)
        {
            case SoundOuts.Master:
                masterVol = vol;
                break;
            case SoundOuts.Sfx:
                sfxVol = vol;
                break;
            case SoundOuts.Music:
                musicVol = vol;
                break;
        }

        foreach (KeyValuePair<string, AudioSource> t in musicListInstance) {
            t.Value.volume = musicVol * masterVol;
        }

        PlayerPrefs.SetFloat("MasterVolume", masterVol);
        PlayerPrefs.SetFloat("SfxVolume", sfxVol);
        PlayerPrefs.SetFloat("MusicVolume", musicVol);

    }

    /*
     * Versão 1 do Método PlayMusic, não implementa Fader entre a troca de músicas
     * 
     * */
    public void PlayMusic(string trackName) {
        if (musicListInstance.ContainsKey(trackName)) {
            musicListInstance[trackName].loop = true;
            musicListInstance[trackName].Play();
        }
    }

    public void StopMusic(string trackName) {
        if (musicListInstance.ContainsKey(trackName))
        {
            musicListInstance[trackName].Stop();
        }
    }
        
    public void PlaySfx(AudioClip clip, Vector2 pos) {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVol * masterVol);
    }

    public void PlaySfx(string soundName, Vector2 pos) {
        PlaySfx(audioLibrary.GetClipByName(soundName), pos);
    }

    /**
     * 
     * Se der implementar fade
     * 
     **/

    /*IEnumerator AnimateMusicCrossFade(float duration) {
        float percent = 0f;

        while (percent < 1) {
            percent += Time.deltaTime * 1 / duration;
            //gameMusics[playingTrackIndex].volume = Mathf.Lerp(0, musicVol * masterVol, percent);
            //gameMusics[1-playingTrackIndex].volume = Mathf.Lerp(musicVol * masterVol, 0, percent);
            yield return null;
        }
        
    }*/

}