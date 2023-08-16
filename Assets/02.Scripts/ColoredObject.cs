using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredObject : MonoBehaviour
{
    public Coloring objectColoring = new Coloring();
    public Coloring currentColoring = new Coloring();
    public GameObject eyeballObject;
    public bool startAsEyeball;
    [HideInInspector] public bool isEyeball;

    private bool _isJellied = false;
    private LineRenderer _lr;
    private List<SpriteRenderer> _eyeballRenderers = new List<SpriteRenderer>();

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        GetComponent<Collider2D>().enabled = true;
        InitializeColoring();
        InitEyeball();
    }

    private void OnEnable()
    {
        FindObjectOfType<ColorManager>().mainColoringChanged += UpdateColoringLogic;
    }

    private void OnDisable()
    {
        ColorManager.instance.mainColoringChanged -= UpdateColoringLogic;
    }

    /// <summary>
    /// 오브젝트의 원본 색을 설정.
    /// </summary>
    private void InitializeColoring()
    {

        GetComponent<SpriteRenderer>().color = ColorManager.instance.GetColorByColoring(objectColoring);
        UpdateColoringLogic();
    }

    /// <summary>
    /// 이 오브젝트를 젤리가 뒤덮음.
    /// </summary>
    /// <param name="jellyColoring"></param>
    public void GetJellied(Coloring jellyColoring)
    {
        _isJellied = true;
        UpdateColoringLogic();
    }

    public void GetUnjellied()
    {
        //필요: 젤리 없어지는 시각 효과
        _isJellied = false;
        UpdateColoringLogic();
    }

    /// <summary>
    /// 메인 컬러링과 이 오브젝트의 컬러링을 비교한 뒤 콜라이더 등 설정.
    /// </summary>
    void UpdateColoringLogic()
    {
        currentColoring = _isJellied? FindObjectOfType<JellyShooter>().jellyColoring : objectColoring;

        if (ColorManager.instance.mainColoring != currentColoring) 
        {
            GetComponent<Collider2D>().isTrigger = false;
            if (_isJellied)
            {
                SetActiveLineRenderer(true);
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                SetActiveLineRenderer(false);
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            GetComponent<Collider2D>().isTrigger = true;
            SetActiveLineRenderer(true);
            GetComponent<SpriteRenderer>().enabled = false;
        }

        if (isEyeball)
        {
            if (ColorManager.instance.mainColoring == currentColoring || _isJellied) //젤리 붙었거나 결론적으로 투명하거나 둘중 하나면 반투명
            {
                SetEyeballAlpha(0.5f);
            }
            else { SetEyeballAlpha(1.0f);}
        }
    }

    private void SetActiveLineRenderer(bool value)
    {
        _lr.startColor = ColorManager.instance.GetHighlightColorByColoring(objectColoring);
        _lr.endColor = ColorManager.instance.GetHighlightColorByColoring(objectColoring);
        _lr.enabled = value;
    }

    private void InitEyeball()
    {
        eyeballObject.SetActive(true);
        _eyeballRenderers.Clear();
        _eyeballRenderers.AddRange(eyeballObject.GetComponentsInChildren<SpriteRenderer>());

        if (startAsEyeball)
        {
            eyeballObject.SetActive(true);
            isEyeball = true;
        }
        else 
        {
            eyeballObject.SetActive(false);
            isEyeball = false;
        }
    }

    public void EyeballEaten()
    {
        if (isEyeball == false) return;

        //눈깔 없애기
        eyeballObject.SetActive(false);
    }

    public void JellyLeavesEyeball()
    {
        if (isEyeball == false) return;

        isEyeball = false;
    }

    IEnumerator StartEyeballFade()
    {
        //눈깔 페이드 아웃. (페이드 인은 필요 없을듯)
        yield return null;
    }

    private void SetEyeballAlpha(float alpha)
    {
        for (int i = 0; i < _eyeballRenderers.Count; i++)
        {
            Color _color = _eyeballRenderers[i].color;
            _eyeballRenderers[i].color = new Color(_color.r, _color.g, _color.b, alpha);
        }
    }
}