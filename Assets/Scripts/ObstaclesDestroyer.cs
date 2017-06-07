using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ObstaclesDestroyer : MonoBehaviour
{

    //TODO
    /*
     * Rotina ainda não funcionando
     * 

    /*
     * destrói obstáculos quando colide com o ponto de destruição definido por um objeto
     */ 

    /*
     * um bloco é destruido a cada 6 segundos
     */
    private float timeInterval = 6.0f;

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Executou");
        StartCoroutine(DestroyInTime(collision.gameObject));    
    }
    IEnumerator DestroyInTime(GameObject go) {
        yield return new WaitForSeconds(timeInterval);
        Destroy(go);
    }
}
