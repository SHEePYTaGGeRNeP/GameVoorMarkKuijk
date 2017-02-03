using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts;

public class Card : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Vector3 BeginPosition;
    public string CurrentColumn;

    private Task task;
    private MouseManager mm;

    void Awake()
    {
        mm = GameObject.Find("MM").GetComponent<MouseManager>();
    }

    void Start()
    {
        task = this.GetComponent<Task>();
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

        if (state == TaskState.Todo && mm.CurrentHover == "TaskPanel" || mm.CurrentHover == "DoingPanel")
        {
            task.NextState();
            Debug.Log("yay!");
        }
        else if (state == TaskState.InProgress && mm.CurrentHover == "DoingPanel" || mm.CurrentHover == "ReviewPanel")
        {
            task.NextState();
            Debug.Log("yay!");
        }
        else if (state == TaskState.Review && mm.CurrentHover == "ReviewPanel" || mm.CurrentHover == "ArchiverenPanel")
        {
            task.NextState();
            Debug.Log("yay!");
        }
        else if (state == TaskState.Archive && mm.CurrentHover == "ArchiverenPanel")
        {
            task.NextState();
            Debug.Log("yay!");
        }
        else
        {
            Debug.Log("nay!");
        }
    }
}
