using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour {

    /*
     * 
     * Entenda esse Array obstacles como uma caixa cheia de prefabs de obstáculos
     * com padrões diferentes de posicionamento.
     *
     */
    public GameObject[] obstacles;

    /*
     * Ponto em que os obstáculos irão surgir.
     * 
     */
    public Transform genPoint;

    public int count;

    public float newPos;

    private int randomNum;

    private float genTimeInterval = 0.5f;

    private GameObject obstacle;

	// Use this for initialization
	void Start () {
        CreateObstacle();
    }
	
    //Cria os obstáculos
	public void CreateObstacle() {
		obstacle = Instantiate(obstacles[Random.Range(0, 21)], genPoint.position, Quaternion.identity) as GameObject;
		Vector3 tmp = genPoint.position;
        tmp.x += 7f;
		genPoint.position = tmp;
        StartCoroutine(GenInInterval());
    }

    // gera obstáculos dado um intervalo de tempo
    IEnumerator GenInInterval() {
        yield return new WaitForSeconds(genTimeInterval);
        count += 1;
        CreateObstacle();
    }

}
