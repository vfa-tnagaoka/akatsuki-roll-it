using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UniRx;
using System.Collections.ObjectModel;

namespace UDL.Core
{
    

    public abstract class AbstractModel : IModel
    {
        public SimpleSubject OnDispose { get; } = new SimpleSubject();

        public GameObject gameObject { get; set; }

        public CompositeDisposable disposables { get; } = new CompositeDisposable();

        #region IDisposable implementation
        public virtual void Dispose()
        {
            OnDispose.OnNext();
            disposables.Dispose();
        }
        #endregion

        #region ICommunicatable implementation

        public virtual void Communicate(ICommunicable communicatable)
        {

        }

        #endregion
       

    }
}