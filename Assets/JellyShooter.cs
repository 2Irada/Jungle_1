using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShooter : MonoBehaviour
{
    public JellyData data;

    Coloring jellyColoring = Coloring.Red;

    private ColoredObject jelliedObject = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var _hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (_hit.collider != null)
            {
                ColoredObject _obj = _hit.collider.GetComponent<ColoredObject>();
                if (_obj != null)
                {
                    _obj.GetJellied(jellyColoring);
                    jelliedObject = _obj;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (jelliedObject != null)
            {
                jelliedObject.GetUnjellied();
                jelliedObject = null;
            }
        }
    }
}
