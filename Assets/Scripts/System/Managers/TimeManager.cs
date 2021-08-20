﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : ManagerService
{
    public float TimeScale { get => Time.timeScale; set => Time.timeScale = value; }
    public bool IsSlow { get; private set; } = true;
    private const float TimeScaleFactor = .1f;
    private const float FixedDeltaTimeModificator = .02f;

    public void DoSlowMotion(){
        TimeScale = TimeScaleFactor;
        Time.fixedDeltaTime = TimeScale * FixedDeltaTimeModificator;
        Debug.Log("SLOWMOTION");
        IsSlow = true;
    }

    public void ResetTimeScale(){
        TimeScale = 1;
        Debug.Log("RESETTIME");
        IsSlow = false;
    }
}
