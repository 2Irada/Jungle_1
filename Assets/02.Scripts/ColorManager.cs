using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    [SerializeField]
    public Coloring mainColoring = Coloring.Red;
    
    [Header("Colors")]
    public Color black;
    public Color red;
    public Color yellow;
    public Color green;

    public delegate void MainColoringChanged();
    public MainColoringChanged mainColoringChanged;

    #region Monobehavior Methods
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //����׿�
        if (Input.GetKeyDown(KeyCode.Space))
        {
        //    AutoSwitchMainColoring();
        }
    }
    #endregion

    /// <summary>
    /// ���� �÷����� ����. ���� �÷����� �� �Լ��� ���ؼ��� ������ ��.
    /// </summary>
    /// <param name="targetColoring"></param>
    public void SwitchMainColoring(Coloring targetColoring)
    {
        mainColoring = targetColoring;
        Camera.main.backgroundColor = GetColorByColoring(targetColoring);
        mainColoringChanged?.Invoke();
    }

    /// <summary>
    /// ���� ������ ���� �ø����� ����
    /// </summary>
    public void AutoSwitchMainColoring()
    {
        if (mainColoring == Coloring.Black) { SwitchMainColoring(Coloring.Red); }
        else if (mainColoring == Coloring.Red) { SwitchMainColoring(Coloring.Yellow); }
        else if (mainColoring == Coloring.Yellow) { SwitchMainColoring(Coloring.Green); }
        else if (mainColoring == Coloring.Green) { SwitchMainColoring(Coloring.Red); }
    }

    /// <summary>
    /// �÷����� �����Ǵ� ���� Color ���� ������.
    /// </summary>
    /// <param name="coloring"></param>
    /// <returns></returns>
    public Color GetColorByColoring(Coloring coloring)
    {
        switch (coloring)
        {
            case Coloring.Black:
                return black;
            case Coloring.Red:
                return red;
            case Coloring.Yellow:
                return yellow;
            case Coloring.Green:
                return green;
            default:
                Debug.LogError("Coloring out of bounds.");
                return black;
        }
    }
}

public enum Coloring
{
    Black,
    Red,
    Yellow,
    Green
}
