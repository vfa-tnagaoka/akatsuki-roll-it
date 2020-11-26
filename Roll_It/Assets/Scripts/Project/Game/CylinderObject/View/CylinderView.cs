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

    [SerializeField] private Transform StarPosition;
    [SerializeField] private Transform EndPosition;

    private bool isOpen = false;

    private void Start() {
        this.isOpen = false;

        // Open();
        StartCoroutine(OpenAndClose());
    }

    public void Open()
    {
        this.isOpen = true;

        this.CylinderGo.transform.DOLocalMoveZ(5,2f);
        this.LineGo.transform.DOScaleZ(5,2f);
        
    }

    public void Close()
    {
        this.isOpen = false;

        this.CylinderGo.transform.DOLocalMoveZ(0.5f,2f);
        this.LineGo.transform.DOScaleZ(1,2f);
    }

    IEnumerator OpenAndClose()
    {
        Open();

        yield return new WaitForSeconds(6);

        Close();
    }
}
