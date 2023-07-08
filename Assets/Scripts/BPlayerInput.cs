using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BPlayerInput : MonoBehaviour
{
    [FormerlySerializedAs("mouseClick")]
    [SerializeField]
    private InputAction pointerClick;

    private Camera _mainCamera;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField]
    private float minDropDist = 1;

    private Droppable droppableTarget;


    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void OnEnable()
    {
        pointerClick.Enable();
        pointerClick.performed += PointerPressed;
    }

    void OnDisable()
    {
        pointerClick.performed -= PointerPressed;
        pointerClick.Disable();
    }

    private void PointerPressed(InputAction.CallbackContext context)
    {
        print("PointerPressed");
        var point = _mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
        var hit = Physics2D.Raycast(point, Vector2.zero);
        if (hit.collider != null)
        {
            var draggable = hit.collider.GetComponentInParent<Draggable>();
            if (draggable != null)
                StartCoroutine(DoDrag(draggable));
        }
    }

    private IEnumerator DoDrag(Draggable draggable)
    {
        #region Start DoDrag

        print("Start DoDrag");
        var offset = draggable.transform.position
                     - _mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());

        var hasTargetJoint = draggable.TryGetComponent<TargetJoint2D>(out var targetJoint2D);
        var hasStack = draggable.TryGetComponent<Stack>(out var stack);
        if (hasTargetJoint)
        {
            // targetJoint2D.anchor = offset;
            targetJoint2D.enabled = true;
        }

        draggable.transform.SetAsLastSibling();

        var droppables = !hasStack
            ? new Droppable[] { }
            : FindObjectsOfType<Droppable>()
                .Where(x => x.GetComponent<Stack>().acceptsTypes.Contains(stack.itemType))
                .ToArray();

        #endregion

        #region Update DoDrag

        do
        {
            print("Update DoDrag");
            var point = offset + _mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());

            if (hasTargetJoint)
            {
                targetJoint2D.target = point;
                OnDragging(draggable, droppables);
                yield return _waitForFixedUpdate; // for physics
            }
            else
            {
                // point.z = draggable.transform.position.z;
                draggable.transform.position = point;
                OnDragging(draggable, droppables);

                yield return null;
            }
        } while (pointerClick.ReadValue<float>() != 0);

        #endregion

        #region End DoDrag

        print("End DoDrag");
        if (hasTargetJoint)
        {
            targetJoint2D.enabled = false;
        }
        if (droppableTarget != null)
        {
            droppableTarget.ToggleDropUi(false);
            Drop(draggable, droppableTarget);
        }
        droppableTarget = null;

        #endregion
    }

    private void OnDragging(Draggable draggable, Droppable[] droppables)
    {
        var closestDroppable = GetClosest2D(draggable, droppables, out var dist);

        print($"OnDragging {dist}");
        if (dist < minDropDist)
        {
            if (droppableTarget != closestDroppable)
            {
                if (droppableTarget != null) droppableTarget.ToggleDropUi(false);
                droppableTarget = closestDroppable;
                droppableTarget.ToggleDropUi(true);
            }
        }
        else
        {
            if (droppableTarget != null) droppableTarget.ToggleDropUi(false);
            droppableTarget = null;
        }
    }

    private void Drop(Draggable draggable, Droppable droppable)
    {
        var fromStack = draggable.GetComponent<Stack>();
        var toStack = droppable.GetComponent<Stack>();

        if (fromStack && toStack)
        {
            toStack.AddStack(fromStack);
            Destroy(fromStack.gameObject);
        }
    }

    Droppable GetClosest2D(Draggable draggable, Droppable[] droppables, out float minDist)
    {
        Droppable tMin = null;
        minDist = Mathf.Infinity;
        Vector2 currentPos = draggable.transform.position;
        foreach (var t in droppables)
        {
            if (t.gameObject == draggable.gameObject) continue;

            var dist = Vector2.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}