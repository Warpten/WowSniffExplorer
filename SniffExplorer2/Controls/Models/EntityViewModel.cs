using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.UI.Controls.Models
{
    public abstract partial class EntityViewModel<T, U> : INotifyPropertyChanged
        where U : EntityViewModel<T, U>
    {
        [NotifyingProperty(PropertyName = "NameFilter")]
        protected string _nameFilter;

        private readonly Func<T, U, bool> _filter;

        private ObservableCollection<T> _entities;
        public ObservableCollection<T> Entities
        {
            get => _entities;
            set
            {
                _entities = value;
                EntitiesView = CollectionViewSource.GetDefaultView(_entities);
                EntitiesView.Filter += element =>
                {
                    if (element is not T entity)
                        return false;

                    return _filter(entity, (U) this);
                };
            }
        }

        [NotifyingProperty(PropertyName = "EntitiesView")]
        private ICollectionView _entitiesView;

        public event PropertyChangedEventHandler PropertyChanged;

        protected EntityViewModel(Func<T, U, bool> filter)
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