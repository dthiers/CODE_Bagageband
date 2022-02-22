﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODE_Bagageband.Model
{
    public class Aankomsthal : IObserver<Bagageband>
    {
        // TODO: Hier een ObservableCollection van maken, dan weten we wanneer er vluchten bij de wachtrij bij komen of afgaan.
        public List<Vlucht> WachtendeVluchten { get; private set; }
        public List<Bagageband> Bagagebanden { get; private set; }

        public Aankomsthal()
        {
            WachtendeVluchten = new List<Vlucht>();
            Bagagebanden = new List<Bagageband>();

            // TODO: Als bagageband Observable is, gaan we subscriben op band 1 zodat we updates binnenkrijgen.
            Bagagebanden.Add(new Bagageband("Band 1", 30));
            // TODO: Als bagageband Observable is, gaan we subscriben op band 2 zodat we updates binnenkrijgen.
            Bagagebanden.Add(new Bagageband("Band 2", 60));
            // TODO: Als bagageband Observable is, gaan we subscriben op band 3 zodat we updates binnenkrijgen.
            Bagagebanden.Add(new Bagageband("Band 3", 90));
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            // TODO: Het proces moet straks automatisch gaan, dus als er lege banden zijn moet de vlucht niet in de wachtrij.
            // Dan moet de vlucht meteen naar die band.

            // Denk bijvoorbeeld aan: Bagageband legeBand = Bagagebanden.FirstOrDefault(b => b.AantalKoffers == 0);

            var hasWachtendeVluchten = WachtendeVluchten.Any();

            if (hasWachtendeVluchten)
            {
                WachtendeVluchten.Add(new Vlucht(vertrokkenVanuit, aantalKoffers));
            }
            else
            {
                Bagageband legeBand = Bagagebanden.FirstOrDefault(b => b.AantalKoffers == 0);
                OnNext(legeBand);
            }
            WachtendeVluchten.Add(new Vlucht(vertrokkenVanuit, aantalKoffers));
        }


        public void OnNext(Bagageband value)
        {
            if(value.AantalKoffers == 0 && WachtendeVluchten.Any())
            {
                Bagageband legeBand = value;
                Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
                WachtendeVluchten.RemoveAt(0);

                legeBand.HandelNieuweVluchtAf(volgendeVlucht);
            }
        }
        public void WachtendeVluchtenNaarBand()
        {
            while(Bagagebanden.Any(bb => bb.AantalKoffers == 0) && WachtendeVluchten.Any())
            {
                // TODO: Straks krijgen we een update van een bagageband. Dan hoeven we alleen maar te kijken of hij leeg is.
                // Als dat zo is kunnen we vrijwel de hele onderstaande code hergebruiken en hebben we geen while meer nodig.
                
                Bagageband legeBand = Bagagebanden.FirstOrDefault(bb => bb.AantalKoffers == 0);
                Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
                WachtendeVluchten.RemoveAt(0);

                legeBand.HandelNieuweVluchtAf(volgendeVlucht);
            }
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
