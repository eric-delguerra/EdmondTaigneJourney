using System;
using UnityEngine;

public class HidePlayer : MonoBehaviour
{
    public SpriteRenderer EdSpriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("CanHide"))
        {
            EdSpriteRenderer.color = new Color(.3f, .3f, .3f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("CanHide"))
        {
            EdSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
