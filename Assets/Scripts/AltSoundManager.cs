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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    // Update is called once per frame
    void Update() {
        /* "I could not simplify this part at the time."
        if (SceneManager.GetActiveScene().name == "Practice") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } else if (SceneManager.GetActiveScene().name == "Easy") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } else if (SceneManager.GetActiveScene().name == "Medium") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } else if (SceneManager.GetActiveScene().name == "Hard") {
            AltSoundManager.Instance.GetComponent<AudioSource>().Pause();
        } */
    }

    // "This is [a] more simplified version."
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        string[] pauseScenes = {"Practice", "Easy", "Medium", "Hard", "GameOver"};
        
        if (System.Array.Exists(pauseScenes, s => s == scene.name)) {
            Music.Pause();
        } else {
            PlayAudio();
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