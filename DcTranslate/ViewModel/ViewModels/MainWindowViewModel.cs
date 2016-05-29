using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using DcTranslate.Model;
using DcTranslate.View.Helpers;
using DcTranslate.View.Windows;
using DcTranslate.ViewModel.BaseClasses;
using DcTranslate.ViewModel.Helpers;

namespace DcTranslate.ViewModel.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        private readonly INumberTranslationRepository _repository;

        public MainWindowViewModel()
        {
            _repository = new NumberTranslationRepositorySqliteDemo();
            RowsPerPage = _repository.PageSize;
            UpdateListOfNumberTranslations();
            UpdatePosiblePages();
            EditNumberTranslationCommand = new DelegateCommand<ViewFunctions>(EditNumberTranslation);
            AddNewNumberTranslationCommand = new DelegateCommand<ViewFunctions>(AddNewNumberTranslation);
        }

        private void EditNumberTranslation(ViewFunctions viewFunctions)
        {
            if (SelectedNumberTranslation == null) return;

            if (viewFunctions.EditNumberTranslation(SelectedNumberTranslation) == false) return;

            try
            {
                _repository.Update(SelectedNumberTranslation);
            }
            catch (Exception exception)
            {
                viewFunctions.DisplayMessage(exception.Message);
                return;
            }
            UpdateListOfNumberTranslations(force: true);
        }

        private void AddNewNumberTranslation(ViewFunctions viewFunctions)
        {
            var numberTranslation = viewFunctions.AddNumberTranslation();

            if (numberTranslation == null) return;

            try
            {
                _repository.Add(numberTranslation);
            }
            catch (Exception exception)
            {
                viewFunctions.DisplayMessage(exception.Message);
                return;
            }

            UpdateListOfNumberTranslations(force: true);
        }

        public ObservableCollection<NumberTranslation> NumberTranslations
        {
            get { return GetField<ObservableCollection<NumberTranslation>>(); }
            set { SetField(value); }
        }

        public long RowsPerPage
        {
            get
            {
                return GetField<long>();
            }
            set
            {
                SetField(value);
                _repository.PageSize = value;
                
                UpdateListOfNumberTranslations();
                UpdatePosiblePages();
            }
        }

        public long Page
        {
            get
            {
                return GetField<long>();
            }
            set
            {
                SetField(value);
                UpdateListOfNumberTranslations();
            }
        }

        public List<long> PosiblePages { get { return GetField<List<long>>(); } set { SetField(value); } }
        public NumberTranslation SelectedNumberTranslation { get { return GetField<NumberTranslation>(); } set { SetField(value); } }

        public string Search
        {
            get
            {
                return GetField<string>();
            }
            set
            {
                SetField(value);
                UpdateListOfNumberTranslations();
                UpdatePosiblePages();
            }
        }

        private void UpdatePosiblePages()
        {
            var listOfPosiblePages = new List<long>();
            for (int i = 0; i < _repository.LastQueryWouldHaveReturnedThisAmountOfPages + 1; i++)
                listOfPosiblePages.Add(i);
            PosiblePages = listOfPosiblePages;
            Page = PosiblePages[0];
        }

        private void UpdateListOfNumberTranslations(bool force = false)
        {
            if (force || LastRepositoryCallWasDifferentThanThis())
                NumberTranslations = new ObservableCollection<NumberTranslation>(_repository.GetNumberTranslations(Search, Page));
            _lastCall = new Tuple<string, long, long>(Search, Page, RowsPerPage);
        }

        private Tuple<string, long, long> _lastCall;
        private bool LastRepositoryCallWasDifferentThanThis()
        {
            if (_lastCall == null) return true;
            if (_lastCall.Item1 != Search) return true;
            if (_lastCall.Item2 != Page) return true;
            if (_lastCall.Item3 != RowsPerPage) return true;
            return false;
        }

        public DelegateCommand<ViewFunctions> EditNumberTranslationCommand { get; }
        public DelegateCommand<ViewFunctions> AddNewNumberTranslationCommand { get; } 
    }
}
