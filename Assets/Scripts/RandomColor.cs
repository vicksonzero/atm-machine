using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomColor : MonoBehaviour
{
    public float minHue;
    public float maxHue;
    public float minSat;
    public float maxSat;
    public float minVal;
    public float maxVal;
    public float minAlpha;
    public float maxAlpha;

    // Start is called before the first frame update
    void Start()
    {
        var color = Color.HSVToRGB(
            Random.Range(minHue, maxHue),
            Random.Range(minSat, maxSat),
            Random.Range(minVal, maxVal));
        color.a = Random.Range(minAlpha, maxAlpha);
        GetComponent<SpriteRenderer>().color = color;
    }

}