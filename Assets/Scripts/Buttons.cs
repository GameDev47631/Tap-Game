using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        // "GameObjects/Prefabs were edited for animation."
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnMouseDown() {
        // "Classic Easy/Medium/Hard scenario."
        switch (gameObject.name) {
            // "Check the Image name (and text)."
            case "EasyMode":
                // "Find the Unity scene with this EXACT name."
                SceneManager.LoadScene("Easy");
                break;

            case "MediumMode":
                SceneManager.LoadScene("Medium");
                break;

            case "HardMode":
                SceneManager.LoadScene("Hard");
                break;

            // "The in-game buttons will give away which scene you go to."
            // "'MainMenu' can also be found within the 'SpawnBat' script."
            case "HomeButton":
            case "ExitButton":
                SceneManager.LoadScene("MainMenu");
                break;
                
            case "PlayButton":
                SceneManager.LoadScene("SelectionScreen");
                break;
            
            case "PracticeButton":
                SceneManager.LoadScene("Practice");
                break;
            
            case "CreditsButton":
                SceneManager.LoadScene("Credits");
                break;

            default:
                Debug.LogWarning("Unrecognized image clicked!");
                break;
        }
    }

    public void StartAnim() {
        // "GameObjects/Prefabs are always animated."
        anim.SetTrigger("Active");
    }

    public void PlayGameScene() {
        SceneManager.LoadScene("SelectionScreen");
    }

    public void PracticeScene() {
        SceneManager.LoadScene("Practice");
    }
}