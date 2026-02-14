using System;
using UnityEngine;

public static class GameEventManager 
{
    public static event Action OnCoinCollect;
    public static void OnCoinCollected()
    {
        OnCoinCollect?.Invoke();
    }
}
