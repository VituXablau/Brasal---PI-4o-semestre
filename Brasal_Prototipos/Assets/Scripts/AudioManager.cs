using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    public AudioClip[] music;

    public static AudioMixer audioMixer;
    AudioClip newClip;

    bool switched = false;

    public static float Volume = 1f;

    public static AudioManager instance;

    [SerializeField] string currentScene;

    [SerializeField] bool options = false;

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
        // if (instance != null)
        // {
        //     Destroy(instance.gameObject);
        //     instance = null;
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }


        musicSource.loop = true;

        switch (scene.name)
        {
            case "Menu":
                newClip = music[0];
                break;
            case "Hub":
                newClip = music[0];
                break;
            case "Options":
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

        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Options")
        {
            options = true;
        }
        else if (currentScene != "Options")
        {
            options = false;
        }

    }

    // Update is called once per frame
    void Update()
    {   
        musicSource.volume = Volume;
        
        if (options)
        {
            if (OptionsManager.currentMusic != musicSource.clip)
            {
                musicSource.clip = OptionsManager.currentMusic;
                if (musicSource.clip == music[3] || musicSource.clip == music[4])
                {
                    musicSource.loop = false;
                }
                else
                {
                    musicSource.loop = true;
                }
                musicSource.Play();
                Debug.Log("'-'");
            }
        }

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

    public void Sound1()
    {
        musicSource.clip = music[0];
        musicSource.Play();
        musicSource.loop = true;
    }


    public void Sound2()
    {
        musicSource.clip = music[1];
        musicSource.Play();
        musicSource.loop = true;
    }

    public void Sound3()
    {
        musicSource.clip = music[2];
        musicSource.Play();
        musicSource.loop = true;
    }

    public void Sound4()
    {
        musicSource.clip = music[3];
        musicSource.Play();
        musicSource.loop = false;
    }

    public void Sound5()
    {
        musicSource.clip = music[4];
        musicSource.Play();
        musicSource.loop = false;
    }

    public void Sound6()
    {
        musicSource.clip = music[5];
        musicSource.Play();
        musicSource.loop = true;
    }


}
