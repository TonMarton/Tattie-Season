using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int dropsToCollectForWin = 2;
    public int dropsCollected { get; private set; } = 0;

    public void CollectDrop()
    {
        dropsCollected += 1;
        if (dropsCollected == dropsToCollectForWin)
        {
            // TODO: open win gate
        }
        Debug.Log("DropsCollected: " + dropsCollected);
    }
}
