using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LunaScript : MonoBehaviour
{
    public GameObject crystalPrefab;
    public GameObject robotPrefab;
    private int score = 0;
    private Rigidbody rb;
    public float moveSpeed = 5f;
    public int crystalPoints = 50;
    public int robotPoints = -100;
    public TMP_Text scoreText;
    private float horizontalInput;
    private float verticalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreText.text = "Score: " + score;
        InvokeRepeating("SpawnCrystals", 2.0f, 2.0f);
        InvokeRepeating("SpawnRobots", 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void SpawnCrystals()
    {
        Vector3 inRadius = Random.insideUnitCircle * 7f;
        Vector3 spawnPosition = new Vector3(transform.position.x + inRadius.x, 3.0f, transform.position.z + inRadius.y);
        GameObject crystal = Instantiate(crystalPrefab, spawnPosition, Quaternion.identity);
        Destroy(crystal, 4f);
    }

    void SpawnRobots()
    {
        Vector3 inRadius = Random.insideUnitCircle * 7f;
        Vector3 spawnPosition = new Vector3(transform.position.x + inRadius.x, 5.0f, transform.position.z + inRadius.y);
        GameObject robot = Instantiate(robotPrefab, spawnPosition, Quaternion.identity);
        Destroy(robot, 4f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            score += crystalPoints;
            scoreText.text = "Score: " + score;

            if (score < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            score += robotPoints;
            scoreText.text = "Score: " + score;

            if (score < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
        }
    }

}
