using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    [SerializeField] float verticalOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position + new Vector3(0, verticalOffset, 0);
    }
}
