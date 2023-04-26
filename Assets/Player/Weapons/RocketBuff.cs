using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBuff : Buff
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
