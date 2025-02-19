using UnityEngine;

public class KillBat : MonoBehaviour {
    [SerializeField] GameObject DeathEffect;
    [SerializeField] GameObject GroundEffect;

    public int totalScore, soundIndex;

    // "A player's score can not be infinite! Have a reasonable limit ready!"
    private const int MAX_SCORE = 1000;

    SoundManager soundManager;

    // Start is called before the first frame update
    void Start() {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseDown() {
        totalScore = PlayerPrefs.GetInt("Score", 0);
        /* totalScore++; */

        // "Arcade games usually added 100 points to make the score more appealing."
        totalScore += 100;

        // "'SoundManager' script; line #33."
        soundManager.PlayAudio();
        
        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();

        if (totalScore >= MAX_SCORE) {
            totalScore = MAX_SCORE;
            Debug.Log("Score: MAX");
        } else {
            Debug.Log("Score: " + totalScore);
        }
        
        Destroy(gameObject);
        // "Click the bat(s) to see the effects."
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            Destroy(gameObject);
            // "Missing each bat will be displayed."
            Instantiate(GroundEffect, transform.position, Quaternion.identity);
            soundManager.AltPlayAudio();
        }
    }
}