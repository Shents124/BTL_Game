using UnityEngine;

public class ExplosionParticleSystem : MonoBehaviour
{
    private float duration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, duration);
    }

}
