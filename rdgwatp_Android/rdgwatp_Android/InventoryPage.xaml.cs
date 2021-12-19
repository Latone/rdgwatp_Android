using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ConsoleApp1.src.map;
using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.items;
using System.Diagnostics;
using ConsoleApp1.src.items.Types;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace rdgwatp_Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public ObservableCollection<ItemType> Items { get; set; }
        public InventoryPage()
        {
            Title = "Inventory Page";
            InitializeComponent();
            CreatureBuilder cb = map.getPlayer();
            Items = new ObservableCollection<ItemType>( cb.getInventory());
            Items.CollectionChanged += OnCollectionChanged;

            BindingContext = this;
        }
        //Для изменения UI(CollectionView) с помощью ObservableCollection, через ивент NotifyCollectionChanged// 2 последних - связаны <->
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<ItemType> objSender = sender as ObservableCollection<ItemType>;
            
            List<ItemType> editedOrRemovedItems = new List<ItemType>();

            //При добавлении в коллекцию (/Проверка на null)
            if(e.NewItems?.Count>0)
            foreach (ItemType it in e.NewItems)
            {
                editedOrRemovedItems.Add(it);
            }

            //При удалении из коллекции (/Проверка на null)
            if (e.OldItems?.Count > 0)
            foreach (ItemType it in e.OldItems)
            {
                editedOrRemovedItems.Add(it);
            }

            //Действие, что пробудило ивент:
            NotifyCollectionChangedAction action = e.Action;
            Debug.WriteLine($"Item has changed. Action: {action}");
        }
        
        //При выборе всплывает окно с использованием (popUpMenu)
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e) //Item Selection
        {
            if (this.collectionINV.SelectedItem == null) //Обрабатывается при любом изменение выделения
                return;

                ItemType selectedItem = e.CurrentSelection[0] as ItemType;
            
            bool answer = await DisplayAlert($"Использовать\n{selectedItem.name}?",
                $"Тип: [{selectedItem.subType}]\nЭффективность: [{selectedItem.points}]\nОписание: {selectedItem.Lore}","Бахнем","Не");
            
            Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                CreatureBuilder cb = map.getPlayer();
                switch (selectedItem.subType)
                {
                    case SubType.ARMOUR:
                        cb.setARMOR((short)(cb.getARMOR() + selectedItem.points));
                        break;
                    case SubType.HP:
                        cb.setHP((short)(cb.getHP() + selectedItem.points));
                        if (cb.getHP() > cb.getMaxHP()) cb.setHP(cb.getMaxHP());
                        break;
                    //IncreaseStats - в конечном итоге повышает мощь критического удара
                    case SubType.IncreaseStats:
                        cb.setPower_Crit_Attack((short)(cb.getPower_Crit_Attack() + selectedItem.points));
                        break;
                    case SubType.DMG:
                        if (cb.getIsFighting()) //Если в драке => +урон к след. атаке
                        {
                            cb.setDMG((short)(cb.getDMG() + selectedItem.points));
                            //Зато тут увеличивается базовый урон игрока на 0.1 от значения предмета
                            cb.setDefaultDMG((short)(cb.getDefaultDMG() + selectedItem.points * 0.1));
                        }
                        else //Иначе бьёт себя .-.
                        {
                            cb.setHP((short)(cb.getHP() - Math.Abs( selectedItem.points)));
                            bool answer2 = await DisplayAlert($"..Эмм\n?","Вам норм?", "Ага", "Да");
                            Debug.WriteLine("Answer: " + answer2);
                            
                        }
                        break;
                }
                //Уборка использованных предметов из инвентаря
                Items.Remove(selectedItem);
                //Апдейт инвентаря игрока
                cb.setInventory(Items.ToList());
            }
            //Убрать выделение
            this.collectionINV.SelectedItem = null;

        }
        private async void BacktoGameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}