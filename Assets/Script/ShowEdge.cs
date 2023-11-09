using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowEdge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject edge;

    private void OnEnable()
    {
        edge.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        edge.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        edge.SetActive(false);
    }

}
