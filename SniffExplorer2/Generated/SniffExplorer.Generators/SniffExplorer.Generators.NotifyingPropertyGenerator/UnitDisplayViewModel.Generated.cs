﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by NotifyingPropertyGenerator on 4/11/2021 8:23:47 PM.
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SniffExplorer.UI.Controls.Models
{
    public partial class UnitDisplayViewModel : SniffExplorer.UI.Controls.Models.EntityViewModel<SniffExplorer.UI.Controls.Models.DisplayCreature, SniffExplorer.Parsing.Engine.Tracking.Entities.Creature, SniffExplorer.UI.Controls.Models.UnitDisplayViewModel>, System.ComponentModel.INotifyPropertyChanged
    {
        
        public string EntryFilter
        {
            get => _entryFilter;
            set {
                if (EqualityComparer<string>.Default.Equals(_entryFilter, value))
                    return;

                BeforeEntryFilterChange(_entryFilter, value);
                _entryFilter = value;
                AfterEntryFilterChange();
                NotifyPropertyChanged(nameof(EntryFilter));
            }
        }

        partial void BeforeEntryFilterChange(string oldValue, string newValue);
        partial void AfterEntryFilterChange();
        

        

        
    }
}
