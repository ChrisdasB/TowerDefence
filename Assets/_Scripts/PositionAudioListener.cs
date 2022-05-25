using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAudioListener : MonoBehaviour
{

    AudioListener audioListener;
    // Start is called before the first frame update
    void Start()
    {
        audioListener = GetComponent<AudioListener>();
        audioListener.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
