using System;
using UnityEngine;

public static class GameEventManager 
{
    public static event Action OnCoinCollect;

    public static event Action OnPlayerReachEnd;
    public static void OnCoinCollected()
    {
        Debug.Log("Coin event invoked");
        OnCoinCollect?.Invoke();
    }
    public static void OnPlayerReachedEnd()
    {
        Debug.Log("Coin event invoked");
        OnPlayerReachEnd?.Invoke();
    }
}
