using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : MonoBehaviour {
    /* https://www.youtube.com/watch?v=2XQsKNHk1vk */
    public Button[] difficulty;

    private void Awake() {
        int prerequisite = PlayerPrefs.GetInt("Unlocked", 1);

        for (int i = 0; i < difficulty.Length; i++) {
            difficulty[i].interactable = false;
        }

        for (int i = 0; i < prerequisite; i++) {
            difficulty[i].interactable = true;
        }
    }

    public void UnlockStage(string mode) {
        string stageName = mode;
        SceneManager.LoadScene(stageName);
    }
}