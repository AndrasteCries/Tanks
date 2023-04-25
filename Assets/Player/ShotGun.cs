using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Buff
{
    public Sprite shotgunSprite; // Новое свойство для хранения спрайта

    void Start()
    {
        if (shotgunSprite != null)
        {
            spriteRenderer.sprite = shotgunSprite; // Установка спрайта в SpriteRenderer
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
