﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by BitmaskPropertyGenerator on 4/6/2021 1:34:14 AM.
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SniffExplorer.UI.Controls.Models
{
    public partial class RaceFilters : System.ComponentModel.INotifyPropertyChanged
    {
        
        public bool Human
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Human);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Human) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Human;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Human;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Human)));
            }
        }
        
        public bool Orc
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Orc);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Orc) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Orc;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Orc;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Orc)));
            }
        }
        
        public bool Dwarf
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Dwarf);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Dwarf) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Dwarf;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Dwarf;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dwarf)));
            }
        }
        
        public bool NightElf
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.NightElf);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.NightElf) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.NightElf;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.NightElf;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NightElf)));
            }
        }
        
        public bool Undead
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Undead);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Undead) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Undead;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Undead;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Undead)));
            }
        }
        
        public bool Tauren
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Tauren);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Tauren) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Tauren;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Tauren;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tauren)));
            }
        }
        
        public bool Gnome
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Gnome);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Gnome) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Gnome;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Gnome;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Gnome)));
            }
        }
        
        public bool Troll
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Troll);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Troll) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Troll;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Troll;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Troll)));
            }
        }
        
        public bool Goblin
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Goblin);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Goblin) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Goblin;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Goblin;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Goblin)));
            }
        }
        
        public bool BloodElf
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.BloodElf);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.BloodElf) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.BloodElf;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.BloodElf;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BloodElf)));
            }
        }
        
        public bool Draenei
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Draenei);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Draenei) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Draenei;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Draenei;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Draenei)));
            }
        }
        
        public bool FelOrc
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.FelOrc);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.FelOrc) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.FelOrc;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.FelOrc;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FelOrc)));
            }
        }
        
        public bool Naga
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Naga);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Naga) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Naga;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Naga;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Naga)));
            }
        }
        
        public bool Broken
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Broken);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Broken) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Broken;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Broken;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Broken)));
            }
        }
        
        public bool Skeleton
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Skeleton);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Skeleton) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Skeleton;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Skeleton;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Skeleton)));
            }
        }
        
        public bool Vrykul
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Vrykul);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Vrykul) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Vrykul;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Vrykul;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vrykul)));
            }
        }
        
        public bool Tuskarr
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Tuskarr);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Tuskarr) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Tuskarr;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Tuskarr;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tuskarr)));
            }
        }
        
        public bool ForestTroll
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.ForestTroll);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.ForestTroll) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.ForestTroll;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.ForestTroll;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForestTroll)));
            }
        }
        
        public bool Taunka
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Taunka);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Taunka) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Taunka;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Taunka;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Taunka)));
            }
        }
        
        public bool NorthrendSkeleton
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.NorthrendSkeleton);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.NorthrendSkeleton) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.NorthrendSkeleton;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.NorthrendSkeleton;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NorthrendSkeleton)));
            }
        }
        
        public bool IceTroll
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.IceTroll);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.IceTroll) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.IceTroll;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.IceTroll;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IceTroll)));
            }
        }
        
        public bool Worgen
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Worgen);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Worgen) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Worgen;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Worgen;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Worgen)));
            }
        }
        
        public bool Gilnean
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Gilnean);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Gilnean) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Gilnean;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Gilnean;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Gilnean)));
            }
        }
        
        public bool PandarenNeutral
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenNeutral);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenNeutral) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenNeutral;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenNeutral;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PandarenNeutral)));
            }
        }
        
        public bool PandarenAlliance
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenAlliance);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenAlliance) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenAlliance;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenAlliance;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PandarenAlliance)));
            }
        }
        
        public bool PandarenHorde
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenHorde);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenHorde) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenHorde;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.PandarenHorde;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PandarenHorde)));
            }
        }
        
        public bool Nightborne
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Nightborne);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Nightborne) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Nightborne;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Nightborne;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nightborne)));
            }
        }
        
        public bool HighmountainTauren
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.HighmountainTauren);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.HighmountainTauren) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.HighmountainTauren;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.HighmountainTauren;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HighmountainTauren)));
            }
        }
        
        public bool VoidElf
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.VoidElf);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.VoidElf) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.VoidElf;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.VoidElf;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VoidElf)));
            }
        }
        
        public bool LightforgedDraenei
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.LightforgedDraenei);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.LightforgedDraenei) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.LightforgedDraenei;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.LightforgedDraenei;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LightforgedDraenei)));
            }
        }
        
        public bool ZandalariTroll
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.ZandalariTroll);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.ZandalariTroll) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.ZandalariTroll;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.ZandalariTroll;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ZandalariTroll)));
            }
        }
        
        public bool KulTiran
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.KulTiran);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.KulTiran) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.KulTiran;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.KulTiran;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KulTiran)));
            }
        }
        
        public bool ThinHuman
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.ThinHuman);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.ThinHuman) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.ThinHuman;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.ThinHuman;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThinHuman)));
            }
        }
        
        public bool DarkIronDwarf
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.DarkIronDwarf);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.DarkIronDwarf) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.DarkIronDwarf;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.DarkIronDwarf;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DarkIronDwarf)));
            }
        }
        
        public bool Vulpera
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Vulpera);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Vulpera) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Vulpera;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Vulpera;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vulpera)));
            }
        }
        
        public bool MagharOrc
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.MagharOrc);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.MagharOrc) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.MagharOrc;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.MagharOrc;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MagharOrc)));
            }
        }
        
        public bool Mechagnome
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Mechagnome);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.RaceMask.Mechagnome) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.RaceMask.Mechagnome;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.RaceMask.Mechagnome;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mechagnome)));
            }
        }
        

        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
    }
}
