using DPINT_Wk3_Observer.Process;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODE_Bagageband.Model
{
    /// <summary>
    /// Als er een bagageBand vrij komt, wil ik dat weten.
    /// </summary>
    public class Aankomsthal : IObserver<Bagageband>, IObserver<Vlucht>
    {
        // TODO: Hier een ObservableCollection van maken, dan weten we wanneer er vluchten bij de wachtrij bij komen of afgaan.
        public ObservableCollection<Vlucht> WachtendeVluchten { get; private set; }
        public List<Bagageband> Bagagebanden { get; private set; }

        public Aankomsthal()
        {
            WachtendeVluchten = new ObservableCollection<Vlucht>();
            Bagagebanden = new List<Bagageband>();

            CreateBagageBand("Band 1", 30);
            CreateBagageBand("Band 2", 60);
            CreateBagageBand("Band 3", 90);
        }

        private void CreateBagageBand(string naam, int aantalKoffersPerMinuut)
        {
            var band = new Bagageband(naam, aantalKoffersPerMinuut);
            band.Subscribe(this);
            Bagagebanden.Add(band);
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            var vlucht = new Vlucht(vertrokkenVanuit, aantalKoffers);
            vlucht.Subscribe(this);

            var legeBand = Bagagebanden.FirstOrDefault(b => b.AantalKoffers == 0);
            if(legeBand != null)
            {
                legeBand.HandelNieuweVluchtAf(vlucht);
            }
            else
            {
                WachtendeVluchten.Add(vlucht);
            }
        }


        public void OnNext(Bagageband value)
        {
            if (value.AantalKoffers == 0 && WachtendeVluchten.Any())
            {
                Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
                WachtendeVluchten.RemoveAt(0);

                value.HandelNieuweVluchtAf(volgendeVlucht);
            }
        }
        public void OnNext(Vlucht value)
        {
            return;
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
