using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{

    //Lista de listas de Audios
    public PlayList[] playLists;

    /*
     * Dicionário de listas de Audios
     * 
     * Chave nome da lista;
     * VAlor Array de AudioClips; (os audios que poderão ser executados)
     * */

    Dictionary<string, AudioClip[]> listDictionary = new Dictionary<string, AudioClip[]>();

    private void Awake()
    {
        /*
         * Percorre as listas criadas pelo desenvolvedor no Editor
         * e adiciona ao dicionário
         */

        foreach (PlayList pl in playLists)
        {
            listDictionary.Add(pl.playListId, pl.tracks);
        }
    }

    /*
     * Este método seleciona uma lista de Audios pelo seu nome
     * e retorna aleatóriamente um Audio contido na lista 
     * 
     * */
    public AudioClip GetClipByName(string name)
    {
        if (listDictionary.ContainsKey(name))
        {
            AudioClip[] audios = listDictionary[name];
            return audios[Random.Range(0, audios.Length)];
        }
        return null;
    }

    /*
     * Objeto utilizado para representar uma Lista de Audios;
     *  
     *  string playLisId (o nome da playlist dado no Editor)
     *  tracks AudioClip[] (os audio clips indicados no Editor)
     */

    [System.Serializable]
    public class PlayList
    {
        public string playListId;
        public AudioClip[] tracks;
    }
}
