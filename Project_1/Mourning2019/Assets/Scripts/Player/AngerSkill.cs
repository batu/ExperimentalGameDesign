using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerSkill : MonoBehaviour
{
    Camera mainCamera;
    public float boltSpeed = 15f;

    float shootingRate = 0.5f;
    float shootingCooldown = 0.0f;

    public GameObject angerBoltPrefab;
    public float spawnDistance = 1f;

    public float recoilStrenght = 5f;
    Rigidbody2D rb;

    public bool explosion;

    AngerBoltBehavior boltBehavior;
    void Start()
    {
        boltBehavior = angerBoltPrefab.GetComponent<AngerBoltBehavior>();
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }


    void FireAngerBolt() {
        if (Input.GetButtonDown("Fire1") && shootingCooldown <= 0) {
            shootingCooldown = shootingRate;

            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 targetWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            ShootAngerBoltAtTarget(targetWorldPosition);
            ApplyRecoil(targetWorldPosition);
        }

        if (shootingCooldown > 0) {
            shootingCooldown -= Time.deltaTime;
        }
    }

    void ShootAngerBoltAtTarget(Vector3 targetLocation) {
        Vector3 direction = (targetLocation - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
        GameObject boltInstance = Instantiate(angerBoltPrefab, transform.position + spawnDistance * direction, rotation) as GameObject;
        boltInstance.AddComponent<AngerBoltBehavior>().SetValuesFromAngerSkill(this);
    }

    void ApplyRecoil(Vector3 targetLocation) {
        Vector3 direction = (targetLocation - transform.position).normalized;
        rb.AddForce(-direction * recoilStrenght, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        FireAngerBolt();

    }
}
