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

    #region Monobehavior Methods
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AutoSwitchMainColoring();
        }
    }
    #endregion

    public void SwitchMainColoring(Coloring targetColoring)
    {
        mainColoring = targetColoring;
        switch (targetColoring)
        {
            case Coloring.Red:
                Camera.main.backgroundColor = red;
                break;
            case Coloring.Yellow:
                Camera.main.backgroundColor = yellow;
                break;
            case Coloring.Green:
                Camera.main.backgroundColor = green;
                break;
            default:
                Debug.LogError("Trying to change main color to black.");
                break;
        }
    }

    public void AutoSwitchMainColoring()
    {
        if (mainColoring == Coloring.Black) { SwitchMainColoring(Coloring.Red); }
        else if (mainColoring == Coloring.Red) { SwitchMainColoring(Coloring.Yellow); }
        else if (mainColoring == Coloring.Yellow) { SwitchMainColoring(Coloring.Green); }
        else if (mainColoring == Coloring.Green) { SwitchMainColoring(Coloring.Red); }
    }

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
