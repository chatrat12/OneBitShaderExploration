using UnityEngine;

[ExecuteInEditMode]
public class OneBitShader : MonoBehaviour
{
    private Material _imageEffectMaterial;

    [SerializeField]
    [Range(1, 50)]
    private int _pixelSize = 5;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        LazyInstantiateMaterial();

        _imageEffectMaterial.SetFloat("_PixelSize", _pixelSize);
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
