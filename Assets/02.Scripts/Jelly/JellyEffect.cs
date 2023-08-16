using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JellyEffect : MonoBehaviour
{
    public JellyData data;
    public GameObject effectObject;
    public SpriteRenderer jellyEffectFront;
    public SpriteRenderer jellyEffectBack;
    private SpriteMask _platformMask;
    private JellyShooter _jellyShooter;
    private IEnumerator _jellyEffectCoroutine;

    //코드 내 지정 변수들
    private float _maxEffectScale = 3.5f;


    private void Awake()
    {
        effectObject.transform.localScale = Vector3.zero;
        _jellyShooter = GetComponent<JellyShooter>();
    }

    private void OnEnable()
    {
        FindObjectOfType<ColorManager>().mainColoringChanged += UpdateJellyEffectVisibleness;
    }

    private void OnDisable()
    {
        ColorManager.instance.mainColoringChanged -= UpdateJellyEffectVisibleness;
    }



    public void JellyEffectOn(Transform targetPlatform, Vector2 startingPoint)
    {
        //코루틴 시작. (안겹치게)
        UpdateJellyEffectVisibleness();
        if (_jellyEffectCoroutine != null) StopCoroutine(_jellyEffectCoroutine);
        _jellyEffectCoroutine = StartJellifyCoroutine(targetPlatform, startingPoint);
        StartCoroutine(_jellyEffectCoroutine);
    }

    public void JellyEffectOff(Vector2 endingPoint)
    {
        UpdateJellyEffectVisibleness();
        if (_jellyEffectCoroutine != null) StopCoroutine(_jellyEffectCoroutine);
        _jellyEffectCoroutine = StartUnjellifyCoroutine(endingPoint);
        StartCoroutine(_jellyEffectCoroutine);
    }

    IEnumerator StartJellifyCoroutine(Transform targetPlatform, Vector2 startingPoint)
    {
        float _timer = data.jellySpreadTime;
        effectObject.transform.position = startingPoint;

        _platformMask = targetPlatform.GetComponentInChildren<SpriteMask>();
        _platformMask.enabled = true;
        effectObject.transform.parent = _platformMask.transform;

        while(_timer > 0)
        {
            effectObject.transform.localScale = Vector3.one * Mathf.Lerp(_maxEffectScale, 0f, _timer / data.jellySpreadTime);
            _timer -= Time.deltaTime;
            yield return null;
        }
        effectObject.transform.localScale = Vector3.one * _maxEffectScale;
        _jellyShooter.JellifyComplete(targetPlatform.GetComponent<ColoredObject>());
    }

    IEnumerator StartUnjellifyCoroutine(Vector2 endingPoint)
    {
        float _timer = data.jellySpreadTime;
        effectObject.transform.position = endingPoint;

        while (_timer > 0)
        {
            effectObject.transform.localScale = Vector3.one * Mathf.Lerp(0f, _maxEffectScale, _timer / data.jellySpreadTime);
            _timer -= Time.deltaTime;
            yield return null;
        }
        _platformMask.enabled = false;
        _jellyShooter.UnjellifyComplete();

    }

    void UpdateJellyEffectVisibleness()
    {
        if (_jellyShooter.jellyColoring == ColorManager.instance.mainColoring)
        {
            //둘다 플레이어보단 낮고, 플랫폼보단 높기만 하면 됨.
            jellyEffectFront.sortingOrder = 10;
            jellyEffectBack.sortingOrder = 10;
        }
        else
        {
            //front는 플레이어, 플랫폼 모두보다 높게, back은 둘보다 낮게.
            jellyEffectFront.sortingOrder = 100;
            jellyEffectBack.sortingOrder = -100;
        }
    }
}
