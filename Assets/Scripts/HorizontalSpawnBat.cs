using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorizontalSpawnBat : MonoBehaviour {
    [SerializeField] GameObject[] BatPrefabA, BatPrefabB, gameObjects;
    [SerializeField] protected TextMeshProUGUI scoreText;
    public int totalScore, isPaused;
    public GameObject darkScreen, frame;
    private Coroutine hSpawner1, hSpawner2;
    public Collider otherCollider;
    private float speed = 9f;

    // Start is called before the first frame update
    void Start() {
        hSpawner1 = StartCoroutine(LeftFacingBat());
        hSpawner2 = StartCoroutine(RightFacingBat());

        Collider myCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(myCollider, otherCollider, true);

        isPaused = 0;
    }

    // Update is called once per frame
    void Update() {
        // "Score increments by 1 after every click; 'totalscore++'."
        totalScore = PlayerPrefs.GetInt("Score", 0);

        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();
        
        if (isPaused == 1) {
            gameObjects = GameObject.FindGameObjectsWithTag("Bat");

            foreach (GameObject Bat in gameObjects) {
                Destroy(Bat);                
            }
        }
    }

    IEnumerator LeftFacingBat() {
        while (isPaused == 0) {
            yield return new WaitForSeconds(2f);

            var horizontal = Random.Range(-20, 20);
            var vertical = Random.Range(0, 10);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject gameObject = Instantiate(BatPrefabA[Random.Range(0, BatPrefabA.Length)], spawnPosition, Quaternion.identity);

            // Apply movement to the bat using Rigidbody2D.
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                Vector2 direction = Vector2.left;
                rigidbody.velocity = direction * 9f; // Adjust speed as needed.
            }

            Destroy(gameObject, 4f);
        }
    }

    IEnumerator RightFacingBat() {
        while (isPaused == 0) {
            yield return new WaitForSeconds(6.75f);

            var horizontal = Random.Range(-30, 30);
            var vertical = Random.Range(0, 20);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject gameObject = Instantiate(BatPrefabB[Random.Range(0, BatPrefabB.Length)], spawnPosition, Quaternion.identity);

            // Apply movement to the bat using Rigidbody2D.
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                Vector2 direction = Vector2.right;
                rigidbody.velocity = direction * speed;
            }

            Destroy(gameObject, 4f);
        }
    }

    public void PauseGame() {
        isPaused = 1;
        darkScreen.SetActive(true);
        frame.SetActive(true);

        // "This will pause the entire game."
        Time.timeScale = 0;

        if (hSpawner1 != null && hSpawner2 != null) {
            StopCoroutine(hSpawner1);
            StopCoroutine(hSpawner2);
            hSpawner1 = null;
            hSpawner2 = null;
        }
    }

    public void ResumeGame() {
        isPaused = 0;
        darkScreen.SetActive(false);
        frame.SetActive(false);
        
        // "This will unpause it."
        Time.timeScale = 1;

        if (hSpawner1 == null && hSpawner2 == null) {
            hSpawner1 = StartCoroutine(LeftFacingBat());
            hSpawner2 = StartCoroutine(RightFacingBat());
        }
    }

    private void UpdateScoreText() {
        if (SceneManager.GetActiveScene().name == "GameOver") {
            // "The score text will change in the 'GameOver' screen."
            scoreText.text = "Your Score: " + totalScore.ToString() + "\nPlay Again?";
        } else {
            // "This is the default score text for in-game."
            scoreText.text = "Caught: " + totalScore.ToString();
        }
    }

    public void MenuScene() {
        // "Check the 'Play' script for more."
        SceneManager.LoadScene("MainMenu");
    }
}