using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BPlayerInput : MonoBehaviour
{
    [SerializeField]
    private InputAction pointerClick;

    private Camera _mainCamera;
    private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField]
    private float minDragDist = 2;
    [SerializeField]
    private bool isDragging;
    [SerializeField]
    private float minDropDist = 2;

    private Droppable droppableTarget;

    [SerializeField]
    private StackManagement stackManagement;


    private void Awake()
    {
        _mainCamera = Camera.main;
        if (!stackManagement) stackManagement = FindObjectOfType<StackManagement>();
        stackManagement.enabled = false;
    }

    private void OnEnable()
    {
        pointerClick.Enable();
        pointerClick.performed += PointerPressed;
    }

    private void OnDisable()
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
            if (draggable != null && draggable.enabled)
            {
                StartCoroutine(DoDragCycle(draggable));
            }

            if (stackManagement.enabled)
            {
                stackManagement.HandlePointerPressed(context, hit, pointerClick);
            }
        }
    }

    private IEnumerator DoDragCycle(Draggable draggable)
    {
        #region Start DoDragCycle

        print("Start DoDragCycle");
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

        #region Update DoDragCycle

        do
        {
            var pointerPosition = _mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            var dist = Vector2.Distance(draggable.transform.position - offset, pointerPosition);
            // print($"DoDragCycle Prep {dist}");
            if (!isDragging && dist < minDragDist)
            {
                // do nothing
            }
            else
            {
                if (!isDragging) print("Start dragging");
                isDragging = true;
                print("Update DoDragCycle");
                var point = offset + pointerPosition;


                if (hasTargetJoint)
                {
                    targetJoint2D.target = point;
                }
                else
                {
                    // point.z = draggable.transform.position.z;
                    draggable.transform.position = point;
                }

                OnDragging(draggable, droppables);
            }

            yield return hasTargetJoint
                ? _waitForFixedUpdate // for physics
                : null;
        } while (pointerClick.ReadValue<float>() != 0);

        #endregion

        #region End DoDragCycle

        print("End DoDragCycle");
        if (hasTargetJoint)
        {
            targetJoint2D.enabled = false;
        }
        if (droppableTarget != null)
        {
            droppableTarget.ToggleDropUi(false);
            DoDrop(draggable, droppableTarget);
        }
        droppableTarget = null;
        if (!isDragging)
        {
            DoClick(draggable);
        }
        isDragging = false;

        #endregion
    }

    private void OnDragging(Draggable draggable, Droppable[] droppables)
    {
        var closestDroppable = GetClosest2D(draggable, droppables, out var dist);

        // print($"OnDragging {dist}");
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

    private void DoDrop(Draggable draggable, Droppable droppable)
    {
        var fromStack = draggable.GetComponent<Stack>();
        var toStack = droppable.GetComponent<Stack>();

        if (fromStack && toStack)
        {
            toStack.AddStack(fromStack);
            Destroy(fromStack.gameObject);
        }
    }

    private Droppable GetClosest2D(Draggable draggable, Droppable[] droppables, out float minDist)
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

    private void DoClick(Draggable draggable)
    {
        print("DoClick");
        stackManagement.enabled = true;
        stackManagement.ShowModal(draggable.GetComponent<Stack>());
        draggable.enabled = false;
    }
}