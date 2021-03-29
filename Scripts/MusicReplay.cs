using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReplay : MonoBehaviour
{
    private GameObject[] music;

    // Start is called before the first frame update]
    void Start()
    {
    music = GameObject.FindGameObjectsWithTag("music");
    if (music.Length > 1){
        Destroy (music[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake(){
    DontDestroyOnLoad(GameObject.FindWithTag("music"));
    }
}
