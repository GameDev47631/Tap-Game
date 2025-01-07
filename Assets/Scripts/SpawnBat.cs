using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnBat : MonoBehaviour {
    // "Create empty GameObjects for Prefabs."
    [SerializeField] GameObject[] BatPrefab1, BatPrefab2A, BatPrefab2B, gameObjects;
    
    // "'Timer' inherits from 'SpawnBat', but needs to access 'scoreText'."
    [SerializeField] protected TextMeshProUGUI scoreText;
    public int totalScore, isPaused;
    
    // "A pause button is required to see these."
    public GameObject darkScreen, frame;

    private Coroutine vSpawner, hSpawner1, hSpawner2;

    public bool movement = true;

    // Start is called before the first frame update
    void Start() {
        vSpawner = StartCoroutine(VerticalSpawningBat());
        hSpawner1 = StartCoroutine(LeftFacingBat());
        hSpawner2 = StartCoroutine(RightFacingBat());

        isPaused = 0;
    }

    // Update is called once per frame
    void Update() {
        // "Score increments by 1 after every click; 'totalscore++'."
        totalScore = PlayerPrefs.GetInt("Score", 0);
        
        // "More inforation down below."
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

    IEnumerator VerticalSpawningBat() {
        while (isPaused == 0) {
            yield return new WaitForSeconds(1f);
            /* https://blog.sentry.io/unity-tutorial-developing-your-first-unity-game-part-2/
            "I could not work this code out."
            float posY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
                Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float posX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            var spawnPosition = new Vector2(posX, posY);
            Instantiate(Bat, spawnPosition, Quaternion.identity); */

            /* https://www.youtube.com/watch?v=j6p5Nh7JvmY
            "The bats will appear from within the screen." */
            var horizontal = Random.Range(-20, 20);
            var vertical = Random.Range(0, 10);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject gameObject = Instantiate(BatPrefab1[Random.Range(0, BatPrefab1.Length)], spawnPosition, Quaternion.identity);
            Destroy(gameObject, 4f);
        }
    }

    IEnumerator LeftFacingBat() {
        while (isPaused == 0) {
            yield return new WaitForSeconds(2.75f);

            var horizontal = Random.Range(-20, 20);
            var vertical = Random.Range(0, 10);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject gameObject = Instantiate(BatPrefab2A[Random.Range(0, BatPrefab2A.Length)], spawnPosition, Quaternion.identity);

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

            var horizontal = Random.Range(-20, 20);
            var vertical = Random.Range(0, 10);
            var spawnPosition = new Vector2(horizontal, vertical);
            GameObject gameObject = Instantiate(BatPrefab2B[Random.Range(0, BatPrefab2B.Length)], spawnPosition, Quaternion.identity);

            // Apply movement to the bat using Rigidbody2D.
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null) {
                Vector2 direction = Vector2.right;
                rigidbody.velocity = direction * 9f; // Adjust speed as needed.
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

        if (vSpawner != null || (hSpawner1 != null && hSpawner2 != null)) {
            StopCoroutine(vSpawner);
            vSpawner = null;

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

        if (vSpawner == null || hSpawner1 == null) {
            vSpawner = StartCoroutine(VerticalSpawningBat());
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