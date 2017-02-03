using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _lost;
    public bool Lost { get { return _lost; } }

    public float energyLeft = 30;


    public void Archive()
    {
        if (_lost) return;
        energyLeft += 5;
    }

    public void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        energyLeft = Mathf.Max(0, energyLeft - (Time.deltaTime * 3));
        if (energyLeft == 0)
            Lose();
    }

    public void Lose()
    {
        _lost = true;
        Debug.Log("You lose!");
        enabled = false;
    }

    #region "Singleton"

    private static GameManager instance;

    public static GameManager INSTANCE
    {
        get
        {
            return instance;
        }
    }

    #endregion

}
