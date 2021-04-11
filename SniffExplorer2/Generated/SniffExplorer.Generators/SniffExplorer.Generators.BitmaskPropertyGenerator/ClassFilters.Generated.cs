﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by BitmaskPropertyGenerator on 4/12/2021 12:15:16 AM.
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SniffExplorer.UI.Controls
{
    public partial class ClassFilters : System.ComponentModel.INotifyPropertyChanged
    {
        
        public bool Warrior
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Warrior);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Warrior) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Warrior;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Warrior;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warrior)));
            }
        }
        
        public bool Paladin
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Paladin);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Paladin) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Paladin;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Paladin;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Paladin)));
            }
        }
        
        public bool Hunter
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Hunter);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Hunter) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Hunter;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Hunter;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hunter)));
            }
        }
        
        public bool Rogue
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Rogue);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Rogue) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Rogue;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Rogue;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rogue)));
            }
        }
        
        public bool Priest
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Priest);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Priest) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Priest;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Priest;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Priest)));
            }
        }
        
        public bool DeathKnight
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.DeathKnight);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.DeathKnight) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.DeathKnight;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.DeathKnight;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeathKnight)));
            }
        }
        
        public bool Shaman
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Shaman);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Shaman) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Shaman;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Shaman;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shaman)));
            }
        }
        
        public bool Mage
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Mage);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Mage) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Mage;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Mage;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mage)));
            }
        }
        
        public bool Warlock
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Warlock);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Warlock) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Warlock;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Warlock;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warlock)));
            }
        }
        
        public bool Monk
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Monk);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Monk) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Monk;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Monk;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Monk)));
            }
        }
        
        public bool Druid
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Druid);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.Druid) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.Druid;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.Druid;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Druid)));
            }
        }
        
        public bool DemonHunter
        {
            get => Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.DemonHunter);
            set {
                if (Value.HasFlag(SniffExplorer.Parsing.Types.Enums.ClassMask.DemonHunter) == value)
                    return;

                if (value)
                    Value |= SniffExplorer.Parsing.Types.Enums.ClassMask.DemonHunter;
                else
                    Value &= ~SniffExplorer.Parsing.Types.Enums.ClassMask.DemonHunter;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DemonHunter)));
            }
        }
        

        
    }
}
