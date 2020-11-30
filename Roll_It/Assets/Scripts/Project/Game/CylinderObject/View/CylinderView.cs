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
    [SerializeField] private GameObject LineGo;
    [SerializeField] Color[] colors;

    public bool IsOpen = false;

    public int ID;

    private Dictionary<int, CylinderView> belowObjects = new Dictionary<int, CylinderView>();
    private Dictionary<int, CylinderView> aboveObjects = new Dictionary<int, CylinderView>();

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

    public bool Close()
    {
        if (this.aboveObjects.Count <= 0)
        {
            this.IsOpen = false;

            this.CylinderGo.transform.DOLocalMoveZ(0.5f, 2f);
            this.LineGo.transform.DOScaleZ(1, 2f);

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
        Close();
        this.belowObjects.Clear();
        this.aboveObjects.Clear();
        // this.LineGo.transform.localScale.Set(1,1,1);
        // this.LineGo.transform.localPosition = new Vector3(0,0,0.5f);
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

        // Debug.Log("OnTriggerEnter --> " + this.ID + " -above- " + this.aboveObjects.Count + " -below- " + this.belowObjects.Count);
    }
}
