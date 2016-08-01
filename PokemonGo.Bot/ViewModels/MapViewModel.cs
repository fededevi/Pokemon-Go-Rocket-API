﻿using AllEnum;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.GeneratedCode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.Bot.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        readonly Client client;

        public AsyncRelayCommand GetMapObjects { get; }

        public ObservableCollection<FortData> Pokestops { get; } = new ObservableCollection<FortData>();
        public ObservableCollection<WildPokemon> WildPokemon { get; } = new ObservableCollection<WildPokemon>();

        public MapViewModel(Client client)
        {
            this.client = client;
            GetMapObjects = new AsyncRelayCommand(async () =>
            {
                var mapResponse = await client.GetMapObjects();
                Pokestops.UpdateWith(mapResponse.MapCells.SelectMany(m => m.Forts).Where(f => f.Type == FortType.Checkpoint));
                WildPokemon.UpdateWith(mapResponse.MapCells.SelectMany(m => m.WildPokemons));
            });
        }
    }
}
