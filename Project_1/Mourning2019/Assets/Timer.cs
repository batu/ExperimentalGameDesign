using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    SpriteRenderer obstruction;
    public SpriteRenderer title;
    // Start is called before the first frame update
    void Start()
    {
        obstruction = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Ending() {
        print("In here.");
        yield return new WaitForSeconds(30);
        for (float i = 0; i <= 1; i += Time.deltaTime / 10) {
            // set color with i as alpha
            print("In loop1.");
            obstruction.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        for (float i = 0; i <= 1; i += Time.deltaTime / 4) {
            // set color with i as alpha
            title.color = new Color(1, 1, 1, i);
            yield return null;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
