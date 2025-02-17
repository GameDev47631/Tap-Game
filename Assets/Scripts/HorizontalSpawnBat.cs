using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HorizontalSpawnBat : MonoBehaviour {
    [SerializeField] GameObject[] BatPrefabA, BatPrefabB, gameObjects;
    [SerializeField] protected TextMeshProUGUI scoreText;
    public int totalScore, isPaused;
    private Coroutine hSpawner1, hSpawner2;

    // Start is called before the first frame update
    void Start() {
        hSpawner1 = StartCoroutine(LeftFacingBat());
        hSpawner2 = StartCoroutine(RightFacingBat());

        isPaused = 0;
    }

    // Update is called once per frame
    void Update() {
        // "Score still increments in regards to Unity, not the 'SpawnBat' script."
        totalScore = PlayerPrefs.GetInt("Score", 0);

        UpdateScoreText();
        
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
            yield return new WaitForSeconds(Random.Range(5f, 8f));

            var horizontal = Random.Range(-30, 30);
            var vertical = Random.Range(-20, 20);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject newBat = Instantiate(BatPrefabA[Random.Range(0, BatPrefabA.Length)], spawnPosition, Quaternion.identity);

            // "Apply movement to the bat using Rigidbody2D."
            Rigidbody2D rigidbody = newBat.GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.velocity = Vector2.left * 20f;
            }

            Collider2D newBatCollider = newBat.GetComponent<Collider2D>();
            if (newBatCollider != null) {
                GameObject[] existingBats = GameObject.FindGameObjectsWithTag("Bat");
                foreach (GameObject existingBat in existingBats) {
                    Collider2D existingCollider = existingBat.GetComponent<Collider2D>();
                    if (existingCollider != null) {
                        Physics2D.IgnoreCollision(newBatCollider, existingCollider);
                    }
                }
            }

            Destroy(newBat, 4f);
        }
    }

    IEnumerator RightFacingBat() {
        while (isPaused == 0) {
            yield return new WaitForSeconds(Random.Range(5f, 8f));

            var horizontal = Random.Range(-30, 30);
            var vertical = Random.Range(-20, 20);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject newBat = Instantiate(BatPrefabB[Random.Range(0, BatPrefabB.Length)], spawnPosition, Quaternion.identity);

            // "This is meant for the horizontal prefabs only."
            Rigidbody2D rigidbody = newBat.GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                rigidbody.velocity = Vector2.right * 20f;
            }

            Collider2D newBatCollider = newBat.GetComponent<Collider2D>();
            if (newBatCollider != null) {
                GameObject[] existingBats = GameObject.FindGameObjectsWithTag("Bat");
                foreach (GameObject existingBat in existingBats) {
                    Collider2D existingCollider = existingBat.GetComponent<Collider2D>();
                    if (existingCollider != null) {
                        Physics2D.IgnoreCollision(newBatCollider, existingCollider);
                    }
                }
            }

            Destroy(newBat, 4f);
        }
    }

    public void PauseGame() {
        isPaused = 1;

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