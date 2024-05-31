using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class SetKeywordPerCamera : MonoBehaviour {

    [SerializeField] protected Tuner tuner = new();

    public Camera AttachedCamera { get; protected set; }

    #region unity
    void OnEnable() {
        AttachedCamera = GetComponent<Camera>();

        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }
    void OnDisable() {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }
    #endregion

    #region events
    void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera) {
        if (camera != AttachedCamera) return;

        if (tuner.setBaseColor) {
            foreach (var gk in Shader.globalKeywords) {
                if (gk.name.StartsWith("_BASECOLORMODE_")) {
                    Shader.DisableKeyword(gk.name);
                }
            }
            Shader.EnableKeyword(tuner.baseColor.ToString());
        }
    }
    void OnEndCameraRendering(ScriptableRenderContext context, Camera camera) {
        if (camera != AttachedCamera) return;
        foreach (var gk in Shader.globalKeywords) {
            if (gk.name.StartsWith("_BASECOLORMODE_")) {
                Shader.DisableKeyword(gk.name);
            }
        }
    }
    #endregion

    #region declarations
    public enum BaseColorMode {
        _BASECOLORMODE____ = 0,
        _BASECOLORMODE_RED,
        _BASECOLORMODE_BLUE,
    }
    [System.Serializable]
    public class Tuner {
        public bool setBaseColor;
        public BaseColorMode baseColor;
    }
    #endregion
}