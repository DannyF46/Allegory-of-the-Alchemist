using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public string videoFileName;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer = GetComponent<VideoPlayer>();
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        Debug.Log(path);
        VideoPlayer.url = path;
        VideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
