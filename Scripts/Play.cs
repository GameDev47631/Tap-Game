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

    public void startAnim() {
        // "GameObjects/Prefabs are always animated."
        anim.SetTrigger("Active");
    }

    public void PlayGameScene() {
        // "Check any scripts with 'Bat' or 'Sound' in the name."
        SceneManager.LoadScene("TapGame");
    }
}