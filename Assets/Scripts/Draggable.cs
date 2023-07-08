using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class Draggable : MonoBehaviour
{
    private bool dragging;
    private Vector3 offset;
    private float timeCount = 0.0f;
    //
    // private void OnMouseDown()
    // {
    //     print("OnMouseDown");
    //     offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     dragging = true;
    // }
    //
    // private void FixedUpdate()
    // {
    //     if (dragging)
    //     {
    //         print("dragging");
    //         transform.position = offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     }
    // }
}