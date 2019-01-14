using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerBoltBehavior : MonoBehaviour {

    float boltSpeed = 15f;
    float despawnTimer = 25f;

    GameObject gameManager;
    Rigidbody2D rb;

    AngerSkill angerSkill;
    LayerMask breakableLayer = 11;

    public void SetValuesFromAngerSkill(AngerSkill angerSkillScript) {
        angerSkill = angerSkillScript;
        boltSpeed = angerSkill.boltSpeed;
    }

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * boltSpeed;

        Destroy(gameObject, despawnTimer);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == breakableLayer) {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }
}
