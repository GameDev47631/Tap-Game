using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    [SerializeField] AudioSource Music, Effects;
    
    // "Have an array ready for more "randomized" sound effects."
    public AudioClip[] DeathSound, GroundSound;
    int isSound;
    public Sprite soundOnImage, soundOffImage;
    public Button SoundButton;

    // Start is called before the first frame update
    void Start() {
        isSound = PlayerPrefs.GetInt("Sound", 1);

        // "Sound on by default; check the 'ToggleSound()' method down below."
        if (isSound == 1) {
            SoundButton.GetComponent<Image>().sprite = soundOnImage;
        } else {
            SoundButton.GetComponent<Image>().sprite = soundOffImage;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void PlayAudio() {
        isSound = PlayerPrefs.GetInt("Sound", 1);

        if (isSound == 1 && !Music.isPlaying) {
            Music.Play();
        }

        // "Different sound effects play even for the 'same' GameObject/Prefab."
        Effects.clip = DeathSound[Random.Range(0, DeathSound.Length)];
        // "Sound effects will clip over each other."
        Effects.PlayOneShot(Effects.clip, 1F);
    }

    public void AltPlayAudio() {
        Effects.clip = GroundSound[Random.Range(0, GroundSound.Length)];
        Effects.PlayOneShot(Effects.clip, 1F);
        Effects.maxDistance = 4;
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

    public void ToggleSound() {
        // "Icon changes after every click."
        if (isSound == 1) {
            isSound = 0;
            PlayerPrefs.SetInt("Sound", isSound);
            PlayerPrefs.Save();
            SoundButton.GetComponent<Image>().sprite = soundOffImage;
            MuteSound();
        } else {
            isSound = 1;
            PlayerPrefs.SetInt("Sound", isSound);
            PlayerPrefs.Save();
            SoundButton.GetComponent<Image>().sprite = soundOnImage;
            UnmuteSound();
        }
    }
}