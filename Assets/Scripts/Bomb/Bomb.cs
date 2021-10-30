using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int dame;
    [SerializeField] private Transform checkDamePos;
    [SerializeField] private float dameRange;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private float timeCountDown;
    [SerializeField] private GameObject explostionEffect;
    [SerializeField] private Text txtCountDown;
    private Rigidbody2D rid;
    private float currentTime = 0;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        StartCoroutine(Explosion());
        currentTime = timeCountDown;
        txtCountDown.text = currentTime.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        UpdateTimeCountDown();
    }

    private void UpdateTimeCountDown()
    {
       // txtCountDown.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        currentTime -= 1 * Time.deltaTime;
        txtCountDown.text = currentTime.ToString("0");
    }

    private void MoveToTarget()
    {
        float agngle = Mathf.Atan2(rid.velocity.y, rid.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(agngle, Vector3.forward);
    }

    private void InflictDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(checkDamePos.position, dameRange, damageableLayerMask);
        if (hits != null)
        {
            foreach (var collider in hits)
                collider.GetComponent<IDamageable>().TakeDame(dame);
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timeCountDown);
        InflictDamage();
        Destroy(this.gameObject, 0.1f);
        Instantiate(explostionEffect, checkDamePos.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkDamePos.position, dameRange);
    }

}
