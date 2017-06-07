using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayGUIController : MonoBehaviour {

	public GameObject pauseGUI;
	public GameObject statGUI;
	public PlayerController playerCtrl;
	public Animator anim;
	             
    void Start()
    {
		pauseGUI.SetActive (false);
		statGUI.SetActive (false);
		playerCtrl = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
		Time.timeScale = 1;
    }


    //esconde tela de pause
    public void HidePauseGUI()
    {
        pauseGUI.SetActive(false);
    }

    //mostra tela de pause
	public void ShowPauseGUI()
    {
        pauseGUI.SetActive(true);
    }

    //para o jogo
    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPauseGUI();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePauseGUI();
        }
    }

    //continua o jogo
    public void StillPlaying() {
        HidePauseGUI();
        Time.timeScale = 1;
    }

	//Tela de resumo do jogo - GameOver
	public void GameOver(){
		Time.timeScale = 0;
		ShowStatGUI ();
	}

    //esconde tela de GameOver
	public void HideStatGUI(){
		statGUI.SetActive (false);
	}

    //mostra tela de GameOver
	public void ShowStatGUI(){
		statGUI.SetActive (true);

        /*
         * Atualiza os textos exibidos na telas
         * com o resumo do jogo.
         */
		GameObject.Find ("TxtCltdIdeias").GetComponent<Text> ().text = playerCtrl.cltdIdeias.ToString();
		GameObject.Find ("TxtCltdLuxes").GetComponent<Text> ().text = playerCtrl.cltdLuxes.ToString();
		GameObject.Find ("TxtRlsdMinds").GetComponent<Text> ().text = playerCtrl.mindsReleased.ToString();
		GameObject.Find ("TxtScoreAchieved").GetComponent<Text> ().text = GameObject.Find ("TxtScore").GetComponent<Text> ().text;
		GameObject.Find ("TxtBestScore").GetComponent<Text> ().text = PlayerPrefs.GetInt ("BestScore", 0).ToString ();
	}

    //carrega cenas do jogos
    public void LoadScene(string sceneName)
	{
        //Application.LoadLevel(levelName); Absoleto
        SceneManager.LoadScene(sceneName);
    }

}
