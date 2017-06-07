using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 lastPlayerPos;
    private float distanceToMove;

	void Start ()
    {
        
        //Pega o personagem para utilizar sua posição
        player = GameObject.Find ("Luminus");

        //Salva a última posição do personagem
        lastPlayerPos = player.transform.position;

        //atribui a última posição do personagem à camera
		transform.position = new Vector3 (lastPlayerPos.x, 0, -1);//player.transform.position;

	}

    void FixedUpdate()
    {
        /*
         * Tira a diferença da última posição do personagem
         * e da posição atual para calcular o quanto a camera irá se movimentar no eixo x
         * fazendo que siga o personagem
         */
        distanceToMove = player.transform.position.x - lastPlayerPos.x;
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, -1f);
        lastPlayerPos = player.transform.position;
    }

    void SetColor()
    {

    }
}
