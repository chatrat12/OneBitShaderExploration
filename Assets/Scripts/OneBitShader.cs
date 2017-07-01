using UnityEngine;

[ExecuteInEditMode]
public class OneBitShader : MonoBehaviour
{
    private Material _imageEffectMaterial;

    [SerializeField]
    [Range(1, 50)]
    private int _pixelSize = 5;
    [SerializeField]
    [Range(2, 32)]
    private int _shades = 8;
    [SerializeField]
    [Range(0f, 2f)]
    private float _brightness = 1;
    [SerializeField]
    //[Range(1, 16)]
    //private int _spacingTest = 2;
    

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        LazyInstantiateMaterial();

        _imageEffectMaterial.SetFloat("_PixelSize", _pixelSize);
        _imageEffectMaterial.SetFloat("_Shades", _shades);
        _imageEffectMaterial.SetFloat("_Brightness", _brightness);
        //_imageEffectMaterial.SetFloat("_SpacingTest", _spacingTest);
        Graphics.Blit(source, destination, _imageEffectMaterial);
    }

    private void LazyInstantiateMaterial()
    {
        if(_imageEffectMaterial == null)
        {
            _imageEffectMaterial = new Material(Shader.Find("Hidden/OneBitShader"));
        }
    }
}
