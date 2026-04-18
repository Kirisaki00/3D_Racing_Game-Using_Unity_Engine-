using UnityEngine;

public class AICarController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1200f;
    public float turnSpeed = 5f;
    public float waypointReachDist = 12f;

    private int currentWaypoint = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];

        Vector3 toTarget = target.position - transform.position;
        Vector3 direction = toTarget.normalized;

        // --- TURN FIRST (important) ---
        Quaternion targetRot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime));

        // --- BRAKE when angle is big (helps on turns) ---
        float angle = Vector3.Angle(transform.forward, direction);
        float throttle = Mathf.Clamp01(1f - angle / 90f); // 0 when facing wrong way, 1 when aligned

        // --- MOVE ---
        rb.AddForce(transform.forward * speed * throttle, ForceMode.Acceleration);

        // --- SWITCH WAYPOINT ---
        if (toTarget.magnitude < waypointReachDist)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
}