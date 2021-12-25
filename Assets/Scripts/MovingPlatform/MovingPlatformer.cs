using UnityEngine;

public class MovingPlatformer : MonoBehaviour
{
    public float speed;
    public GameObject[] waypoints;

    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentIndex].transform.position, transform.position) < 0.1f)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
                currentIndex = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        other.gameObject.transform.SetParent(null);
    }
}
