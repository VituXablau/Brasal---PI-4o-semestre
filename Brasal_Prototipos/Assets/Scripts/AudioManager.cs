using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    [SerializeField] AudioClip[] music;

    public static AudioMixer audioMixer;
    AudioClip newClip;

    bool switched = false;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // music[0] = menu;
        // music[1] = cutscene;
        // music[2] = level;
        // music[3] = win;
        // music[4] = lose;
        // music[5] = finale

        musicSource.clip = music[0];
        musicSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // public void UpdateAudioVolume(float volume)
    // {
    //     audioMixer.SetFloat("AudioVolume", volume);
    // }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        musicSource.loop = true;

        switch (scene.name)
        {
            case "Menu":
                newClip = music[0];
                break;
            case "Hub":
                newClip = music[0];
                break;
            case "MataAtlanticaTutorial":
                switched = false;
                newClip = music[2];
                break;
            case "MataAtlantica":
                switched = false;
                newClip = music[2];
                break;
            case "Pantanal":
                switched = false;
                newClip = music[2];
                break;
            case "Amazonia":
                switched = false;
                newClip = music[2];
                break;
            case "Cerrado":
                switched = false;
                newClip = music[2];
                break;
            case "Caatinga":
                switched = false;
                newClip = music[2];
                break;
            case "NormalEnding":
                newClip = music[5];
                break;
            case "GoodEnding":
                newClip = music[5];
                break;
            default:
                newClip = music[1];
                break;

        }

        if (newClip != musicSource.clip)
        {
            musicSource.clip = newClip;
            musicSource.Play();
            Debug.Log("'-'");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        
       musicSource.volume = OptionsManager.Volume;
        
        if (!switched)
        {
            if (GameManager.endGame && !GameManager.gameOver)
            {
                musicSource.clip = music[3];
                switched = true;
                musicSource.loop = false;
                musicSource.Play();
            }
            else if (GameManager.endGame && GameManager.gameOver)
            {
                musicSource.clip = music[4];
                switched = true;
                musicSource.loop = false;
                musicSource.Play();
            }
        }
    }
}
