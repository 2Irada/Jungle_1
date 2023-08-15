using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JellyEffect : MonoBehaviour
{
    private GameObject jellyEffect, platformMask;
    [SerializeField] private float effectSpeed = 0.5f;


    private void Start()
    {
        jellyEffect = GameObject.Find("JellyEffect");
        jellyEffect.transform.localScale = Vector3.zero;
    }


    public void JellyEffectOn(Transform targetPlatform, Transform jelly)
    {
        jellyEffect.transform.position = jelly.position;

        platformMask = targetPlatform.Find("Platform_Mask").gameObject;
        platformMask.SetActive(true);
        jellyEffect.transform.parent = platformMask.transform;
        jellyEffect.transform.DOScale(Vector3.one * 2.9f, effectSpeed);
    }

    public void JellyEffectOff(Transform targetPlayer)
    {
        jellyEffect.transform.position = targetPlayer.position;
        
        Tween tween = jellyEffect.transform.DOScale(Vector3.zero, effectSpeed);

        tween.OnComplete(() =>
        {
            platformMask.SetActive(false);
        });
    }
}
