using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UDL.Core.Helper;

namespace UDL.Core
{
	public class ScreenLoader
	{
		private static float maxAspectRatio = 2960.0f / 1440.0f;
		private static LimitedCanvas limitedCanvas;
		private static GameObject background;
		private static Camera camera;

		public static Canvas GetCanvas()
		{
			return limitedCanvas.GetComponent<Canvas>();
		}

		public static Camera GetCamera()
		{
			return camera;
		}

		public static void SetCameraBackgroundColor(Color color)
		{
			if (camera)
			{
				camera.backgroundColor = color;
			}
		}

		public static void SetMaxAspectRatio(float ratio)
		{
			maxAspectRatio = ratio;
		}

		public static Vector2 CanvasRefRez
		{
			get
			{
				return limitedCanvas.GetComponent<CanvasScaler>().referenceResolution;
			}
		}

		public static Vector2 CanvasSize
		{
			get
			{
				return limitedCanvas.GetComponent<RectTransform>().sizeDelta;
			}
		}

		public static Vector3 CanvasScale
		{
			get
			{
				return limitedCanvas.transform.localScale;
			}
		}

		private static void SetUpCanvas()
		{
			Canvas canvasPrefab = Resources.Load<Canvas>("UDL/Canvas");

			if (canvasPrefab == null)
			{
				throw new System.Exception("Create a canvas prefab here: Resources/GameSolution/Canvas.prefab");
			}

			Canvas canvasGo = Object.Instantiate(canvasPrefab);

			limitedCanvas = canvasGo.gameObject.AddComponent<LimitedCanvas>();
			limitedCanvas.SetMaxAspectRatio(maxAspectRatio);

			camera = limitedCanvas.GetComponentInChildren<Camera>();

            if(camera == null)
            {
                var go = new GameObject("Camera");
                camera = go.AddComponent<Camera>();
                go.transform.SetParent(limitedCanvas.transform);
            }

			camera.clearFlags = CameraClearFlags.SolidColor;
            camera.cullingMask = 0;

			GameObject backgroundPrefab = Resources.Load<GameObject>("UDL/Background");

			if (backgroundPrefab != null)
			{
				background = Object.Instantiate(backgroundPrefab);
				limitedCanvas.Insert(background, 1);
			}
		}

		public static T Load<T>(string path, int slot = 3)
		{
			if (limitedCanvas == null)
			{
				SetUpCanvas();
			}

			var prefab = Resources.Load<GameObject>(path);

			if (prefab == null)
			{
				throw new System.Exception(path + " doesn't exist");
			}

			var go = Object.Instantiate(prefab);

			limitedCanvas.Insert(go, slot);

			T view = go.GetComponent<T>();

			return view;
		}

		public static T LoadElement<T>(string path) where T : class
		{
			if (limitedCanvas == null)
			{
				SetUpCanvas();
			}

			var go = Object.Instantiate(Resources.Load<GameObject>(path));

			T view = go.GetComponent<T>();

			if (view == null)
			{
				throw new System.Exception("Not found");
			}

			return view;
		}

		public static T Load<T>(string path) where T : UnityEngine.Object
		{
			if (string.IsNullOrEmpty(path)) throw new System.Exception("Path Not found " + path);

			return Resources.Load<T>(path);
		}

		public static void Reset2D()
		{
			Object.Destroy(limitedCanvas.gameObject);
			Object.Destroy(background.gameObject);
			limitedCanvas = null;
			background = null;
			camera = null;
		}
	}
}