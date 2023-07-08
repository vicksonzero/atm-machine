using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemTypeSo itemType;
    public GameObject dropUi;

    private void Start()
    {
        dropUi.gameObject.SetActive(false);
    }

}
