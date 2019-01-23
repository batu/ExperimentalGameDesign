using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateTimer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision) {
        StartCoroutine(GameObject.Find("Obstruction").GetComponent<Timer>().Ending());
    }
}
