using System.Linq;
using UnityEngine;

public class Droppable : MonoBehaviour
{
    [SerializeField]
    private GameObject dropUi;

    public virtual void ToggleDropUi(bool value)
    {
        print("ToggleDropUi");
        if (!dropUi)
        {
            dropUi = GetComponentsInChildren<Item>()
                .OrderByDescending(x => x.transform.GetSiblingIndex())
                .FirstOrDefault()?.dropUi;
        }
        if (dropUi) dropUi.SetActive(value);
    }

    private void OnEnable()
    {
        // do nothing
    }
}