using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class StackManagement : MonoBehaviour
{
    public Stack currentStack;
    public Vector3 currentStackStartPos;
    public Stack desktop; // for its thickness
    [SerializeField]
    private CanvasGroup backdrop;

    private bool _isClickingItem;
    private bool _isDragging;

    [SerializeField]
    private float minDragDist = 2;

    private Tween backdropTween;

    [SerializeField]
    private Stack stackPrefab;

    [Header("Params for ShowModal")]
    [SerializeField]
    [Tooltip("In seconds")]
    public float stackMoveInDuration = 0.5f;
    [SerializeField]
    [Tooltip("In seconds")]
    public float backdropFadeInDuration = 1f;

    [Header("Params for HideModal")]
    [SerializeField]
    [Tooltip("In seconds")]
    public float stackMoveOutDuration = 0.5f;
    [SerializeField]
    [Tooltip("In seconds")]
    public float backdropFadeOutDuration = 0.5f;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (!backdrop) backdrop = GetComponentInChildren<CanvasGroup>();
        backdrop.alpha = 0;
        backdrop.GetComponentInChildren<Collider2D>().enabled = false;
    }

    private void OnEnable()
    {
        // do nothing
    }


    public bool ShowModal(Stack stack)
    {
        if (currentStack != null) return false;
        currentStack = stack;
        currentStackStartPos = currentStack.transform.position;

        foreach (var randomAppearance in currentStack.GetComponentsInChildren<RandomAppearance>())
        {
            randomAppearance.RandomizeLocal();
        }

        var myTransform = transform;

        var pos = myTransform.position;
        pos.z = desktop.transform.position.z - desktop.thickness;
        myTransform.position = pos;

        var stackTransform = stack.transform;
        stackTransform.SetParent(myTransform);

        var stackNewPos = stackTransform.localPosition;
        stackNewPos.z = -1;
        stackTransform.localPosition = stackNewPos;
        stackNewPos.x = 0;
        stackNewPos.y = 0;
        stack.transform.DOLocalMove(stackNewPos, stackMoveInDuration);

        if (stack.TryGetComponent<Draggable>(out var draggable))
        {
            draggable.enabled = false;
        }

        if (backdropTween != null && backdropTween.IsPlaying()) backdropTween.Kill();
        backdropTween = backdrop.DOFade(1, backdropFadeInDuration).From(0);
        backdrop.GetComponentInChildren<Collider2D>().enabled = true;
        return true;
    }

    public void HideModal()
    {
        if (_isClickingItem) return;
        if (currentStack == null) return;
        print("HideModal");
        currentStack.transform.SetParent(desktop.transform);
        if (currentStack.stack2.childCount > 0)
        {
            SplitStack(currentStack);
        }
        if (currentStack.transform.childCount == 1
            && currentStack.transform.GetChild(0) == currentStack.stack2.transform)
        {
            DestroyStack(currentStack);
        }
        currentStack.transform.DOMove(currentStackStartPos, stackMoveOutDuration)
            .SetOptions(AxisConstraint.X | AxisConstraint.Y);

        if (currentStack.TryGetComponent<Draggable>(out var draggable))
        {
            draggable.enabled = true;
        }
        if (backdropTween != null && backdropTween.IsPlaying()) backdropTween.Kill();
        backdropTween = backdrop.DOFade(0, backdropFadeOutDuration);
        backdrop.GetComponentInChildren<Collider2D>().enabled = false;

        currentStack = null;
        enabled = false;
    }

    public void HandlePointerPressed(InputAction.CallbackContext context, RaycastHit2D hit, InputAction pointerClick)
    {
        if (currentStack == null) return;
        print("HandlePointerPressed");
        var item = hit.collider.GetComponent<Item>();
        if (item != null && item.enabled)
        {
            print("HandlePointerPressed item");
            StartCoroutine(DoClickCycle(pointerClick, item));
        }
    }

    private IEnumerator DoClickCycle(InputAction pointerClick, Item item)
    {
        #region Start DoDrag

        print("Start DoDrag");
        var offset = item.transform.position
                     - _mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());


        _isClickingItem = true;
        var stack2 = item.GetComponentInParent<SplitStack>();
        var stack = item.GetComponentInParent<Stack>();
        if (stack2 != null && stack2.enabled)
        {
            print("HandlePointerPressed count back");
            // count back
            var itemTransform = item.transform;
            itemTransform.SetParent(currentStack.transform, true);
            itemTransform.SetAsLastSibling();
            item.transform.DOLocalMove(Vector3.zero, stackMoveInDuration)
                .SetOptions(AxisConstraint.X);
            item.transform.DOLocalMove(Vector3.zero, stackMoveInDuration)
                .SetOptions(AxisConstraint.Y);
        }
        else if (stack != null && stack.enabled)
        {
            print("HandlePointerPressed count forward");
            // count forward
            var itemTransform = item.transform;
            itemTransform.SetParent(stack.stack2, true);
            itemTransform.SetAsLastSibling();
            item.transform.DOLocalMove(Vector3.zero, stackMoveInDuration)
                .SetOptions(AxisConstraint.X);
            item.transform.DOLocalMove(Vector3.zero, stackMoveInDuration)
                .SetOptions(AxisConstraint.Y);
        }
        else
        {
            _isClickingItem = false;
        }

        #endregion

        #region Update DoDrag

        do
        {
            var pointerPosition = _mainCamera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            var dist = Vector2.Distance(item.transform.position - offset, pointerPosition);
            // print($"DoDrag Prep {dist}");
            if (!_isDragging && dist < minDragDist)
            {
                // do nothing
            }
            else
            {
                if (!_isDragging) print("Start dragging");
                _isDragging = true;
                print("Update DoDrag");
            }

            yield return null;
        } while (pointerClick.ReadValue<float>() != 0);

        #endregion

        #region End DoDrag

        print("End DoDrag");

        _isDragging = false;
        _isClickingItem = false;

        #endregion
    }

    private void SplitStack(Stack stack)
    {
        print($"SplitStack {stack.stack2.transform.childCount}");
        var firstChild = stack.stack2.transform.GetChild(0);
        var newStack = Instantiate(stackPrefab, firstChild.position, Quaternion.identity, desktop.transform);

        foreach (Transform child in stack.stack2.transform.Cast<Transform>().OrderBy(t => t.GetSiblingIndex()))
        {
            child.SetParent(newStack.transform);
        }
    }

    private void DestroyStack(Stack stack)
    {
        print($"DestroyStack {stack.transform.childCount - 1}");
        Destroy(stack.gameObject);
    }
}