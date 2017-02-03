using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Column : MonoBehaviour, IPointerEnterHandler
{
    private MouseManager mm;

    void Awake()
    {
        mm = GameObject.Find("MM").GetComponent<MouseManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mm.CurrentHover = transform.name;
    }
}
