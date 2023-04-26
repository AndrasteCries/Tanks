using UnityEngine;

public class ShotGunBuff : Buff
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