using UnityEngine;

public class Droppable : MonoBehaviour
{

    [SerializeField]
    private GameObject dropUi;
    
    public virtual void ToggleDropUi(bool value)
    {
        if (!dropUi) dropUi = GetComponentInChildren<Item>().dropUi;
        dropUi.SetActive(value);
    }
    private void OnEnable()
    {
        // do nothing
    }
}