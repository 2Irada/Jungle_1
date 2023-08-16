using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredObject : MonoBehaviour
{
    public Coloring objectColoring = new Coloring();

    private bool _isJellied = false;

    private void Start()
    {
        InitializeColoring();
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
    if (_isJellied) currentColoring = FindObjectOfType<JellyShooter>().jellyColoring;
        if (ColorManager.instance.mainColoring != currentColoring)
        {
            GetComponent<Collider2D>().enabled = true;
            //GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            //GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void SetActiveAllSpriteRenderers(bool value)
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
        spriteRenderers.Add(GetComponent<SpriteRenderer>());
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());

        foreach (SpriteRenderer spriteRenderer in spriteRenderers) spriteRenderer.enabled = value;
    }
}
