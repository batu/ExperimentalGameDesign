using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyLeaver : MonoBehaviour
{

    public GameObject denial;
    public GameObject anger;
    public GameObject bargaining;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.transform.name == "DenialActivate") {
            GetComponent<PlayerMovement>().frozen = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            denial.SetActive(true);
        }
        if (collision.transform.name == "AngerActivate")
            anger.SetActive(true);
        if (collision.transform.name == "BargainingActivate")
            bargaining.SetActive(true);
    }
}
