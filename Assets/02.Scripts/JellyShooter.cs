using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyShooter : MonoBehaviour
{
    public JellyData data;
    public JellyBullet jellyBullet;
    public GameObject slimeHeadGraphic;

    public Coloring jellyColoring = Coloring.Red;

    private ColoredObject jelliedObject = null;

    private void Start()
    {
        UpdateHeadColor();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (jelliedObject == null)
            {
                var _hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (_hit.collider != null)
                {
                    ColoredObject _obj = _hit.collider.GetComponent<ColoredObject>();
                    if (_obj != null)
                    {
                        _obj.GetJellied(jellyColoring);
                        jelliedObject = _obj;
                        ShootJelly(_obj.transform);
                    }
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

    private void ShootJelly(Transform target)
    {
        slimeHeadGraphic.SetActive(false);
        jellyBullet.transform.position = slimeHeadGraphic.transform.position;
        jellyBullet.SetTarget(target);
        jellyBullet.GetComponent<SpriteRenderer>().color = ColorManager.instance.GetColorByColoring(jellyColoring);
        jellyBullet.gameObject.SetActive(true);
    }

    private void UpdateHeadColor()
    {
        slimeHeadGraphic.GetComponent<SpriteRenderer>().color = ColorManager.instance.GetColorByColoring(jellyColoring);
    }
}
