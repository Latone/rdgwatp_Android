using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.map.TacticalMovement.FightScene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ConsoleApp1.src.map;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;

namespace rdgwatp_Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FightPage : ContentPage
    {
        public CreatureBuilder cb { get; set; }
        //Для апдейта эмоций
        private string Emotion; 
        public string EmotionsView
        {
            get
            {
                updateBars();
                return Emotion;
            }
            set
            { 
                Emotion = value;
                OnPropertyChanged(nameof(EmotionsView));
            }
        }
        //Полоска хп для врага
        private double en_hbar;
        public double en_pbar
        {
            get
            {
                return en_hbar;
            }
            set
            {
                en_hbar = value;
                OnPropertyChanged(nameof(en_pbar));
            }
        }
        private string en_numhp;
        public string en_Vnumhp
        {
            get
            {
                return en_numhp;
            }
            set
            {
                en_numhp = value;
                OnPropertyChanged(nameof(en_Vnumhp));
            }
        }
        //Полоска брони для врага
        private double en_EDVnumarmor;
        public double en_Vnumarmor
        {
            get
            {
                return en_EDVnumarmor;
            }
            set
            {
                en_EDVnumarmor = value;
                OnPropertyChanged(nameof(en_Vnumarmor));
            }
        }
        private double en_EDarmorbar;
        public double en_armorbar
        {
            get
            {
                return en_EDarmorbar;
            }
            set
            {
                en_EDarmorbar = value;
                OnPropertyChanged(nameof(en_armorbar));
            }
        }
        //Полоска хп для игрока
        private string h_numhp;
        public string h_Vnumhp
        {
            get
            {
                return h_numhp;
            }
            set
            {
                h_numhp = value;
                OnPropertyChanged(nameof(h_Vnumhp));
            }
        }
        private double h_hbar;
        public double h_pbar
        {
            get
            {
                return h_hbar;
            }
            set
            {
                h_hbar = value;
                OnPropertyChanged(nameof(h_pbar));
            }
        }
        //Полоска брони для игрока
        private double h_EDVnumarmor;
        public double h_Vnumarmor
        {
            get
            {
                return h_EDVnumarmor;
            }
            set
            {
                h_EDVnumarmor = value;
                OnPropertyChanged(nameof(h_Vnumarmor));
            }
        }
        private double h_EDarmorbar;
        public double h_armorbar
        {
            get
            {
                return h_EDarmorbar;
            }
            set
            {
                h_EDarmorbar = value;
                OnPropertyChanged(nameof(h_armorbar));
            }
        }
        public FightPage()
        {
            Title = "Fight Page";
            InitializeComponent();
            //Ждём инициализации существа в классе fight посторонним потоком
            var t = Task.Run(async delegate
            {
                while (fight.cb == null)
                {
                    Console.WriteLine("Waiting for an alternative thread to set value...");
                    await Task.Delay(25);
                }
            });
            cb = fight.cb;

            cb.PropertyChanged += _OnCBPropertyChanged;
            cb.PropertyChanged += OnCBPropertyChangedHP;
            EmotionsView = cb.getEmotions(0, true)[0];
            updateBars();

            BindingContext = this;
        }
        void updateBars()
        {
            //hp
            en_pbar = ((double)cb.getHP() / (double)cb.getMaxHP());
            h_pbar = ((double)map.getPlayer().getHP() / (double)map.getPlayer().getMaxHP());
            en_Vnumhp = cb.getHP().ToString() + " /" + cb.getMaxHP().ToString();
            h_Vnumhp = map.getPlayer().getHP().ToString() + " /" + map.getPlayer().getMaxHP().ToString();
            //armor
            en_Vnumarmor = cb.getARMOR();
            en_armorbar = cb.getARMOR();
            h_armorbar = map.getPlayer().getARMOR();
            h_Vnumarmor = map.getPlayer().getARMOR();
        }
        void _OnCBPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "emotions")
               EmotionsView = ((CreatureBuilder)sender).getEmotions(((CreatureBuilder)sender).getlastEmotionReproduced(),false)[((CreatureBuilder)sender).getlastEmotionReproduced()];
        }
        //Атака
        void AttackButton_Clicked(object sender, EventArgs e)
        {
            if (fight.cb.getIsDead() == false)
            {
                Thread th = new Thread(x => fight.stepFight("ATTACK_MOVE"));//Атака
                EnemyMoves.threads.Add(th);
                th.IsBackground = true;
                th.Start();
            }
            //EmotionsView = ""; //Обновление эмоций
        }
        async void InventoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage(), true);
        }
        protected override void OnAppearing()
        {
                updateBars();
        }
        void AbilitiesButton_Clicked(object sender, EventArgs e)
        {
            //fight.stepFight("ATTACK_MOVE");//Атака
            //EmotionsView = ""; //Обновление эмоций
        }
        void ConvinceButton_Clicked(object sender, EventArgs e)
        {
            //fight.stepFight("ATTACK_MOVE");//Атака
            //EmotionsView = ""; //Обновление эмоций
        }
        //Бег
        async void EscapeButton_Clicked(object sender, EventArgs e)
        {
            Thread th = new Thread(x => fight.stepFight("RUN_AWAY"));//Бег
            th.Start();
            Thread.Sleep(200);
            if(fight.cb.getDisableTime() > 0)
                await Navigation.PopAsync();
        }
        //Выход на карту (Инвоук, потому что запрос идёт со внешнего треда. Таким боком форсим главный поток выйти)
        void OnCBPropertyChangedHP(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "got less than 1 hp") 
               Device.BeginInvokeOnMainThread(() => {
                   foreach (var thread in EnemyMoves.threads)
                       thread.Join();
                   Navigation.PopAsync(); 
               });
        }
    }
}