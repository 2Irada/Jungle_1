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
        ColorManager.instance.mainColoringChanged += UpdateColoringLogic;
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
        //�ʿ�: ���� �ð� ȿ��
        GetComponent<Collider2D>().enabled = false;
        _isJellied = true;
    }

    public void GetUnjellied()
    {
        //�ʿ�: ���� �������� �ð� ȿ��
        GetComponent<Collider2D>().enabled = true;
        _isJellied = false;
    }

    /// <summary>
    /// ���� �÷����� �� ������Ʈ�� �÷����� ���� �� �ݶ��̴� �� ����.
    /// </summary>
    void UpdateColoringLogic()
    {
        if (_isJellied) return;

        if (ColorManager.instance.mainColoring != objectColoring) 
        {
            GetComponent<Collider2D>().enabled = true; 
        }
        else 
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
