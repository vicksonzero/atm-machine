using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemTypeSo itemType;
    public GameObject dropUi;
    
    void Start()
    {
        dropUi.gameObject.SetActive(false);
    }

}
