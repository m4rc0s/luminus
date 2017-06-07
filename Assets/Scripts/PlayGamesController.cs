using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGamesController : MonoBehaviour {

    /*
     * Implementações do Google Play Games Service
     */

    public static PlayGamesController instance { get; private set; }

	void Start () {

        /*
         * Singleton
         */
        if (instance) {
            Destroy(instance);
        }

        instance = this;

        //Configura biblioteca do google
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
		
	}

    //Autenticação
    void SignIn() {
        Social.localUser.Authenticate((bool success) => { });
    }

    //Salva pontuação na classificação
    public static void AddScoreToRanking(string resourceId, long score) {
        Social.ReportScore(score, resourceId, (bool success) => { });
    }

    public static void ShowRankingUI()
    {
        Social.ShowLeaderboardUI();
    }

}
