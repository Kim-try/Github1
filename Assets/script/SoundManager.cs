using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        var objs = FindObjectsOfType<SoundManager>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public void SceneCheck()
    {
        if (SceneManager.GetActiveScene().name == "TownScene")
        {
            audioSource.clip = audioClip[0];
        }
        else if (SceneManager.GetActiveScene().name == "FieldScene")
        {
            audioSource.clip = audioClip[1];
        }
        else if (SceneManager.GetActiveScene().name == "MazeScene")
        {
            audioSource.clip = audioClip[2];
        }
        audioSource.Play();
    }

}
