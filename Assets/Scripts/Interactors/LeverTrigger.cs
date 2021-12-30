using UnityEngine;
using UnityEngine.Events;

public class LeverTrigger : MonoBehaviour, IDamageable
{
    public UnityEvent OnLeft;
    public UnityEvent OnRight;

    public GameObject leverLeft;
    public GameObject leverRight;

    public bool isRight;
    public GameObject hitEffect;

    private void Start()
    {
        if (isRight)
        {
            leverLeft.SetActive(false);
            leverRight.SetActive(true);
        }
    }
    public void TakeDame(int amountOfDame, Vector3 damePos)
    {
        Instantiate(hitEffect, damePos, Quaternion.identity);
        if (isRight)
            TriggerLeft();
        else
            TriggerRight();
    }

    private void TriggerLeft()
    {
        isRight = false;
        leverLeft.SetActive(true);
        leverRight.SetActive(false);
        OnLeft?.Invoke();
    }
    private void TriggerRight()
    {
        isRight = true;
        leverLeft.SetActive(false);
        leverRight.SetActive(true);
        OnRight?.Invoke();
    }
}
