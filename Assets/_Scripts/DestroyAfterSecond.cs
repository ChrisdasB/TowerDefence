using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSecond : MonoBehaviour
{
    [SerializeField] float timeToLiveInSeconds = 1.5f;
       
    void Start()
    {
        StartCoroutine("DestroyAfterSeconds");
    }

    // Destroy the gameObject after X seconds
    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(timeToLiveInSeconds);
        Destroy(gameObject);
    }

}
