using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.map.TacticalMovement.FightScene;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace rdgwatp_Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FightPage : ContentPage
    {
        public CreatureBuilder cb { get; set; }
        private string Emotion; 
        public string EmotionsView
        {
            get
            {
                return Emotion;
            }
            set
            { 
                Emotion = value;
                OnPropertyChanged(nameof(EmotionsView));
            }
        }
        public FightPage()
        {
            Title = "Fight Page";
            InitializeComponent();

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
            EmotionsView = "gsdgsgsdgdsggds";
            BindingContext = this;
        }
        void _OnCBPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "emotions")
               EmotionsView = ((CreatureBuilder)sender).getEmotions(((CreatureBuilder)sender).getlastEmotionReproduced(),false)[((CreatureBuilder)sender).getlastEmotionReproduced()];
            
        }
        async void AttackButton_Clicked(object sender, EventArgs e)
        {
            fight.stepFight("ATTACK_MOVE");//Атака
            //EmotionsView = ""; //Обновление эмоций
        }
        void InventoryButton_Clicked(object sender, EventArgs e)
        {
            //fight.stepFight("ATTACK_MOVE");//Атака
           // EmotionsView = ""; //Обновление эмоций
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
        void EscapeButton_Clicked(object sender, EventArgs e)
        {
            fight.stepFight("RUN_AWAY");//Атака
            //EmotionsView = ""; //Обновление эмоций
        }
    }
}