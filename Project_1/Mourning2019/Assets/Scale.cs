using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    // Start is called before the first frame update

    int totalWeight = 0;
    public Holder holder1;
    public Holder holder2;

    bool notStarted = true;
    void Start()
    {
        
    }

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
        yield return null;
    }
}
