using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public GameObject[] hairStyles;
    public SpriteRenderer body;

    public void SetBodyColor(Color color)
    {
        body.color = color;
    }

    public void SetHairStyle(int styleIndex)
    {
        for (var index = 0; index < hairStyles.Length; index++)
        {
            var hairStyle = hairStyles[index];
            hairStyle.SetActive(styleIndex == index);
        }
    }

}