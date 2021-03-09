using UnityEngine;

public static class SkyboxManager
{
    public static void SetSkybox (Material skyboxMaterial)
    {
        if (skyboxMaterial == null)
        {
            throw new SkyboxException("skybox material invalid");
        }
        RenderSettings.skybox = skyboxMaterial;
    }
}