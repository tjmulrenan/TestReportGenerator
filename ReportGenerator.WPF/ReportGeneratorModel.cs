using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using static ReportGenerator.Services.EnumConverter;

namespace ReportGeneratorWPF
{
    public class ReportGeneratorModel : INotifyPropertyChanged
    { 
        private List<string> _availableStandards = new List<string>();
        private List<string> _availableMaterials = new List<string>();
        private List<string> _availableActing = new List<string>();
        private List<string> _availablePanels = new List<string>();
        private List<string> _availableGlazedInfilled = new List<string>();
        private List<string> _availableInsulated = new List<string>();
        private List<string> _availableLatched = new List<string>();
        private List<string> _availableShootbolts = new List<string>();

        private string _lhText = "LH Doorset";
        private Visibility _isRHVisible;

        private string _selectedStandard;
        private string _selectedLHMaterial;
        private string _selectedRHMaterial;
        private string _selectedLHActing;
        private string _selectedRHActing;
        private string _selectedLHPanels;
        private string _selectedRHPanels;
        private string _selectedLHGlazedInfilled;
        private string _selectedRHGlazedInfilled;
        private string _selectedLHInsulated;
        private string _selectedRHInsulated;
        private string _selectedLHLatched;
        private string _selectedRHLatched;
        private string _selectedLHShootbolts;
        private string _selectedRHShootbolts;

        private string _testNumber;
        private string _sponsorName;
        private string _address;

        private bool _isSampleReport;
        private bool _isIdentical;
        private bool _isLeftHandGlazed;
        private bool _isRightHandGlazed;
        private bool _isLeftHandOpeningTowardsHeatConditions;
        private bool _isRightHandOpeningTowardsHeatConditions;

        private readonly ICommand _createCommand;

        public ReportGeneratorModel()
        {
            SetDefaults();
        }

        public ReportGeneratorModel(ICommand createCommand) : this()
        {
            _createCommand = createCommand;
            LoadValues();
        }

        private void SetDefaults()
        {
            IsIdentical = true;
        }

        private void LoadValues()
        {
            foreach (var material in Enum.GetValues(typeof(SpecimenMaterialType)))
            {
                if ((SpecimenMaterialType)material == SpecimenMaterialType.Unknown)
                {
                    continue;
                }
                _availableMaterials.Add(material.ToString());
            }

            foreach (var standard in Enum.GetValues(typeof(SpecimenStandardType)))
            {
                if ((SpecimenStandardType)standard == SpecimenStandardType.Unknown)
                {
                    continue;
                }
                _availableStandards.Add(standard.ToString());
            }

            foreach (var acting in Enum.GetValues(typeof(SpecimenActingType)))
            {
                if ((SpecimenActingType)acting == SpecimenActingType.Unknown)
                {
                    continue;
                }
                _availableActing.Add(acting.ToString());
            }

            foreach (var panel in Enum.GetValues(typeof(SpecimenPanelType)))
            {
                var currentPanelType = (SpecimenPanelType)panel;

                if (currentPanelType == SpecimenPanelType.Unknown)
                {
                    continue;
                }
                _availablePanels.Add(currentPanelType.ToDisplayName());
            }

            foreach (var glazedInfilled in Enum.GetValues(typeof(SpecimenGlazedInfilledType)))
            {
                if ((SpecimenGlazedInfilledType)glazedInfilled == SpecimenGlazedInfilledType.Unknown)
                {
                    continue;
                }
                _availableGlazedInfilled.Add(glazedInfilled.ToString());
            }

            foreach (var insulated in Enum.GetValues(typeof(SpecimenInsulatedType)))
            {
                if ((SpecimenInsulatedType)insulated == SpecimenInsulatedType.Unknown)
                {
                    continue;
                }
                _availableInsulated.Add(insulated.ToString());
            }

            foreach (var latched in Enum.GetValues(typeof(SpecimenLatchedType)))
            {
                var currentLatchedType = (SpecimenLatchedType)latched;

                if (currentLatchedType == SpecimenLatchedType.Unknown)
                {
                    continue;
                }
                _availableLatched.Add(currentLatchedType.ToDisplayName());
            }

            foreach (var shootbolts in Enum.GetValues(typeof(SpecimenShootboltsType)))
            {
                var currentShootboltsType = (SpecimenShootboltsType)shootbolts;

                if (currentShootboltsType == SpecimenShootboltsType.Unknown)
                {
                    continue;
                }
                _availableShootbolts.Add(currentShootboltsType.ToDisplayName());
            }

        }

