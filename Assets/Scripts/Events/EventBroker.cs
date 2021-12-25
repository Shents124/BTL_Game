using System;

public class EventBroker 
{
    public static event Action OnPushing;
    public static event Action OnStopPushing;
    public static event Action OnPlayerDead;

    public static void CallOnPushing()
    {
        OnPushing?.Invoke();
    }
     public static void CallOnStopPushing()
    {
        OnStopPushing?.Invoke();
    }

    public static void CallOnPlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
}
