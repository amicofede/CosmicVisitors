using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoSingleton<EventController>
{
    public static event Action OnVisitorKilled;
    public static event Action<bool> OnVisitorHitBounds;
    public static event Action<int> OnScoreChanged;

    public static void VisitorKilled()
    {
        OnVisitorKilled?.Invoke();
    }

    public static void ScoreChanged(int _score)
    {
        OnScoreChanged?.Invoke(_score);
    }

    public static void VisitorHitBounds(bool _hitBounds)
    {
        OnVisitorHitBounds?.Invoke(_hitBounds);
    }
}
