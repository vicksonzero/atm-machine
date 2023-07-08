using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomAppearance : MonoBehaviour
{
    public float maxRotate = 1;
    public float maxDisplace = 1;

    private Tween moveTween;
    private Tween rotateTween;

    private void Start()
    {
        RandomizeLocal();
    }

    public void RandomizeLocal()
    {
        if (moveTween != null && moveTween.IsPlaying()) moveTween.Kill();

        Vector3 newPos = Random.insideUnitCircle * maxDisplace;
        newPos.z = transform.localPosition.z;
        moveTween = transform.DOLocalMove(newPos, 0.5f);

        var newAngle = Random.value * maxRotate;
        transform.DOLocalRotate(Vector3.forward * newAngle, 0.5f);
    }
}