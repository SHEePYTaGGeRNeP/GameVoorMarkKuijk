using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;

public class Card : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Vector3 BeginPosition;
    public string CurrentColumn;
    
    private Task task;
    private MouseManager mm;
    private GameObject canvas;

    void Awake()
    {
        mm = GameObject.Find("MM").GetComponent<MouseManager>();
        canvas = GameObject.Find("Canvas");
    }

    void Start()
    {
        task = this.GetComponent<Task>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (task.State != TaskState.Archive)
        {
            this.transform.parent = canvas.transform;
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Input.mousePosition.x + 10, Input.mousePosition.y - 10, Input.mousePosition.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckNextState();
    }

    public void CheckNextState()
    {
        TaskState state = task.State;

        if (state == TaskState.Todo && mm.CurrentHover == "DoingPanel")
        {
            task.NextState();
            GUIManager.instance.PlaceCard(this.gameObject, TaskState.InProgress, 0);
            Debug.Log("yay!");
        }
        else if (state == TaskState.InProgress && mm.CurrentHover == "ReviewPanel")
        {
            task.NextState();
            GUIManager.instance.PlaceCard(this.gameObject, TaskState.Review, 0);
            Debug.Log("yay!");
        }
        else if (state == TaskState.Review && mm.CurrentHover == "ArchivePanel")
        {
            task.NextState();
            GUIManager.instance.PlaceCard(this.gameObject, TaskState.Archive, 0);
            GameManager.INSTANCE.Archive();
            Debug.Log("yay!");
        }
        else
        {
            GUIManager.instance.PlaceCard(this.gameObject, state, 0);
            Debug.Log("nay!");
        }

        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