        public ICommand CreateCommand => _createCommand;

        public List<string> AvailableStandards => _availableStandards;

        public List<string> AvailableMaterials => _availableMaterials;

        public List<string> AvailableActing => _availableActing;

        public List<string> AvailablePanels => _availablePanels;

        public List<string> AvailableGlazedInfilled => _availableGlazedInfilled;

        public List<string> AvailableInsulated => _availableInsulated;

        public List<string> AvailableLatched => _availableLatched;

        public List<string> AvailableShootbolts => _availableShootbolts;

        public string SelectedStandard
        {
            get
            {
                return _selectedStandard;
            }
            set
            {
                if (Equals(value,_selectedStandard))
                {
                    return;
                }

                _selectedStandard = value;

                OnPropertyChanged(nameof(SelectedStandard));
            }
        }

        public string SelectedLHMaterial
        {
            get
            {
                return _selectedLHMaterial;
            }
            set
            {
                if (Equals(value,_selectedLHMaterial))
                {
                    return;
                }

                _selectedLHMaterial = value;

                OnPropertyChanged(nameof(SelectedLHMaterial));
            }
        }
        public string SelectedRHMaterial
        {
            get
            {
                return _selectedRHMaterial;
            }
            set
            {
                if (Equals(value, _selectedRHMaterial))
                {
                    return;
                }

                _selectedRHMaterial = value;

                OnPropertyChanged(nameof(SelectedRHMaterial));
            }
        }

        public string SelectedLHActing
        {
            get
            {
                return _selectedLHActing;
            }
            set
            {
                if (Equals(value,_selectedLHActing))
                {
                    return;
                }

                _selectedLHActing = value;

                OnPropertyChanged(nameof(SelectedLHActing));
            }
        }
        public string SelectedRHActing
        {
            get
            {
                return _selectedRHActing;
            }
            set
            {               
                if (Equals(value,_selectedRHActing))
                {
                    return;
                }

                _selectedRHActing = value;

                OnPropertyChanged(nameof(SelectedRHActing));
            }
        }

        public string SelectedLHPanels
        {
            get
            {
                return _selectedRHPanels;
            }
            set
            {
                if (Equals(value,_selectedLHPanels))
                {
                    return;
                }

                _selectedLHPanels = value;

                OnPropertyChanged(nameof(SelectedLHPanels));
            }
        }
        public string SelectedRHPanels
        {
            get
            {
                return _selectedRHPanels;
            }
            set
            {
                if (Equals(value,_selectedRHPanels))
                {
                    return;
                }

                _selectedRHPanels = value;

                OnPropertyChanged(nameof(SelectedRHPanels));
            }
        }

        public string SelectedLHGlazedInfilled
        {
            get
            {
                return _selectedLHGlazedInfilled;
            }
            set
            {
                if (Equals(value,_selectedLHGlazedInfilled))
                {
                    return;
                }

                _selectedLHGlazedInfilled = value;

                OnPropertyChanged(nameof(SelectedLHGlazedInfilled));
            }
        }
        public string SelectedRHGlazedInfilled
        {
            get
            {
                return _selectedRHGlazedInfilled;
            }
            set
            {
                if (Equals(value,_selectedRHGlazedInfilled))
                {
                    return;
                }

                _selectedRHGlazedInfilled = value;

                OnPropertyChanged(nameof(SelectedRHGlazedInfilled));
            }
        }

        public string SelectedLHInsulated
        {
            get
            {
                return _selectedLHInsulated;
            }
            set
            {
                if (Equals(value,_selectedLHInsulated))
                {
                    return;
                }

                _selectedLHInsulated = value;

                OnPropertyChanged(nameof(SelectedLHInsulated));
            }
        }
        public string SelectedRHInsulated
        {
            get
            {
                return _selectedRHInsulated;
            }
            set
            {
                if (Equals(value,_selectedRHInsulated))
                {
                    return;
                }

                _selectedRHInsulated = value;

                OnPropertyChanged(nameof(SelectedRHInsulated));
            }
        }

        public string SelectedLHLatched
        {
            get
            {
                return _selectedLHLatched;
            }
            set
            {
                if (Equals(value,_selectedLHLatched))
                {
                    return;
                }

                _selectedLHLatched = value;

                OnPropertyChanged(nameof(SelectedLHLatched));
            }
        }
        public string SelectedRHLatched
        {
            get
            {
                return _selectedRHLatched;
            }
            set
            {
                if (Equals(value,_selectedRHLatched))
                {
                    return;
                }

                _selectedRHLatched = value;

                OnPropertyChanged(nameof(SelectedRHLatched));
            }
        }

