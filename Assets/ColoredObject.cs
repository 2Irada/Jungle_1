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
    /// 오브젝트의 원본 색을 설정.
    /// </summary>
    private void InitializeColoring()
    {
        currentColoring = originalColoring;
        GetComponent<SpriteRenderer>().color = ColorManager.instance.GetColorByColoring(originalColoring);
    }

    public void GetJellied(Coloring jellyColoring)
    {
        currentColoring = jellyColoring;
        //필요: 젤리 시각 효과
    }
}
