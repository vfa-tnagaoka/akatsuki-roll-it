using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace UDL.Core
{
	public abstract class AbstractView : MonoBehaviour, IModel
	{
		#region IDisposable implementation

		public virtual void Dispose ()
		{
            if(this != null)
			    Destroy (this.gameObject);
            OnDispose.OnNext();
		}

		#endregion

		protected ICommunicable communicable = new EmptyCommunicable();

        public CompositeDisposable disposables { get; } = new CompositeDisposable();

        public SimpleSubject OnDispose { get; } = new SimpleSubject();

        public void SetCommunicable(ICommunicable x){
			communicable = x;
		}

        public ICommunicable GetCommunicable()
        {
            return communicable;
        }

        protected void MakeCommunication(AbstractView receiver){
			communicable.Communicate (receiver.communicable);
		}

        public virtual void Communicate(ICommunicable communicatable)
        {
            
        }
    }


}
