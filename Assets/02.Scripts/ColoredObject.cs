using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredObject : MonoBehaviour
{
    public Coloring objectColoring = new Coloring();
    public Material dashedLineMaterial;

    private bool _isJellied = false;
    private LineRenderer _lr;


    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        InitializeColoring();
        //InitLineRenderer();
        //ColorManager.instance.mainColoringChanged += UpdateColoringLogic;
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
    /// ������Ʈ�� ���� ���� ����.
    /// </summary>
    private void InitializeColoring()
    {

        GetComponent<SpriteRenderer>().color = ColorManager.instance.GetColorByColoring(objectColoring);
        UpdateColoringLogic();
    }

    /// <summary>
    /// �� ������Ʈ�� ������ �ڵ���.
    /// </summary>
    /// <param name="jellyColoring"></param>
    public void GetJellied(Coloring jellyColoring)
    {
        _isJellied = true;
        UpdateColoringLogic();
    }

    public void GetUnjellied()
    {
        //�ʿ�: ���� �������� �ð� ȿ��
        _isJellied = false;
        UpdateColoringLogic();
    }

    /// <summary>
    /// ���� �÷����� �� ������Ʈ�� �÷����� ���� �� �ݶ��̴� �� ����.
    /// </summary>
    void UpdateColoringLogic()
    {
        Coloring currentColoring = objectColoring;

        if (ColorManager.instance.mainColoring != currentColoring)
        {
            GetComponent<Collider2D>().enabled = true;
            SetActiveLineRenderer(false);
            //GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            SetActiveLineRenderer(true);
            //GetComponent<SpriteRenderer>().enabled = false;
        }

        if (_isJellied) 
        {
            currentColoring = FindObjectOfType<JellyShooter>().jellyColoring;
            GetComponent<SpriteRenderer>().enabled = false;
            SetActiveLineRenderer(true);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void SetActiveLineRenderer(bool value)
    {
        _lr.startColor = ColorManager.instance.GetHighlightColorByColoring(objectColoring);
        _lr.endColor = ColorManager.instance.GetHighlightColorByColoring(objectColoring);
        _lr.enabled = value;
    }
}
