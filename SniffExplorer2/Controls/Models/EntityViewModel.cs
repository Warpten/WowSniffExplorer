using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.UI;
using SniffExplorer.UI.Commands;

namespace SniffExplorer.UI.Controls.Models
{
    public abstract class EntityDisplay<T> where T : IEntity
    {
        public T Entity { get; init; }

        public IObjectGUID Guid => Entity.Guid;
        
        public ICommand ShowMovement { get; } = new RelayCommand<DisplayCreature>(c => c.HandleShowMovement());
        public ICommand ShowSpells { get; } = new RelayCommand<DisplayCreature>(c => c.HandleShowSpells());

        protected virtual void HandleShowMovement() { }
        protected virtual void HandleShowSpells() { }
    }

    public abstract partial class EntityViewModel<TDisplay, TEntity, U> : INotifyPropertyChanged
        where U : EntityViewModel<TDisplay, TEntity, U>
        where TDisplay : EntityDisplay<TEntity>, new()
        where TEntity : IEntity
    {
        [NotifyingProperty(PropertyName = "NameFilter")]
        protected string _nameFilter;

        private readonly Func<TEntity, U, bool> _filter;

        private ObservableCollection<TDisplay> _entities;
        
        public IEnumerable<TEntity> Entities
        {
            set
            {
                _entities = new ObservableCollection<TDisplay>(value.Select(value => new TDisplay {Entity = value}));
                EntitiesView = CollectionViewSource.GetDefaultView(_entities);
                EntitiesView.Filter += element =>
                {
                    if (element is not TDisplay entity)
                        return false;

                    return _filter(entity.Entity, (U)this);
                };
            }
        }

        [NotifyingProperty(PropertyName = "EntitiesView")]
        private ICollectionView _entitiesView;

        public event PropertyChangedEventHandler PropertyChanged;

        protected EntityViewModel(Func<TEntity, U, bool> filter)
        {
            _filter = filter;
        }

        partial void AfterNameFilterChange()
        {
            RefreshView();
        }

        protected void RefreshView()
        {
            if (_entitiesView == null)
                return;

            _entitiesView.Refresh();
            NotifyPropertyChanged(nameof(Entities));
        }
    }
}