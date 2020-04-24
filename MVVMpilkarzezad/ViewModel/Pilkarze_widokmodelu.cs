using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using MVVMpilkarzezad.Model;
using MVVMpilkarzezad.ViewModel.BaseClass;


namespace MVVMpilkarzezad.ViewModel
{
   

    internal class Pilkarze_widokmodelu : ViewModelBase
    {
        
        private string imie = "";

        private string nazwisko = "";

        private uint? wiek = 0;

        private uint? waga = 0;

        

        public string Imie
        {
            get
            {
                return imie;
            }
            set
            {
                imie = value;

                onPropertyChanged(nameof(Imie));
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

                onPropertyChanged(nameof(Nazwisko));
            }
        }

        public uint? Wiek
        {
            get
            {
                return wiek;
            }
            set
            {
                wiek = value;
                onPropertyChanged(nameof(Wiek));
            }
        }
        public uint? Waga
        {
            get
            {
                return waga;
            }
            set
            {
                waga = value;

                onPropertyChanged(nameof(Waga));
            }
        }

        private Pilkarz wybranypilkarz = null;

        public Pilkarz Wybranypilkarz
        {
            get
            {
                return wybranypilkarz;
            }
            set
            {
                wybranypilkarz = value;

                onPropertyChanged(nameof(Wybranypilkarz));

                if (Osoba.CanExecute(null))
                {
                    Osoba.Execute(null);
                }
            }
        }

        private BindingList<Pilkarz> listapilkarzy = new BindingList<Pilkarz>();

        public BindingList<Pilkarz> Listapilkarzy
        {
            get
            {
                return listapilkarzy;
            }
            set
            {
                listapilkarzy = value;

                onPropertyChanged(nameof(Listapilkarzy));
            }
        }
        private void Czysc()
        {
            Imie = "";
            Nazwisko = "";
            Wiek = 0;
            Waga = 0;
            onPropertyChanged(nameof(Imie), nameof(Nazwisko), nameof(Wiek), nameof(Waga));
        }

        

        private bool niepuste
        {
            get
            {
                return (!string.IsNullOrEmpty(Imie) && !string.IsNullOrEmpty(Nazwisko) && Wiek >= 0 && Waga >= 0);
            }
        }

        private ICommand dodaj;
        public ICommand Dodaj
        {
            get
            {
                if (dodaj is null)
                {
                    dodaj = new RelayCommand(
                        arg =>
                        {
                            Pilkarz pilkarz = new Pilkarz(Imie, Nazwisko, (uint)Wiek, (uint)Waga);

                            if (!Listapilkarzy.Contains(pilkarz))
                            {
                                Listapilkarzy.Add(pilkarz);
                                onPropertyChanged(nameof(Listapilkarzy));
                                
                                Czysc();
                            }
                        }
                        , arg => niepuste
                    );
                }
                return dodaj;
            }
        }


        private ICommand usun;

        public ICommand Usun
        {
            get
            {
                if (usun == null)
                {
                    usun = new RelayCommand(
                    arg =>
                    {
                     
                        Pilkarz pilkarz = new Pilkarz(Imie, Nazwisko, (uint)Wiek, (uint)Waga);
                        if (Listapilkarzy.Contains(pilkarz))
                        {
                            Listapilkarzy.Remove(pilkarz);

                            onPropertyChanged(nameof(Listapilkarzy));

                           
                            Czysc();
                        }
                    }, arg => niepuste && Wybranypilkarz != null);
                }
                return usun;
            }
        }


        private ICommand modyfikuj;
        public ICommand Modyfikuj
        {
            get
            {
                if (modyfikuj == null)
                {
                    modyfikuj = new RelayCommand(
                    arg =>
                    {
                        Pilkarz pilkarz = new Pilkarz(Imie, Nazwisko, (uint)Wiek, (uint)Waga);

                        if (Listapilkarzy.Contains(Wybranypilkarz))
                        {
                            var id = Listapilkarzy.IndexOf(wybranypilkarz);

                            Listapilkarzy[id].P1(pilkarz);
                            Listapilkarzy.ResetItem(id);


                        }
                    }, arg => niepuste && Wybranypilkarz != null);
                }
                return modyfikuj;
            }
        }


        public string sciezka = "Pilkarze.json";

        private ICommand wczytajzpliku;
        public ICommand Wczytajzpliku
        {
            get
            {
                if (wczytajzpliku == null)
                {
                    wczytajzpliku = new RelayCommand(
                    arg =>
                    {
                        var druzynapilkarzy = File.ReadAllText(sciezka);

                        Listapilkarzy = JsonSerializer.Deserialize<BindingList<Pilkarz>>(druzynapilkarzy);


                        onPropertyChanged(nameof(wczytajzpliku));
                       
                    },
                    arg => File.Exists(sciezka) && (new FileInfo(sciezka).Length > 0));
                }
                return wczytajzpliku;
            }
        }


        private ICommand zapiszdopliku;
        public ICommand Zapiszdopliku
        {
            get
            {
                if (zapiszdopliku == null)
                {
                    zapiszdopliku = new RelayCommand(
                    arg =>
                    {
                        var druzynapilkarzy = JsonSerializer.Serialize(Listapilkarzy);

                        File.WriteAllText(sciezka, druzynapilkarzy);


                        onPropertyChanged(nameof(Zapiszdopliku));

                    }, arg => true);
                }
                return zapiszdopliku;
            }
        }
             
        private ICommand osoba;
        public ICommand Osoba
        {
            get
            {
                if (osoba == null)
                {
                    osoba = new RelayCommand(
                        arg =>
                        {
                            Imie = Wybranypilkarz.Imie;   
                            
                            Nazwisko = Wybranypilkarz.Nazwisko;

                            Wiek = Wybranypilkarz.Wiek;

                            Waga = Wybranypilkarz.Waga;

                        }, arg => Wybranypilkarz != null
                    );
                }
                return osoba;
            }
        }                 
    }
}
