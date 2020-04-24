using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMpilkarzezad.Model
{
    class Pilkarz
    {
        public string imie;
        public string nazwisko;

        public uint wiek;
        public uint waga;

        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                imie = value;
            }
        }

        public string Nazwisko
        {
            get
            {
               return nazwisko;
            }
            set
            {
                nazwisko = value;
            }
        }

        public uint Wiek
        {
            get
            {
                return wiek;
            }
            set
            {
                wiek = value;
            }
        }


        public uint Waga
        {
            get
            {
                return waga;
            }
            set
            {
                waga = value;
            }
        }
       
        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
           Imie = imie;
           Wiek = wiek;
           Nazwisko = nazwisko;
           Waga = waga;
        }

        public Pilkarz() { }

        

        public void P1(Pilkarz pilkarz)
        {
            Imie = pilkarz.Imie;
            Nazwisko = pilkarz.Nazwisko;
            Wiek = pilkarz.Wiek;
            Waga = pilkarz.Waga;
       }


        public override string ToString()
        {
            return $"{Imie} {Nazwisko}, lat: {Wiek}, waga: {Waga} kg";
        }

       
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Pilkarz pilkarz = obj as Pilkarz;

            return (this.Wiek == pilkarz.Wiek && this.Imie == pilkarz.Imie 
                &&this.Nazwisko == pilkarz.Nazwisko
                && this.Waga == pilkarz.Waga);
        }
    }
}
