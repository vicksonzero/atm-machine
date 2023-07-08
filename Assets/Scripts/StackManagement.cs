using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class StackManagement : MonoBehaviour
{
    public Stack currentStack;
    public Vector3 currentStackStartPos;
    public Stack desktop; // for its thickness
    [SerializeField]
    private CanvasGroup backdrop;

    private Tween backdropTween;

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

    void Start()
    {
        if (!backdrop) backdrop = GetComponentInChildren<CanvasGroup>();
        backdrop.alpha = 0;
        backdrop.GetComponentInChildren<Collider2D>().enabled = false;
    }


    public bool ShowModal(Stack stack)
    {
        if (currentStack != null) return false;
        currentStack = stack;
        currentStackStartPos = currentStack.transform.position;

        var myTransform = transform;

        var pos = myTransform.position;
        pos.z = desktop.transform.position.z - desktop.thickness;
        myTransform.position = pos;

        var stackTransform = stack.transform;
        stackTransform.SetParent(myTransform);

        var stackNewPos = stackTransform.localPosition;
        stackNewPos.z = 0;
        stackTransform.localPosition = stackNewPos;
        stack.transform.DOLocalMove(Vector3.zero, stackMoveInDuration);

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
        if (currentStack == null) return;
        print("HideModal");
        currentStack.transform.SetParent(desktop.transform);
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
    }
}