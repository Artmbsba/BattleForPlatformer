using System;
using UnityEngine;

public class AnimationEvents: MonoBehaviour
{
    public event Action Attacing;

    public void InvokeAttackingEvent()
    {
        Attacing?.Invoke();
    }
}