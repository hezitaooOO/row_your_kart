using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface
{
    public void Init();

    public int GetID();
    public Sprite GetSprite();

    public void OnUse();
}
