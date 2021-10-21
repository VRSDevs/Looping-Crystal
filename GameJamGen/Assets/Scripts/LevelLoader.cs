using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    float transitionTimeForEnd;
    float transitionTimeForStart;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "logoScene") {
            transitionTimeForEnd = 5.0f;
            Invoke("LoadGame", 5.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        transition.SetBool("Start", true);
        transitionTimeForStart = 1.0f;

        Invoke("LoadLevel", transitionTimeForStart);
    }

    void LoadLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(index);
    }
}
