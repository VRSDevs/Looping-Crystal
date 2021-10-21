using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadGameResources : MonoBehaviour
{
    public AudioClip trackLevel;
    // Start is called before the first frame update
    void Start()
    {
        // Reproducir música de fondo del juego
        FindObjectOfType<AudioManager>().ChangeMusic(trackLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
