using UnityEngine;

public class AutoTextureTiler : MonoBehaviour
{
    [SerializeField] private Vector2 _textureSize = Vector3.one;

    private void Awake()
    {
        Vector2 newTextureScale = new Vector2(transform.lossyScale.x / _textureSize.x, transform.lossyScale.y / _textureSize.y);
        GetComponent<Renderer>().material.mainTextureScale = newTextureScale;
    }
}
