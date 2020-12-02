using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UDL.Core;
using System;

public class CylinderView : AbstractView
{
    [SerializeField] private GameObject CylinderGo;
    [SerializeField] private Transform posLeft;
    [SerializeField] private Transform posRight;
    [SerializeField] private GameObject LineGo;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] Color[] colors;
    [SerializeField] Texture rollPageTexture;

    private Tweener closeMoveTweener;
    private Tweener closeScaleTweener;
    public bool IsOpen = false;

    public int ID;

    private Dictionary<int, CylinderView> belowObjects = new Dictionary<int, CylinderView>();
    private Dictionary<int, CylinderView> aboveObjects = new Dictionary<int, CylinderView>();

    public Action OnClose = new Action(() => { });

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

    public void RandomColor(Color color)
    {
        Material materialLine = new Material(Shader.Find("Standard"));
        materialLine.color = color;

        Material materialCydinder = new Material(Shader.Find("Standard"));
        materialCydinder.color = color;
        materialCydinder.mainTexture = rollPageTexture;

        CylinderGo.transform.GetChild(0).GetComponent<MeshRenderer>().material = materialCydinder;
        LineGo.transform.GetChild(0).GetComponent<MeshRenderer>().material = materialLine;
    }

    public void Open()
    {

        this.CylinderGo.transform.DOLocalMoveZ(5, 1f);
        this.LineGo.transform.DOScaleZ(5.3f, 1f).OnComplete(() =>
        {
            this.CylinderGo.transform.GetComponent<CapsuleCollider>().enabled = true;
            this.IsOpen = true;
        });
    }

    public bool Close()
    {
        if (!this.IsOpen) return false;

        this.IsOpen = false;
        this.closeMoveTweener = this.CylinderGo.transform.DOLocalMoveZ(0.5f, 2f);
        this.closeScaleTweener = this.LineGo.transform.DOScaleZ(1, 2f).OnComplete(() =>
        {
            this.CylinderGo.transform.GetComponent<CapsuleCollider>().enabled = false;
            this.OnClose();
        });

        if (this.aboveObjects.Count <= 0)
        {
            if (this.belowObjects.Count <= 0) return true;
            foreach (var belowObject in this.belowObjects)
            {
                belowObject.Value.RemoveAboveObject(this.ID);
            }

            return true;
        }

        return false;
    }

    public void RemoveAboveObject(int id)
    {
        if (this.aboveObjects.ContainsKey(id))
        {
            this.aboveObjects.Remove(id);
        }
    }

    public void ResetCylinder()
    {
        this.belowObjects.Clear();
        this.aboveObjects.Clear();
        this.LineGo.transform.localScale.Set(1, 1, 1);
        this.LineGo.transform.localPosition = new Vector3(0, 0.067f, -0.19f);
        this.CylinderGo.transform.localPosition = new Vector3(0, 0.5f, 0.5f);
        this.IsOpen = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            Debug.Log("trigger line" + other.name);
            var cylinderView = other.transform.parent.parent.GetComponent<CylinderView>();
            if (cylinderView.transform.position.y > this.transform.position.y)
            {
                this.aboveObjects.Add(cylinderView.ID, cylinderView);
            }
            else
            {
                this.belowObjects.Add(cylinderView.ID, cylinderView);
            }
        }

    }

    public void OnCylinderTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {

            if (this.aboveObjects.Count > 0)
            {
                this.closeMoveTweener.Kill();
                this.closeScaleTweener.Kill();
                Open();
            }
        }
    }
}
