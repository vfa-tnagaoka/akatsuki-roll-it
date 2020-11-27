using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UDL.Core;

public class CylinderView : AbstractView
{
    [SerializeField] private GameObject CylinderGo;
    [SerializeField] private GameObject LineGo;

    [SerializeField] Color[] colors;

    public bool IsOpen = false;

    public int Index;

    private void Start()
    {
        this.IsOpen = true;
    }

    public void RandomColor()
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = this.colors[UnityEngine.Random.Range(0, colors.Length)];
        CylinderGo.GetComponent<MeshRenderer>().material = material;
        LineGo.transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
    }

    public void Open()
    {
        this.IsOpen = true;

        this.CylinderGo.transform.DOLocalMoveZ(5, 2f);
        this.LineGo.transform.DOScaleZ(5, 2f);

    }

    public void Close()
    {
        this.IsOpen = false;

        this.CylinderGo.transform.DOLocalMoveZ(0.5f, 2f);
        this.LineGo.transform.DOScaleZ(1, 2f);
    }

    public void ResetCylinder()
    {
        Close();
        // this.LineGo.transform.localScale.Set(1,1,1);
        // this.LineGo.transform.localPosition = new Vector3(0,0,0.5f);
    }
}
