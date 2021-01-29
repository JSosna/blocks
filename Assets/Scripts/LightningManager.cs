using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightningManager : MonoBehaviour
{
    [SerializeField]
    private Light DirectionalLight;

    [SerializeField]
    private LightningPreset Preset;

    [SerializeField, Range(0, 24)]
    private float TimeOfDay;

    private void Update()
    {
        if (Preset == null) return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / 15;
            TimeOfDay %= 24;
            UpdateLightning(TimeOfDay / 24f);
        }
        else
        {
            UpdateLightning(TimeOfDay / 24f);
        }
    }

    private void UpdateLightning(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null) return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
