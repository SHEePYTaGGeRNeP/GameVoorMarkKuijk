using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GUIManager : MonoBehaviour {

    public static GUIManager instance;

    [Header("Columns")]
    public GameObject doingColumn;
    public GameObject reviewColumn;
    public GameObject archiveColumn;
    public GameObject taskColumn;

    [Header("CardRef")]
    public GameObject cardRef;

    public float margin = 10f;
    public float cardHeight;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
	// Use this for initialization
	void Start ()
    {
        cardHeight = cardRef.GetComponent<RectTransform>().rect.height;

        GameObject testObj = Instantiate(cardRef);
        GameObject testObj2 = Instantiate(cardRef);
        GameObject testObj3 = Instantiate(cardRef);
        GameObject testObj4 = Instantiate(cardRef);
        GameObject testObj5 = Instantiate(cardRef);

        PlaceCard(testObj, TaskState.InProgress, 0);
        PlaceCard(testObj2, TaskState.InProgress, 1);
        PlaceCard(testObj3, TaskState.InProgress, 2);
        PlaceCard(testObj4, TaskState.Review, 0);
        PlaceCard(testObj5, TaskState.Review, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlaceCard(GameObject card, TaskState columnT, int tasksInColumn)
    {
        RectTransform cRect = card.GetComponent<RectTransform>();
        RectTransform colRect = null;

        if (columnT == TaskState.InProgress)
            colRect = doingColumn.GetComponent<RectTransform>();
        else if(columnT == TaskState.Todo)
            colRect = taskColumn.GetComponent<RectTransform>();
        else if (columnT == TaskState.Review)
            colRect = reviewColumn.GetComponent<RectTransform>();
        else if (columnT == TaskState.Archive)
            colRect = archiveColumn.GetComponent<RectTransform>();

        if(colRect == null)
        {
            Debug.Log("Something went wrong with getting the correct column rect.");
            return;
        }

        card.transform.SetParent(colRect.transform);
        cRect.localPosition = new Vector3(cRect.position.x, colRect.position.y - (cardHeight * (tasksInColumn + 1)), cRect.position.z);
        

    }
}
