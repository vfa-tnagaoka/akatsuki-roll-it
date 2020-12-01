﻿using System.Collections;
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

        Material material = new Material(Shader.Find("Standard"));
        material.color = color;
        CylinderGo.GetComponent<MeshRenderer>().material = material;
        LineGo.transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
    }

    public void Open()
    {

        this.CylinderGo.transform.DOLocalMoveZ(5, 1f);
        this.LineGo.transform.DOScaleZ(5, 1f).OnComplete(() =>
        {
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
            // Debug.Log("--> finsh scale");
            this.OnClose();
        });

        // Debug.Log("---> 1--> " + this.aboveObjects.Count);
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
            if (this.aboveObjects.Count > 0 && !this.IsOpen)
            {
                // Debug.Log("-- open again!");
                this.closeMoveTweener.Kill();
                this.closeScaleTweener.Kill();
                Open();
            }
        }
    }
}
