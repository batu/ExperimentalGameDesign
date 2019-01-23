using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    // Start is called before the first frame update

    int totalWeight = 0;
    public Holder holder1;
    public Holder holder2;
    public float speed = 0.1f;
    public float treshold = 2.5f;

    bool notStarted = true;

    public GameObject wall; 

    bool isBalanced() {
        return (holder1.weight - holder2.weight == 0 && holder1.weight != 0 && holder2.weight != 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (isBalanced() && notStarted) {
            notStarted = false;
            StartCoroutine(StablizeTheScales());
        }  
    }

    IEnumerator StablizeTheScales() {
        print("I have started stabilization process.");
        GetComponent<Rigidbody2D>().freezeRotation = true;
        while (Mathf.Abs(180 - transform.rotation.eulerAngles.z) > treshold) {
            print("In the while.");
            var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -180);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * speed);
            yield return null;
        }
        gameObject.isStatic = true;
        wall.SetActive(false);
    }
}
