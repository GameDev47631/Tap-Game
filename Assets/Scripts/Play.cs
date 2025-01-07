using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {
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
        // Play animation when the object is clicked
        startAnim();

        // Decide what to do based on the name or tag of the clicked object
        switch (gameObject.name) {
            case "PlayGameImage":
                PlayGameScene();
                break;

            case "EasyMode":
                SceneManager.LoadScene("Easy");
                break;

            case "MediumMode":
                SceneManager.LoadScene("Medium");
                break;

            case "HardMode":
                SceneManager.LoadScene("Hard");
                break;

            default:
                Debug.LogWarning("Unrecognized image clicked!");
                break;
        }
    }

    public void startAnim() {
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