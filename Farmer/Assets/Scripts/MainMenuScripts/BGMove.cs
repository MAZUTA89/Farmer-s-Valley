using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float BgSpeed;

    Vector2 _meshOffset;
    MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshOffset = _meshRenderer.sharedMaterial.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _meshOffset.x += Time.deltaTime * BgSpeed;
        _meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", _meshOffset);
    }
}
