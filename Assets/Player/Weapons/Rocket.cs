using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Buff
{
    public Sprite sprite;

    void Start()
    {
        if (sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
