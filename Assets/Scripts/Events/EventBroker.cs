using System;

public class EventBroker 
{
    public static event Action OnPushing;
    public static event Action OnStopPushing;
    public static event Action OnBossDead;
    public static event Action<SavePoint> OnSavePlayerPos;

    public static void CallOnPushing()
    {
        OnPushing?.Invoke();
    }
    public static void CallOnStopPushing()
    {
        OnStopPushing?.Invoke();
    }

    public static void CallOnBossDead()
    {
        OnBossDead?.Invoke();
    }

    public static void CallOnSavePlayerPos(SavePoint savePoint) => OnSavePlayerPos?.Invoke(savePoint);
}
