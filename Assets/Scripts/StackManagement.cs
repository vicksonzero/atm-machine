using DG.Tweening;
using UnityEngine;

public class StackManagement : MonoBehaviour
{
    public Stack desktop; // for its thickness
    private SpriteRenderer _renderer;


    void Start()
    {
        if (!_renderer) _renderer = GetComponentInChildren<SpriteRenderer>();
    }


    public bool ShowModal(Stack stack)
    {
        var pos = transform.position;
        pos.z = desktop.transform.position.z + desktop.thickness;
        transform.position = pos;

        _renderer.DOFade(1, 1000).From(0);
        return false;
    }

    public void HideModal()
    {
    }
}