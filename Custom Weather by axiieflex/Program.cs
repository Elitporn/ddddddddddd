using System;
using System.Collections.Generic;
using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;

namespace axiieflex.ensage2.CustomWeather
{
    class Program
    {
        private static bool _initialized = false;
        private static readonly Menu Menu = new Menu("Pogoda", "Pogoda", true);

        // http://dota2.gamepedia.com/Weather
        // Weather Default 0
        // Weather Ash 8
        // Weather Aurora 9
        // Weather Harvest 5
        // Weather Moonbeam 3
        // Weather Pestilence 4
        // Weather Rain 2
        // Weather Sirocco 6
        // Weather Snow 1
        // Weather Spring 7
        private enum Weather {
            Standart         = 0,
            Sneg            = 1,
            Rain           = 2,
            Moonbeam        = 3,
            Pestilence      = 4,
            Harvest         = 5,
            Sirocco         = 6,
            Spring          = 7,
            Ash             = 8,
            Aurora          = 9
        }

        static void Main(string[] args)
        {

            MenuItem item;

            item = new MenuItem("Pogoda", "Selected").SetValue(new StringList(new[] { 
                        Weather.Default.ToString(), 
                        Weather.Snow.ToString(),
                        Weather.Rain.ToString(),
                        Weather.Moonbeam.ToString(),
                        Weather.Pestilence.ToString(),
                        Weather.Harvest.ToString(),
                        Weather.Sirocco.ToString(),
                        Weather.Spring.ToString(),
                        Weather.Ash.ToString(),
                        Weather.Aurora.ToString()
            }, 0));

            item.ValueChanged += Item_ValueChanged;
            Menu.AddItem(item);

            Menu.AddToMainMenu();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Item_ValueChanged(object sender, OnValueChangeEventArgs e)
        {
            var t = e.GetNewValue<StringList>().SelectedIndex;
            if (!Game.IsInGame) return;
            var var = Game.GetConsoleVar("cl_weather");
            var.RemoveFlags(ConVarFlags.Cheat);
            var.SetValue(t);
        }

        private static void Game_OnUpdate(EventArgs args) {
            if (!Game.IsInGame)
            {
                _initialized = false;
                return;
            }
            
            if (_initialized) {
                return;
            }
            
            _initialized = true;
            var t = Menu.Item("Pogoda").GetValue<StringList>().SelectedIndex;
            var var = Game.GetConsoleVar("cl_weather");
            var.RemoveFlags(ConVarFlags.Cheat);
            var.SetValue(t);
        }
    }
}
