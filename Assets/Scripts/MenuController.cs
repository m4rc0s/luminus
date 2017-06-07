using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    /*
     * script de controle do Menu
     */

    //Objetos a manipular configurados pelo Desenvolvedor no Editor
    public GameObject menuIndex;
    public GameObject aboutScreen;
    public GameObject creditsScreen;
    public GameObject cfgScreen;

    /*
     * Sliders da configuração de som
     */
    public Slider[] volSliders;

    void Start()
    {
        //consigura volume
        volSliders[0].value = AudioManager.instance.masterVol;
        volSliders[1].value = AudioManager.instance.musicVol;
        volSliders[2].value = AudioManager.instance.sfxVol;
    }

    // configura volume geral
    public void SetMasterVolume(float volume)
    {
        AudioManager.instance.SetVolume(volSliders[0].value, AudioManager.SoundOuts.Master);
    }

    // configura volume da música
    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetVolume(volSliders[1].value, AudioManager.SoundOuts.Music);
    }

    // configura volume dos efeitos
    public void SetSfxVolume(float volume)
    {
        AudioManager.instance.SetVolume(volSliders[2].value, AudioManager.SoundOuts.Sfx);
    }

    //Mostra tela de sobre
    public void ShowAbout() {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        menuIndex.SetActive(false);
        aboutScreen.SetActive(true);
    }

    /**
     * Método ShowAbout Versão2 - Exibe a tela de "Sobre" e esconde uma tela anterior
     * 
     * Utilizado na tela de créditos botão Voltar;
     * 
     */

    public void ShowAbout(GameObject from) {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        from.SetActive(false);
        aboutScreen.SetActive(true);
    }

    //mostra tela de créditos
    public void ShowCredits() {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        aboutScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    //mostra tela de configurações
    public void ShowConfig() {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        cfgScreen.SetActive(true);
        menuIndex.SetActive(false);
    }

    // vai para o menu
    public void GoMenuIndex(GameObject from) {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        from.SetActive(false);
        menuIndex.SetActive(true);
    }

    // carrega tela de classificações
    public void ShowHanking() {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        PlayGamesController.ShowRankingUI();
    }

    // carrega cenas
    public void LoadScene(string sceneName)
	{
        //Application.LoadLevel(levelName); Absoleto
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        SceneManager.LoadScene(sceneName);
    }

    // sai do jogo
    public void ExitGame()
    {
        AudioManager.instance.PlaySfx("Clicks", new Vector3(0, 0, 1));
        Application.Quit();
    }

}
