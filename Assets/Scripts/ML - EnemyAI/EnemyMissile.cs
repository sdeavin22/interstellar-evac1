using UnityEngine;

public class EnemyMissile : MonoBehaviour
{

    public float speed = 10f;
    public float rotationSpeed = 3f;

    public Transform player;
    private float timeCreated, maxTimeAlive = 5f;

    private Rigidbody rb;
    private float initialDistance;
    private bool shouldRotate = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            initialDistance = Vector3.Distance(transform.position, player.position);
        }
        else
        {
            Debug.LogError("Missile script could not find an object with tag 'Player'.");
        }

        timeCreated = Time.time;
    }

    void Update()
    {

        if (player == null) return; // Ensure there is a player to target

        // Calculate the direction to the player
        Vector3 targetDirection = player.position - transform.position;
        targetDirection.Normalize(); // Normalize the direction vector

        
        // Calculate the angle between the missile's forward direction and the direction to the player
        float angleToPlayer = Vector3.Angle(transform.forward, targetDirection);

        // Check if the missile has gone past the player
        if (angleToPlayer > 45) // 90 degrees is directly to the side
        {
            shouldRotate = false; // Stop rotating if the missile has gone past the player
        }

        // Rotate the missile to face the player smoothly if it should still rotate
        if (shouldRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        

        //Check if the missile has existed for too long and destroy it if so
        if (Time.time - timeCreated > maxTimeAlive)
        {
            Destroy(gameObject);
        }
    }
}
