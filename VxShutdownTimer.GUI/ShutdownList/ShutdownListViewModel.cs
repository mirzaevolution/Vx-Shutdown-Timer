using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.IO;
using CoreLib.Models;
using CoreLib.WindowsService;
using System.Collections.ObjectModel;

namespace VxShutdownTimer.GUI.ShutdownList
{
    public class ShutdownListViewModel:BindableBase
    {
        private ShutdownModelEx _selectedModel;
        private ObservableCollection<ShutdownModelEx> _shutdownModelCollection;
        private bool _addMode, _editMode, _removeMode, _finishLoading, _canEditFields = false, _canUseDatagrid = true;

        public ShutdownModelEx SelectedModel
        {
            get
            {
                return _selectedModel;
            }
            set
            {
                OnPropertyChanged(ref _selectedModel, value, nameof(SelectedModel));
            }
        }
        public ObservableCollection<ShutdownModelEx> ShutdownModelCollection
        {
            get
            {
                return _shutdownModelCollection;
            }
            set
            {
                OnPropertyChanged(ref _shutdownModelCollection, value, nameof(ShutdownModelCollection));
            }
        }
        public bool AddMode
        {
            get
            {
                return _addMode;
            }
            set
            {
                OnPropertyChanged(ref _addMode, value, nameof(AddMode));
            }
        }
        public bool EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                OnPropertyChanged(ref _editMode, value, nameof(_editMode));
            }
        }
        public bool RemoveMode
        {
            get
            {
                return _removeMode;
            }
            set
            {
                OnPropertyChanged(ref _removeMode, value, nameof(RemoveMode));
            }
        }
        public bool CanEditFields
        {
            get
            {
                return _canEditFields;
            }
            set
            {
                OnPropertyChanged(ref _canEditFields, value, nameof(CanEditFields));
            }
        }
        public bool CanUseDatagrid
        {
            get
            {
                return _canUseDatagrid;
            }
            set
            {
                OnPropertyChanged(ref _canUseDatagrid, value, nameof(CanUseDatagrid));
            }
        }
        public bool FinishLoading
        {
            get
            {
                return _finishLoading;
            }
            set
            {
                OnPropertyChanged(ref _finishLoading, value, nameof(FinishLoading));
            }
        }
        public RelayCommand AddCommand { get; private set; } 
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; } 
        public RelayCommand SaveCommand { get; private set; } 
        public RelayCommand CancelCommand { get; private set; } 
        public override event EventHandler<string> Error;
        public override event EventHandler<string> Info;
        public ShutdownListViewModel()
        {
            InitializeComponent();
        }
        public async void Load()
        {
            bool success = true;
            string error = "";
            FinishLoading = false;
            await Task.Run(() =>
            {
                try
                {
                    var result = IOSerializeDeserialize.Deserialize();
                    if(result.Status.Success)
                    {
                        if(result.Data.Count>0)
                        {
                            var list = result.Data.Select(x => ShutdownModelConverter.ConvertTo(x));
                            ShutdownModelCollection = new ObservableCollection<ShutdownModelEx>(list);
                        }
                        else
                        {
                            ShutdownModelCollection = new ObservableCollection<ShutdownModelEx>();
                        }
                    }
                    else
                    {
                        success = false;
                        error = result.Status.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    error = $"An error occured. Message: {ex.Message}";
                }
            });
            //to avoid cross thread exception
            if (success)
            {
                OnInfoRequested("Data Loaded Successfully");
                FinishLoading = true;
            }
            else
                OnErrorOccured(error);
        }

        private void InitializeComponent()
        {
            ShutdownModelCollection = new ObservableCollection<ShutdownModelEx>();
            AddCommand = new RelayCommand(OnAdd, CanAdd);
            EditCommand = new RelayCommand(OnEdit, CanEdit);
            RemoveCommand = new RelayCommand(OnRemove, CanRemove);
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel, CanCancel);

        }

        private async void AddEditRemoveOperation(string mode)
        {
            try
            {
                bool success = true;
                string error = "";
                OnInfoRequested("Loading...");

                await Task.Run(() =>
                {
                    try
                    {
                        List<ShutdownModel> list;
                        if (ShutdownModelCollection.Count > 0)
                            list = ShutdownModelCollection.Select(x => ShutdownModelConverter.ConvertFrom(x)).ToList();
                        else
                            list = new List<ShutdownModel>();
                        var result = IOSerializeDeserialize.Serialize(list);
                        if (!result.Success)
                        {
                            success = false;
                            error = result.ErrorMessage;
                        }
                        //else
                        //{
                        //    result = Controller.RefreshService();
                        //    if (!result.Success)
                        //    {
                        //        success = false;
                        //        error = $"Data {mode} successfully. But, service generated error.\n{result.ErrorMessage}";
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        error = $"An error occured. Message: {ex.Message}";

                    }
                });
                if (success)
                {
                    OnInfoRequested($"Data {mode} successfully");
                    if(mode=="edited")
                    {
                        SelectedModel.EndEdit();
                    }
                }
                else
                {
                    OnErrorOccured(error);
                    if (mode == "edited")
                    {
                        SelectedModel.CancelEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccured(ex.Message);
            }
        }

        private void OnAdd()
        {
            AddMode = true;
            CanEditFields = true;
            CanUseDatagrid = false;
            SelectedModel = new ShutdownModelEx
            {
                DateTime = DateTime.Now,
                IsChecked = false,
                Repetition = Repetition.None,
                ShutdownType = ShutdownType.Shutdown
            };
            ShutdownModelCollection.Add(SelectedModel);
        }

        private bool CanAdd()
        {
            if (!_addMode && !_editMode && !_removeMode && FinishLoading)
                return true;
            return false;
        }

        private void OnEdit()
        {
            EditMode = true;
            CanEditFields = true;
            CanUseDatagrid = false;
            SelectedModel.BeginEdit();
        }

        private bool CanEdit()
        {
            if ((SelectedModel == null) || (_addMode || _editMode || _removeMode))
                return false;
            return true;
        }

        private void OnRemove()
        {
            RemoveMode = true;
            CanEditFields = false;
            CanUseDatagrid = true;
        }

        private bool CanRemove()
        {
            if ((ShutdownModelCollection.Count == 0) || (_addMode || _editMode || _removeMode))
                return false;
            return true;
        }

        private void OnSave()
        {
            if(AddMode)
            {
                
                AddEditRemoveOperation("added");
                AddMode = false;
            }
            else if(EditMode)
            {
                AddEditRemoveOperation("edited");
                EditMode = false;
            }
            else if(RemoveMode)
            {
                //to avoid Invalid Operation Exception
                for (int i = 0; i < ShutdownModelCollection.Count; i++)
                {
                    if (ShutdownModelCollection[i].IsChecked)
                    {
                        ShutdownModelCollection.RemoveAt(i);
                        i--;
                    }

                }
                AddEditRemoveOperation("removed");
                RemoveMode = false;
            }
            CanUseDatagrid = true;
            CanEditFields = false;
        }

        private bool CanSave()
        {
            if (RemoveMode)
            {
                if (ShutdownModelCollection.Any(x => x.IsChecked))
                    return true;
                return false;
            }
            return (AddMode || EditMode);
        }

        private void OnCancel()
        {
            if (AddMode)
            {
                ShutdownModelCollection.Remove(SelectedModel);
                SelectedModel = null;
                AddMode = false;
            }
            else if (EditMode)
            {
                EditMode = false;
                SelectedModel.CancelEdit();
            }
            else if (RemoveMode)
            {
                foreach (var item in ShutdownModelCollection)
                {
                    if (item.IsChecked)
                        item.IsChecked = false;
                }
                RemoveMode = false;
            }
            CanUseDatagrid = true;
            CanEditFields = false;
        }

        private bool CanCancel()
        {
            return (AddMode || EditMode || RemoveMode);
        }

        protected virtual void OnInfoRequested(string e)
        {
            Info?.Invoke(this, e);
        }
        protected virtual void OnErrorOccured(string e)
        {
            Error?.Invoke(this, e);
        }
    }
}