        public string SelectedLHShootbolts
        {
            get
            {
                return _selectedLHShootbolts;
            }
            set
            {
                if (Equals(value,_selectedLHShootbolts))
                {
                    return;
                }

                _selectedLHShootbolts = value;

                OnPropertyChanged(nameof(SelectedLHShootbolts));
            }
        }
        public string SelectedRHShootbolts
        {
            get
            {
                return _selectedRHShootbolts;
            }
            set
            {
                if (Equals(value,_selectedRHShootbolts))
                {
                    return;
                }

                _selectedRHShootbolts = value;

                OnPropertyChanged(nameof(SelectedRHShootbolts));
            }
        }

        public string TestNumber
        {
            get => _testNumber;
            set
            {
                if (Equals(value,_testNumber))
                {
                    return;
                }

                _testNumber = value;

                OnPropertyChanged(nameof(TestNumber));
            }
        }
        public string SponsorName
        {
            get => _sponsorName;
            set
            {
                if (Equals(value,_sponsorName))
                {
                    return;
                }

                _sponsorName = value;

                OnPropertyChanged(nameof(SponsorName));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (Equals(value,_address))
                {
                    return;
                }

                _address = value;

                OnPropertyChanged(nameof(Address));
            }
        }

        public bool IsSampleReport
        {
            get => _isSampleReport;
            set
            {
                if (value == _isSampleReport)
                {
                    return;
                }

                _isSampleReport = value;

                OnPropertyChanged(nameof(IsSampleReport));
            }
        }

        public bool IsIdentical
        {
            get => _isIdentical;
            set
            {
                if (value == _isIdentical)
                {
                    return;
                }

                _isIdentical = value;

                OnPropertyChanged(nameof(IsIdentical));

                UpdateDoorsetText();

                ClearRHDoorset();
            }
        }

        private void ClearRHDoorset()
        {
            if (!IsIdentical)
            {
                return;
            }

            SelectedRHMaterial = null;
            SelectedRHActing = null;
            SelectedRHPanels = null;
            SelectedRHGlazedInfilled = null;
            SelectedRHInsulated = null;
            SelectedRHLatched = null;
            SelectedRHShootbolts = null;
            IsRightHandGlazed = false;
            IsRightHandOpeningTowardsHeatConditions = false;

        }

        private void UpdateDoorsetText()
        {
            IsRHVisible = IsIdentical ? Visibility.Hidden : Visibility.Visible;
            LHText = IsIdentical ? "Doorsets" : "LH Doorset";
        }

        public string LHText
        {
            get => _lhText;
            set
            {
                if (value == _lhText)
                {
                    return;
                }

                _lhText = value;

                OnPropertyChanged(nameof(LHText));
            }
        }

        public Visibility IsRHVisible
        {
            get => _isRHVisible;
            set
            {
                if (value == _isRHVisible)
                {
                    return;
                }

                _isRHVisible = value;

                OnPropertyChanged(nameof(IsRHVisible));
            }
        }

        public bool IsLeftHandGlazed
        {
            get => _isLeftHandGlazed;
            set
            {
                if (value == _isLeftHandGlazed)
                {
                    return;
                }

                _isLeftHandGlazed = value;

                OnPropertyChanged(nameof(IsLeftHandGlazed));
            }
        }

        public bool IsRightHandGlazed
        {
            get => _isRightHandGlazed;
            set
            {
                if (value == _isRightHandGlazed)
                {
                    return;
                }

                _isRightHandGlazed = value;

                OnPropertyChanged(nameof(IsRightHandGlazed));
            }
        }

        public bool IsLeftHandOpeningTowardsHeatConditions
        {
            get => _isLeftHandOpeningTowardsHeatConditions;
            set
            {
                if (value == _isLeftHandOpeningTowardsHeatConditions)
                {
                    return;
                }

                _isLeftHandOpeningTowardsHeatConditions = value;

                OnPropertyChanged(nameof(IsLeftHandOpeningTowardsHeatConditions));
            }
        }

        public bool IsRightHandOpeningTowardsHeatConditions
        {
            get => _isRightHandOpeningTowardsHeatConditions;
            set
            {
                if (value == _isRightHandOpeningTowardsHeatConditions)
                {
                    return;
                }

                _isRightHandOpeningTowardsHeatConditions = value;

                OnPropertyChanged(nameof(IsRightHandOpeningTowardsHeatConditions));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}