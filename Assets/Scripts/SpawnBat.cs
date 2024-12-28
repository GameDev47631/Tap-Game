using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnBat : MonoBehaviour {
    
    // "Create empty GameObjects to turn them into Prefabs."
    [SerializeField] GameObject[] BatPrefab, gameObjects;
    [SerializeField] TextMeshProUGUI scoreText;
    public int totalScore, isPaused;
    
    // "A pause button is required to see these."
    [SerializeField] GameObject darkScreen, frame;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawningBat());
        isPaused = 0;
    }

    // Update is called once per frame
    void Update() {
        // "Score increments by 1 after every click; 'totalscore++'."
        totalScore = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Caught: " + totalScore.ToString();

        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();
        
        if (isPaused == 1) {
            gameObjects = GameObject.FindGameObjectsWithTag("Bat");

            foreach (GameObject Bat in gameObjects) {
                Destroy(Bat);                
            }
        }
    }

    IEnumerator SpawningBat() {
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
            GameObject gameObject = Instantiate(BatPrefab[Random.Range(0, BatPrefab.Length)], spawnPosition, Quaternion.identity);
            Destroy(gameObject, 5f);
        }
    }

    public void PauseGame() {
        isPaused = 1;
        darkScreen.SetActive(true);
        frame.SetActive(true);

        // "This will pause the in-game timer."
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        isPaused = 0;
        darkScreen.SetActive(false);
        frame.SetActive(false);
        
        // "This will unpause it."
        Time.timeScale = 1;

        StartCoroutine(SpawningBat());
    }

    public void MenuScene() {
        // "'Play' script for more."
        SceneManager.LoadScene("MainMenu");
    }
}