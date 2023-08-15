using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBullet : MonoBehaviour
{
    public JellyData data;
    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > data.jellyBulletSpeed * Time.deltaTime * 2f)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3) direction * data.jellyBulletSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = target.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            //¡©∏Æ ¿·Ωƒ ¿Ã∆Â∆Æ Ω««‡
            gameObject.SetActive(false);
        }
    }
}
