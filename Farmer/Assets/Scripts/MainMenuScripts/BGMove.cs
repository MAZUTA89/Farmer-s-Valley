using System;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float BgSpeed;

    Vector2 _meshOffset;
    MeshRenderer _meshRenderer;

    void Start()
    {
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Screen.width / Screen.height;
        
        transform.localScale = new Vector3 (width, height, 0);
        
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshOffset = _meshRenderer.sharedMaterial.mainTextureOffset;
    }

    void Update()
    {
        _meshOffset.x += Time.deltaTime * BgSpeed;
        _meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", _meshOffset);
    }
}
