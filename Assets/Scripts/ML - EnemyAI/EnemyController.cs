using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Starting variables for basic ai aim and fire
    private Transform player;
    public GameObject missilePrefab;
    public float firingInterval;
    public float detectionRange;

    // Added variables for realistic aim
    private Vector3 lastPlayerPosition;
    private Vector3 playerVelocity;
    private Vector3 lastPosition;

    // Variables for staying in camera
    private Camera mainCamera;
    private Vector3 viewportPosition;
    private bool positionSet = false;
    private float nextFireTime = 1f;

    AudioManager audioManager;


    // State machine
    enum State { Idle, Attack }
    State currentState = State.Idle;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        mainCamera = Camera.main;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            SetInitialViewportPosition();
        }
        else
        {
            Debug.LogError("Enemy AI script could not find an object with tag 'Player'.");
        }

        lastPosition = transform.position; // Sets lastPosition to the starting position
    }

    void Update()
    {
        // Update player velocity
        if (player != null)
        {
            playerVelocity = (player.position - lastPlayerPosition) / Time.deltaTime;
            lastPlayerPosition = player.position;
        }

        LookAtPlayer();

        switch (currentState)
        {
            case State.Idle:
                // Checks if player is near to swap to attack state
                if (Vector3.Distance(transform.position, player.position) < detectionRange)
                {
                    currentState = State.Attack;
                }
                UpdatePositionRelativeToCamera();
                LookAtPlayer();

                break;
            case State.Attack:
                if (!positionSet)
                {
                    positionSet = true;
                }
                UpdatePositionRelativeToCamera();
                LookAtPlayer();

                if (Time.time >= nextFireTime)
                {
                    FireMissile();
                    nextFireTime = Time.time + firingInterval;
                }
                break;
        }

        lastPosition = transform.position;
    }

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    /*
    void FireMissile()
    {
        // Calculate the enemy's current velocity based on the last frame's update
        Vector3 enemyVelocity = (transform.position - lastPosition) / Time.deltaTime;

        // Determine the distance to the player
        Vector3 distanceToPlayer = player.position - transform.position;

        // Manually using 50 as missile speed; adjust as necessary
        float missileSpeed = 50;
        float reachTime = distanceToPlayer.magnitude / missileSpeed;

        // Calculate the predicted position, taking into account both the player's and enemy's velocity
        Vector3 predictedPosition = player.position + (playerVelocity - enemyVelocity) * reachTime;

        // Add inaccuracy to the missile firing
        float inaccuracy = 1.5f; // Adjust this value to increase or decrease inaccuracy
        Vector3 inaccuracyOffset = new Vector3(UnityEngine.Random.Range(-inaccuracy, inaccuracy), 0, UnityEngine.Random.Range(-inaccuracy, inaccuracy));
        predictedPosition += inaccuracyOffset;

        // Set the firing rotation towards the predicted position
        Quaternion firingRotation = Quaternion.LookRotation(predictedPosition - transform.position);

        // Instantiate the missile
        Instantiate(missilePrefab, transform.position, firingRotation);

        // Update lastPosition for the next frame's velocity calculation
        lastPosition = transform.position;
    }
    */
    void FireMissile()
    {
        audioManager.PlaySFXEnemy(audioManager.enemy_shoot);
        GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
        EnemyMissile missileScript = missile.GetComponent<EnemyMissile>();
        missileScript.player = player;
    }

    float RandomExcludingMiddle(float min1, float max1, float min2, float max2)
    {
        if (Random.value < 0.5f) // 50% chance to pick either range
        {
            return Random.Range(min1, max1);
        }
        else
        {
            return Random.Range(min2, max2);
        }
    }

    void SetInitialViewportPosition()
    {
        // Random x and y values within the viewport 0-1 range
        float randomX = RandomExcludingMiddle(0.2f, 0.4f, 0.6f, 0.8f);
        float randomY = RandomExcludingMiddle(0.2f, 0.4f, 0.6f, 0.8f);

        // Set a depth range; how far the object should be from the camera
        float depth = Random.Range(70f, 100f);

        // Set a random position within the viewport
        viewportPosition = new Vector3(randomX, randomY, depth);
    }

    void UpdatePositionRelativeToCamera()
    {
        // Convert the viewport position back to a world position
        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }
}
