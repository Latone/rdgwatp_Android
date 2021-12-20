using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.src.map;
using Xamarin.Forms;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using System.Collections.ObjectModel;
using ConsoleApp1.src.creatures.builders;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using rdgwatp_Android.src.map.Log;

namespace rdgwatp_Android
{
    public partial class MapPage: ContentPage
    {
        CreatureBuilder cb { get; set; }
        public MapPage()
        {
            Title = "Map Page";
            InitializeComponent();

            FastForwardChangeXMLObjects.OnLabelUpdate += new FastForwardChangeXMLObjects.UpdateLabel(updater_OnLabelUpdate);
            
            InventoryVis = false;
            UpVis = false;
            DownVis = false;
            RightVis = false;
            LeftVis = false;
            Logger.StaticPropertyChanged += OnLogChange;
            BindingContext = this;
        }
        private string wholeLog;
        void OnLogChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "upLog")
                Log = Logger.getLog().ToString();
        }
        public string Log
        {
            get {

                return wholeLog;
            }
            set{
                wholeLog = value;
                OnPropertyChanged(nameof(Log));
            }
        }
        void updater_OnLabelUpdate(string value)
        {
            MapView = value;
        }
        //Вперёд-Назад обращение к Label/Text для отображения карты
        public string MapView
        {
            get {
                string result = "";

                for (int i = 0; i < VisualCharacters.PlayerPerspectiveMV.Count;i++)//VisualCharacters.mapview.Count; i++)
                        result += VisualCharacters.PlayerPerspectiveMV[i];
                    result += "\n";
                
                return result;
            }
            set {
                OnPropertyChanged(nameof(MapView));
            }
        }

        //Вперёд-Назад обращение к свойству кнопки инвентаря (Visibility)
        private bool _isInvVis;
        public bool InventoryVis {
            get { return _isInvVis; }
            set {
                _isInvVis = value;
                OnPropertyChanged(nameof(InventoryVis));
            }
        }

        private bool _isUpVis;
        public bool UpVis
        {
            get { return _isUpVis; }
            set
            {
                _isUpVis = value;
                OnPropertyChanged(nameof(UpVis));
            }
        }

        private bool _isDownVis;
        public bool DownVis
        {
            get { return _isDownVis; }
            set
            {
                _isDownVis = value;
                OnPropertyChanged(nameof(DownVis));
            }
        }

        private bool _isLeftVis;
        public bool LeftVis
        {
            get { return _isLeftVis; }
            set
            {
                _isLeftVis = value;
                OnPropertyChanged(nameof(LeftVis));
            }
        }

        private bool _isRightVis;
        public bool RightVis
        {
            get { return _isRightVis; }
            set
            {
                _isRightVis = value;
                OnPropertyChanged(nameof(RightVis));
            }
        }

        //Загрузка карты
        void StartButton_Clicked(object sender, EventArgs e)
        {
            map.StartMap();

            //Подписка игрока на изменение состояния его isFighting-переменной
            cb = map.getPlayer();
            cb.PropertyChanged += OnCBPropertyChanged;

            //Переотображение
            ((Button)sender).IsEnabled = false;
            InventoryVis = true;
            UpVis = true;
            DownVis = true;
            RightVis = true;
            LeftVis = true;
        }
        
        void UPButton_Clicked(object sender, EventArgs e)
        {
            map.stepGame("UP");//Шаг вверх
            //th.Start();
            //MapView = ""; //Обновление карты
            //th.Abort();
            
        }
        void DOWNButton_Clicked(object sender, EventArgs e)
        {
            map.stepGame("DOWN");//Шаг вверх
            //th.Start();
            //MapView = ""; //Обновление карты
            //th.Abort();
        }
        void LEFTButton_Clicked(object sender, EventArgs e)
        {
            map.stepGame("LEFT");//Шаг вверх
            //th.Start();
            //MapView = ""; //Обновление карты
            //th.Abort();
        }
        void RIGHTButton_Clicked(object sender, EventArgs e)
        {
            map.stepGame("RIGHT");
            //Шаг вверх
                                  //th.Start();
                                  //MapView = ""; //Обновление карты
                                  //th.Abort();
        }
        //Выход в меню, сброс игры
        private void MainMenuButton_Clicked(object sender, EventArgs e)
        {
                map.Clear();
                InventoryVis = false;
            Device.BeginInvokeOnMainThread(() => {
                //foreach (var thread in EnemyMoves.threads)
                //    thread.Join();
                Navigation.PopAsync();
            });
        }
        //Заход в бой (ивент переключения параметра isFighting)
        async void OnCBPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (((CreatureBuilder)sender).getIsFighting())
                await Navigation.PushAsync(new FightPage(), true);
        }
        //Просмотр инвентаря
        private async void InventoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage(),true);
        }
    }
}