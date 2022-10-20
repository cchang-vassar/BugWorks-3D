using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
[ExecuteInEditMode]

public class lightsFallOff : MonoBehaviour
{
    public void OnEnable()
    {
        Lightmapping.RequestLightsDelegate testDel = (Light[] requests, Unity.Collections.NativeArray<LightDataGI> lightsOutput) =>
        {
            PointLight point = new PointLight();
            Cookie cookie = new Cookie();
            LightDataGI ld = new LightDataGI();

            for (int i = 0; i < requests.Length; i++)
            {
                Light l = requests[i];
                LightmapperUtils.Extract(l, ref point); LightmapperUtils.Extract(l, out cookie); ld.Init(ref point, ref cookie);
                ld.cookieID = l.cookie?.GetInstanceID() ?? 0;
                ld.falloff = FalloffType.InverseSquared;
                lightsOutput[i] = ld;
            }
        };
        Lightmapping.SetDelegate(testDel);
    }
    void OnDisable()
    {
        Lightmapping.ResetDelegate();
    }
}
