using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFadeDepression : MonoBehaviour {
    public Transform player;
    public Transform start;
    public Transform end;
    SpriteRenderer rend;

    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start() {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        float val = curve.Evaluate((start.position.y - player.position.y) / (start.position.y - end.position.y));
        rend.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), val);
    }
}
