using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{

    public int weight = 0;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Player"){
            weight += 1;
        }
        if (collision.gameObject.name == "SmallWeight") {
            weight += 3;
        }

        if (collision.gameObject.name == "BigWeight") {
            weight += 6;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            weight -= 1;
        }
        if (collision.gameObject.name == "SmallWeight") {
            weight -= 3;
        }
        if (collision.gameObject.name == "BigWeight") {
            weight -= 6;
        }
    }
}
