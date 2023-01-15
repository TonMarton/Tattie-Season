using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public HUD hud { get; private set; }

    private void Awake()
    {
        hud = gameObject.GetComponentInChildren<HUD>();    
    }

}
