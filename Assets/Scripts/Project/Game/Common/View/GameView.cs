using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UDL.Core;
using UDL.Core.UI;

namespace Project.Game.View
{
    public class GameView : AbstractView
    {
        [SerializeField] LayerMask layer;
        [SerializeField] private Transform cylinderParent;
        [SerializeField] private WinScreen winScreen;

        [SerializeField] private List<CylinderView> mapContent = new List<CylinderView>();
        [SerializeField] private Transform[] Levels;

        private GameObject cylinderPrefab;
        private int currentIndex = 0;

        [SerializeField] private int numberCylinder;
        [SerializeField] private int closeCylinder;

        [SerializeField] Color[] colors;
        private Color[] tempColor;

        private System.DateTime startTime;
        private bool canRayCast = false;

        public WinScreen WinScreen
        {
            get
            {
                return this.winScreen;
            }

            set
            {
                this.winScreen = value;
            }
        }


        private void Start()
        {           
            AdjustManager.Instance.Initialized();
            Init();           
        }

        public void Init()
        {
            this.canRayCast = false;
            this.cylinderPrefab = Resources.Load<GameObject>(ResourcePathConfig.CylindePath);
            winScreen.Next.Subscribe(CreateMap).AddTo(gameObject);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientSkyColor = Color.white;
            RenderSettings.ambientIntensity = 0.1f;
            CreateMap();
        }

        private void Update()
        {
            CheckRaycast();
        }

        public void CheckRaycast()
        {
            if (Input.GetMouseButtonDown(0) && this.canRayCast)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit, 1000, this.layer))
                {
                    CylinderView view;
                    if (raycastHit.collider.CompareTag("Line"))
                    {
                        view = raycastHit.collider.transform.parent.parent.GetComponent<CylinderView>();
                    }
                    else
                    {
                        view = raycastHit.collider.transform.parent.GetComponent<CylinderView>();
                    }

                    if (view.IsOpen)
                    {
                        var isClose = view.Close();
                    }
                }
            }
        }

        private void OnCheckWin()
        {
            this.closeCylinder++;
            if (this.closeCylinder >= this.numberCylinder)
            {
                this.closeCylinder = 0;
                StartCoroutine(Win());
            }
        }

        public IEnumerator Win()
        {
            yield return new WaitForSeconds(0.5f);

            this.canRayCast = false;
            winScreen.Show((float)((System.DateTime.Now - startTime).TotalSeconds));
        }

        public void CreateMap()
        {
            this.ResetMap();
            int index = UnityEngine.Random.Range(0, Levels.Length);
            LoadMapLevel(index);
        }

        public void LoadMapLevel(int indexLv)
        {
            this.canRayCast = true;
            Transform level = this.Levels[indexLv];
            level.SetActive(true);

            numberCylinder = level.childCount;
            float yPos = 0;
            tempColor = colors;
            for (int i = 0; i < numberCylinder; i++)
            {
                var objPos = level.GetChild(i);
                Vector3 posCylinder = new Vector3(objPos.position.x, yPos, objPos.position.z);
                this.InitCylinder(i, posCylinder, objPos.rotation);
                objPos.SetActive(false);
                yPos += 0.01f;
            }

            startTime = System.DateTime.Now;
        }

        public void InitCylinder(int index, Vector3 position, Quaternion quaternion)
        {
            GameObject cylinderGo;
            CylinderView cylinderView;
            if (index < this.mapContent.Count)
            {
                cylinderGo = this.mapContent[index].gameObject;
                cylinderView = this.mapContent[index];
            }
            else
            {
                cylinderGo = Instantiate(this.cylinderPrefab, this.cylinderParent);
                cylinderView = cylinderGo.GetComponent<CylinderView>();
                this.mapContent.Add(cylinderView);
            }

            cylinderGo.SetActive(true);
            cylinderGo.transform.position = position;
            cylinderGo.transform.rotation = quaternion;

            cylinderView.ID = index;
            int positionColor = UnityEngine.Random.Range(0, tempColor.Length);
            cylinderView.RandomColor(tempColor[positionColor]);
            tempColor = (Color[])RemoveAt(tempColor, positionColor);

            cylinderView.OnClose -= OnCheckWin;
            cylinderView.OnClose += OnCheckWin;

            cylinderView.Open();

        }
        private Array RemoveAt(Array source, int index)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (0 > index || index >= source.Length)
                throw new ArgumentOutOfRangeException("index", index, "index is outside the bounds of source array");

            Array dest = Array.CreateInstance(source.GetType().GetElementType(), source.Length - 1);
            Array.Copy(source, 0, dest, 0, index);
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        public void ResetMap()
        {
            this.numberCylinder = 0;
            this.closeCylinder = 0;

            if (this.mapContent.Count <= 0) return;
            foreach (var cylinder in this.mapContent)
            {
                cylinder.ResetCylinder();
                cylinder.SetActive(false);
            }
        }

    }
}
