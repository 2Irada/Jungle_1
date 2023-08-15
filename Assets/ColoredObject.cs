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
    /// <summary>
    /// 이 오브젝트를 젤리가 뒤덮음.
    /// </summary>
    /// <param name="jellyColoring"></param>
    public void GetJellied(Coloring jellyColoring)
    {
        currentColoring = jellyColoring;
        //필요: 젤리 시각 효과
    }
    /// <summary>
    /// 메인 컬러링에 따라 
    /// </summary>
    void UpdateByMainColoring()
    {

    }
}
