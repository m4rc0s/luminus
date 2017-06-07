using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    /*
     * script para manipulação de lista de músicas
     * 
     */

    //Array das músicas
    public Track[] tracks;

    /*
     * Dicionário de dados para armazenar objetos do tipo Track
     * 
     * a chave é do tipo string e é o nome da música
     * o valor é um AudioClip
     *   
     */
    Dictionary<string, AudioClip> listDictionary = new Dictionary<string, AudioClip>();

    /*
     * Música a ser reproduzida;
     */
    AudioClip clipToPlay = null;

    private void Awake()
    {
        //Carrega musicas configuras pelo desenvolvedor no Editor.
        foreach (Track t in tracks)
        {
            listDictionary.Add(t.trackName, t.clip);
        }
    }

    #region

    /*
     * Implementação para utilizar OnLevelFinishedLOading.
     */

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;

    }
    #endregion

    /*
     * Troca as músicas com base na mudanção de Cena.
     */

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            AudioManager.instance.StopMusic("MusicaGamePlay");
            AudioManager.instance.PlayMusic("MusicaMenu");
        }
        else if (scene.name == "Main")
        {
            AudioManager.instance.PlayMusic("MusicaGamePlay");
            AudioManager.instance.StopMusic("MusicaMenu");
        }

    }

    // pega uma música da lista definida no editor por nome
    public AudioClip GetMusicByName(string name)
    {
        if (listDictionary.ContainsKey(name))
        {
            AudioClip music = listDictionary[name];
            return music;
        }
        return null;
    }

    // define objeto Track útil para configuração das músicas no Editor;
    [System.Serializable]
    public class Track
    {
        public string trackName;
        public AudioClip clip;
    }

}
