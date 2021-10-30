using System.Collections;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox paramaters")]
    [SerializeField] private Transform detectorOrigin;
    [SerializeField] private Vector2 detectorSize = Vector2.zero;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] private float detectorDelay = 0.1f;
    [SerializeField] private LayerMask detectorLayerMask;

    [Header("Gizmo variables")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectorColor = Color.red;
    public bool showGizmo = true;

    private GameObject target;
    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private void Update()
    {
        PerformDetection();
    }

    private void PerformDetection()
    {
        Collider2D collider =
            Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);

        if (collider != null)
            Target = collider.gameObject;
        else
            Target = null;

    }

    private void OnDrawGizmos()
    {
        if (showGizmo && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectorColor;

            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }
}
