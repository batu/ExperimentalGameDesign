using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGround : MonoBehaviour
{
    public GameObject fakeGround;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision) {
        fakeGround.GetComponent<Collider2D>().enabled = false;
        fakeGround.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(SlowMode());
    }

    IEnumerator SlowMode() {
        Time.timeScale = 0.2f;
        float temp = Time.fixedDeltaTime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSecondsRealtime(3.5f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
