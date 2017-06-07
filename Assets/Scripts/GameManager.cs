using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    /*
     * Esse script será implementado de acordo com o desenvolvimento do projeto do jogo.
     * Deverá ser útil para encapsular tudo o que for útil em mais 
     * de um contexto no jogo;
     */

    public string gameVersion;


    static public GameManager instance { get; private set; }

    void Awake() {

        if (instance) Destroy(instance);

        instance = this;

        DontDestroyOnLoad(instance);

        //GameObject.Find("TxtVersion").GetComponent<Text>().text = gameVersion;
    }

}
