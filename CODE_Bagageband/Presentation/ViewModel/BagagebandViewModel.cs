﻿using CODE_Bagageband.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODE_Bagageband.ViewModel
{
    public class BagagebandViewModel : ViewModelBase, IObserver<Bagageband>
    {
        private string _vluchtVertrokkenVanuit;
        public string VluchtVertrokkenVanuit
        {
            get { return _vluchtVertrokkenVanuit; }
            set { _vluchtVertrokkenVanuit = value; RaisePropertyChanged("VluchtVertrokkenVanuit"); }
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set { _aantalKoffers = value; RaisePropertyChanged("AantalKoffers"); }
        }

        private string _naam;
        private readonly IDisposable _unsubscribe;

        public string Naam
        {
            get { return _naam; }
            set { _naam = value; RaisePropertyChanged("Naam"); }
        }

        public BagagebandViewModel(Bagageband band)
        {
            _unsubscribe = band.Subscribe(this);
            OnNext(band);
            //Update(band);
        }

        public void Update(Bagageband value)
        {
            VluchtVertrokkenVanuit = value.VluchtVertrokkenVanuit;
            AantalKoffers = value.AantalKoffers;
            Naam = value.Naam;
        }

        public void OnNext(Bagageband value)
        {
            VluchtVertrokkenVanuit = value.VluchtVertrokkenVanuit;
            AantalKoffers = value.AantalKoffers;
            Naam = value.Naam;
        }

        #region unused
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
