using UnityEngine;

public class SetMaterialRenderQueue : MonoBehaviour
{

    public Material material;
    public int queueOverrideValue;

    void Start()
    {
        material.renderQueue = queueOverrideValue;
    }

}
