using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredObject : MonoBehaviour
{
    public Coloring originalColoring = new Coloring();
    [HideInInspector]
    public Coloring currentColoring = new Coloring();

    private void Start()
    {
        InitializeColoring();
    }
    /// <summary>
    /// ������Ʈ�� ���� ���� ����.
    /// </summary>
    private void InitializeColoring()
    {
        currentColoring = originalColoring;
        GetComponent<SpriteRenderer>().color = ColorManager.instance.GetColorByColoring(originalColoring);
    }
    /// <summary>
    /// �� ������Ʈ�� ������ �ڵ���.
    /// </summary>
    /// <param name="jellyColoring"></param>
    public void GetJellied(Coloring jellyColoring)
    {
        currentColoring = jellyColoring;
        //�ʿ�: ���� �ð� ȿ��
    }
    /// <summary>
    /// ���� �÷����� ���� 
    /// </summary>
    void UpdateByMainColoring()
    {

    }
}
