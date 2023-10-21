using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWebcamBG : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public RawImage bgImage;

    void Start()
    {
        try
        {
            webCamTexture = new WebCamTexture();
            bgImage.material.mainTexture = webCamTexture;
            webCamTexture.Play();
        }
        finally
        {
            Debug.Log("No camera available.");
        }
    }
}