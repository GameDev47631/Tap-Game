using UnityEngine;
using UnityEngine.SceneManagement;

public class AltSoundManager : MonoBehaviour {
    // "Some scenes will share and continue the same music track."
    [SerializeField] AudioSource Music, Effects;
    public static AltSoundManager Instance;
    
    int isSound;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        // "I couldn't find a way to simplify this part."
        if (SceneManager.GetActiveScene().name == "Practice") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } else if (SceneManager.GetActiveScene().name == "Easy") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } else if (SceneManager.GetActiveScene().name == "Medium") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } else if (SceneManager.GetActiveScene().name == "Hard") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        }
    }

    public void PlayAudio() {
        isSound = PlayerPrefs.GetInt("Sound", 1);

        if (isSound == 1 && !Music.isPlaying) {
            Music.Play();
        }
    }

    public void MuteSound() {
        Music.volume = 0;
        Effects.volume = 0;

        Music.Pause();
    }

    public void UnmuteSound() {
        Music.volume = 1;
        Effects.volume = 1;

        Music.UnPause();
    }
}