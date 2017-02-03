using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _lost;

    public float energyLeft = 30;


    public void Archive()
    {
        if (_lost) return;
        energyLeft += 5;
    }

    private void Update()
    {        
        energyLeft = Mathf.Max(0, energyLeft - (Time.deltaTime * 3));
        if (energyLeft == 0)
            Lose();
    }

    private void Lose()
    {
        _lost = true;
        Debug.Log("You lose!");
        enabled = false;
    }

}
