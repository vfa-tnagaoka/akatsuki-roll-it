using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace UDL.Core
{
	public abstract class AbstractPresenter : IDisposable 
	{
        IModel _model;
		List<MonoBehaviour> _views;

		protected CompositeDisposable disposables = new CompositeDisposable ();

		public AbstractPresenter (IModel model, MonoBehaviour view, MonoBehaviour view2 = null, MonoBehaviour view3 = null, MonoBehaviour view4 = null, MonoBehaviour view5 = null)
		{
            var list = new List<MonoBehaviour>() { view };

            if (view2 != null)
                list.Add(view2);
            if (view3 != null)
                list.Add(view3);
            if (view4 != null)
                list.Add(view4);
            if (view5 != null)
                list.Add(view5);

            HiddenConstructor (model, list);
		}

		public AbstractPresenter (IModel model, List<MonoBehaviour> views)
		{
			HiddenConstructor (model, views);
		}

		void HiddenConstructor(IModel model, List<MonoBehaviour> views)
		{
			this._model = model;
			this._views = views;

			foreach (var view in views) {
				AbstractView abstractView = view.GetComponent<AbstractView> ();
				if (abstractView != null) {
					abstractView.SetCommunicable (model);
				}
			}
        }

		#region IDisposable implementation

		public void Dispose ()
		{
			disposables.Dispose ();
			foreach (var view in _views) {
                if (view == null) continue;
                var abstractView = view.GetComponent<AbstractView>();
                if (abstractView != null)
                {
                    abstractView.Dispose();
                }
                else
                {
                    UnityEngine.Object.Destroy(view.gameObject);
                }
			}
		}

		#endregion

	}
}