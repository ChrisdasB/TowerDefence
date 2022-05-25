using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    [SerializeField] int coinValue = 10;

    // On click, increade value of money & Destroy this game Object
    private void OnMouseDown()
    {
        GameManager.Instance.setNewScore(coinValue, true);
        if (GameManager.Instance.TryGetComponent<AudioManager>(out AudioManager audioManager))
            audioManager.PlaySound();
        Destroy(gameObject);
    }
}
