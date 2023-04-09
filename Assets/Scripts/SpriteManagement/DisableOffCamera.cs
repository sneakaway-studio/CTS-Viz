using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOffCamera : MonoBehaviour
{
    public bool isVisible = true;
    public SpriteRenderer spriteRenderer;

    [Tooltip("All the components (except renderer")]
    public List<Behaviour> components;


    private void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        SetAll(false);
    }
    private void Start() { } /* To disable in Inspector */



    private void OnBecameVisible()
    {
        SetAll(true);
    }
    private void OnBecameInvisible()
    {
        SetAll(false);
    }

    void SetAll(bool state)
    {
        isVisible = state;
        foreach (Behaviour component in components)
        {
            component.enabled = state;
        }
        if (isVisible)
            spriteRenderer.color = new Color(1, 1, 1, 1);
        else
            spriteRenderer.color = new Color(0, 0, 0, 0);
    }

}
