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


    void ExplosionForce(Vector3 center, float radius, float force) {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach(Collider2D col in hitColliders) {
            if(col.gameObject.layer == breakableLayer) { 
            Rigidbody2D theRB = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = col.transform.position - center;
            theRB.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == breakableLayer) {
            ExplosionForce(collision.transform.position, 6f, 5f);
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }
}
