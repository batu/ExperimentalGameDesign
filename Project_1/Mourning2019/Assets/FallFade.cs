using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFade : MonoBehaviour
{
    public Transform player;
    public Transform start;
    public Transform end;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        print((start.position.y - player.position.y) / (start.position.y - end.position.y));
        rend.color = Color.Lerp(new Color(0,0,0, 0), new Color(0,0,0,1), (start.position.y - player.position.y) / (start.position.y - end.position.y - 40));
    }
}
