using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.Controls.Models
{
    public abstract partial class EntityModel<T, U, V> : INotifyPropertyChanged
        where T : EntityDisplay<U>, new()
        where U : IEntity
        where V : EntityModel<T, U, V>
    {
        [NotifyingProperty(PropertyName = "NameFilter", AfterCallback = nameof(RefreshEntities))]
        private string? _nameFilter;

        private readonly Func<U, V, bool> _displayFilter;

        protected EntityModel(Func<U, V, bool> filter)
        {
            _displayFilter = filter;
        }

        private ObservableCollection<T>? _objects;

        public IEnumerable<U> Objects
        {
            set
            {
                _objects = new ObservableCollection<T>(value.Select(value => new T {Object = value}));
                _entities = CollectionViewSource.GetDefaultView(_objects);
                _entities.Filter += element =>
                {
                    if (element is not T entity)
                        return false;

                    return _displayFilter(entity.Object, (V) this);
                };

                NotifyPropertyChanged("Entities");
            }
        }

        [NotifyingProperty(PropertyName = "Entities", AfterCallback = nameof(RefreshEntities))]
        private ICollectionView? _entities;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new(propertyName));
        
        protected void RefreshEntities()
        {
            if (_entities == null)
                return;

            _entities.Refresh();
            NotifyPropertyChanged("Entities");
        }
    }
}