using System;
using System.Linq;
using UnityEngine;

public class Stack : MonoBehaviour
{
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


    private void OnTransformChildrenChanged()
    {
        if (itemType == null)
        {
            InitItemType();
        }
    }

    private void InitItemType()
    {
        var item = GetComponentInChildren<Item>();
        if (item == null) return;
        itemType = item.itemType;
        name = $"{itemType.name} stack";
        if (acceptsTypes.Length == 0) acceptsTypes = new[] { itemType };
        if (stack2)
        {
            stack2.localPosition = item.stack2Transform.localPosition;
            stack2.localRotation = item.stack2Transform.localRotation;
        }
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