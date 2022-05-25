using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
    [SerializeField] Sprite[] weaponSprites;
    public tankClass tankClass = tankClass.none;

    SpriteRenderer spriteRenderer;

    // Grap the sprite renderer, if you got it, fire SetSprite function
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            SetSprite(tankClass);
    }

    // Sets the right sprite for the tankClass
    void SetSprite(tankClass tankClass)
    {
        if (tankClass == tankClass.Tank1)
            spriteRenderer.sprite = weaponSprites[0];

        if (tankClass == tankClass.Tank2)
            spriteRenderer.sprite = weaponSprites[1];

        if (tankClass == tankClass.Tank3)
            spriteRenderer.sprite = weaponSprites[2];

        if (tankClass == tankClass.Mine)
            spriteRenderer.sprite = weaponSprites[3];

        if (tankClass == tankClass.none)
            print("Please set a TankClass on your button ...");
    }

    // Receive the new class from UIHandler
    public void SetTankClass(tankClass newTankClass)
    {
        tankClass = newTankClass;
    }
}
