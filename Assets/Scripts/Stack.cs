using System;
using System.Linq;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public float thickness = 1;
    public ItemTypeSo itemType;
    public ItemTypeSo[] acceptsTypes;

    public Transform stack2;

    private void Start()
    {
        if (itemType == null)
        {
            InitItemType();
        }
    }

    // Update is called once per frame
    private void SortChildren()
    {
        if (stack2 != null) stack2.SetAsLastSibling();
        // print("SortChildren");
        thickness = 0f;
        foreach (Transform child in transform.Cast<Transform>().OrderBy(t => t.GetSiblingIndex()))
        {
            var stack = child.GetComponent<Stack>();
            var childPosition = child.localPosition;
            childPosition.z = -thickness;
            if (stack) stack.SortChildren();
            thickness += stack != null ? stack.thickness : 1;
            child.localPosition = childPosition;
        }
    }

    private void OnTransformChildrenChanged()
    {
        if (itemType == null)
        {
            InitItemType();
        }
        SortChildren();
    }

    private void InitItemType()
    {
        var item = GetComponentInChildren<Item>();
        if (item == null) return;
        itemType = item.itemType;
        if (acceptsTypes.Length == 0) acceptsTypes = new[] { itemType };
        if (stack2)
        {
            stack2.localPosition = item.stack2Transform.localPosition;
            stack2.localRotation = item.stack2Transform.localRotation;
        }
    }

    private void OnValidate()
    {
        SortChildren();
    }

    public void AddStack(Stack fromStack)
    {
        foreach (Transform child in fromStack.transform.Cast<Transform>().OrderBy(t => t.GetSiblingIndex()))
        {
            if (child != fromStack.stack2.transform)
                child.SetParent(transform);
        }
    }
}