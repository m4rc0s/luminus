using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour {

   
    public RectTransform[] passiveUI;
    public LayerMask platformsLayer;

    private Rigidbody2D player;
    private Collider2D playerCollider;
    private Animator anim;
	private GameObject explosion;
	
	public GamePlayGUIController gpgCtrl;

    public float moveSpeed;
    public float jumpSpeed;
    public float secondJumpForce;

    public bool onGround;
    public bool canJump = true;
    public bool superPower = false;
    private bool canMove = false;
    private float targetDist = 87f;
    private float targetPosToScore = 0f;

    public int jumps = 0;
	public int bestScore = 0;
	public int score = 0;
	public int lives = 3;
	public int cltdIdeias = 0;
	public int cltdLuxes = 0;
	public int freeMindInterval = 0;
	public int mindsReleased = 0;

    void Start () {
		bestScore = PlayerPrefs.GetInt ("BestScore", 0);
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
		GameObject.Find ("TxtScore").GetComponent<Text> ().text = "0000";
		freeMindInterval = 10;
        StartCoroutine (FreePlayer());
	}

    void FixedUpdate () {

        onGround = Physics2D.IsTouchingLayers(playerCollider, platformsLayer);

        if (targetPosToScore < player.transform.position.x) {
            targetPosToScore = player.transform.position.x + .5f;
            score += 1;
        }

        if (targetDist < player.transform.position.x)
        {
            targetDist = 97f + ((player.transform.position.x / 100f) * 1.2f) * 100f;
            moveSpeed += .7f;
        }

        if (canMove) {
			player.AddForce(new Vector2(moveSpeed,player.velocity.y));
		}
        
        /**
         * Executa ações sobre o player se o mouse for clicado E não estiver sobre um objeto do tipo GUI
         * 
         */
        if (Input.GetMouseButtonDown(0) && onGround)
        {

            /* 
             * Adiciona força ao eixo x o que faz com que o personagem pule.
             * Nota: é importante usar o método AddForce do objeto para que a Unity preserve 
             * os comportamentos físicos da engine.
             * 
             * Ref.: http://answers.unity3d.com/questions/216204/objects-pass-through-each-other.html
             */

			player.AddForce(new Vector2(player.velocity.x, jumpSpeed),ForceMode2D.Impulse);

            canJump = true;
        }
        else if (Input.GetMouseButtonDown(0) && canJump)
        {

			player.AddForce(new Vector2(player.velocity.x, secondJumpForce),ForceMode2D.Impulse);

            canJump = false;
        }
        
        anim.SetBool("Grounded", onGround);

		GameObject.Find ("TxtScore").GetComponent<Text> ().text = score.ToString();
			
	}

    public bool IsMouseOverUI()
    {
        Vector2 mousePosition = Input.mousePosition;
        foreach (RectTransform elem in passiveUI)
        {
            if (!elem.gameObject.activeSelf)
            {
                continue;
            }
            Vector3[] worldCorners = new Vector3[4];
            elem.GetWorldCorners(worldCorners);

            if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x
               && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
            {
                return true;
            }
        }
        return false;
    }

	public int GetCltdIdeias(){
		return this.cltdIdeias;
	}

	public int GetCltdLuxes(){
		return this.cltdLuxes;
	}

	public int GetRlsdMinds(){
		return this.mindsReleased;
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        #region

        /*
         *  implementação lógica de perda e ganhos
         */

        if (collision.gameObject.tag == "Obstacle") {
			if (lives > 0) {
                if (!superPower)
                {
                    Destroy(GameObject.Find("Life" + lives.ToString()));
                    lives -= 1;
                }
			} else {
				if (score > bestScore) {
					bestScore = score;
					PlayerPrefs.SetInt ("BestScore", bestScore);
				}


                PlayGamesController.AddScoreToRanking(GPGSIds.leaderboard_ranking, (long)score);
                

				PlayerPrefs.SetInt ("CltdIdeias", cltdIdeias);
				PlayerPrefs.SetInt ("CltdLux", cltdLuxes);
				PlayerPrefs.SetInt ("MindsReleased", mindsReleased);
				gpgCtrl.GameOver ();
			}
		} else if (collision.gameObject.tag == "Ideia") {
			score += 1000;
			cltdIdeias += 1;
			StartCoroutine("ExplodeSynapse",collision.gameObject.transform);
			Destroy (collision.gameObject);
		} else if (collision.gameObject.tag == "Lux") {
			score += 10000;
			cltdLuxes += 1;

            //Jogador recebe um Mind
			if (cltdLuxes == freeMindInterval) {
				mindsReleased += 1;
				freeMindInterval = cltdLuxes + 10;
				//Debug.Log("Muito bem! Você libertou uma mente!");
			}
			StartCoroutine("ExplodeLux",collision.gameObject.transform);
			Destroy (collision.gameObject);
		}

        #endregion
    }

    //personagem não se movimenta até a excução desse método
	IEnumerator FreePlayer(){
		yield return new WaitForSeconds (.5f);
		canMove = true;
	}

    //explode Ideia - futuramente será chamado de Synapse
	IEnumerator ExplodeSynapse(Transform t){
		explosion = Instantiate (GameObject.FindGameObjectWithTag("SynapseParticles"), t.position, Quaternion.identity) as GameObject;
		explosion.GetComponent<ParticleSystem> ().Play ();

        //SOM
        AudioManager.instance.PlaySfx("Synapse", new Vector2(0,0));

		StartCoroutine("DestroyParticleSystem",explosion);
		yield return null;
	}

    //explode Lux - Item representando conhecimento 
	IEnumerator ExplodeLux(Transform t){
		explosion = Instantiate (GameObject.FindGameObjectWithTag("LuxParticles"), t.position, Quaternion.identity) as GameObject;
		explosion.GetComponent<ParticleSystem> ().Play ();

        //SOM
        AudioManager.instance.PlaySfx("Lux", new Vector2(0, 0));

        StartCoroutine("DestroyParticleSystem",explosion);
		yield return null;
	}

	IEnumerator DestroyParticleSystem(GameObject go){
		yield return new WaitForSeconds(.8f);
		Destroy (go);
	}

}
