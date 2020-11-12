using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoadout : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _legs = default;
    [SerializeField] private SkinnedMeshRenderer _torso = default;
    
    public void SetLegsTexture(Texture2D newTexture)
    {
        _legs.material.SetTexture("_MainTex", newTexture);
    }

    public void SetTorsoTexture(Texture2D newTexture)
    {
        _torso.material.SetTexture("_MainTex", newTexture);
    }
}
