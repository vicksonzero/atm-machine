using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountPaper : MonoBehaviour
{
    public Text accountNumberLabel;
    // Start is called before the first frame update
    void Start()
    {
        accountNumberLabel.text = $"AB-{Random.Range(100, 200):0000}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
