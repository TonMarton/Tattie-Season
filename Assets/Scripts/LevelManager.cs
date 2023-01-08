using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int dropsToCollectForWin = 2;
    public int dropsCollected { get; private set; } = 0;

    public UnityEvent OnCollectWaterDrop;
    public void CollectDrop()
    {
        OnCollectWaterDrop?.Invoke();
        dropsCollected += 1;
        if (dropsCollected == dropsToCollectForWin)
        {
            // TODO: open win gate
        }
        Debug.Log("DropsCollected: " + dropsCollected);
    }
}
