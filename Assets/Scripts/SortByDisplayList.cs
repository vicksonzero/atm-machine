using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortByDisplayList : MonoBehaviour
{
    public float thickness = 1;

    private void OnTransformChildrenChanged()
        => SortChildren();

    private void OnValidate()
        => SortChildren();

    private void SortChildren()
    {
        if (TryGetComponent<Stack>(out var stack))
        {
            stack.stack2.SetAsLastSibling();
        }

        // print("SortChildren");
        thickness = 0f;
        foreach (var child in transform.Cast<Transform>().OrderBy(t => t.GetSiblingIndex()))
        {
            var childStack = child.GetComponent<SortByDisplayList>();
            var childPosition = child.localPosition;
            childPosition.z = -thickness;
            if (childStack) childStack.SortChildren();
            thickness += childStack != null ? childStack.thickness : 1;
            child.localPosition = childPosition;
        }
    }
}