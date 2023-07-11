using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemTypeSo itemType;
    public GameObject dropUi;
    public Transform stack2Transform;


    private void Start()
    {
        dropUi.gameObject.SetActive(false);
    }

}