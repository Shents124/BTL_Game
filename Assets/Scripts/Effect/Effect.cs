using UnityEngine;

public class Effect : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
