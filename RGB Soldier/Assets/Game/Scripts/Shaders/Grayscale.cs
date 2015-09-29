using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Game/Scripts/Shaders/GrayscaleShader")]
public class Grayscale : MonoBehaviour {
    public Texture  textureRamp;

    [Range(-1.0f,1.0f)]
    public float    rampOffset;

    public Shader shader;

    private Material m_Material;


    protected virtual void Start()
    {
        // Disable if we don't support image effects
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        // Disable the image effect if the shader can't
        // run on the users graphics card
        if (!shader || !shader.isSupported)
            enabled = false;
    }


    protected Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(shader);
                m_Material.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_Material;
        }
    }


    protected virtual void OnDisable()
    {
        if (m_Material)
        {
            DestroyImmediate(m_Material);
        }
    }

    // Called by camera to apply image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination) {
        material.SetTexture("_RampTex", textureRamp);
        material.SetFloat("_RampOffset", rampOffset);
        Graphics.Blit (source, destination, material);
    }
}