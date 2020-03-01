using PoeTradeDesktop.Schemes.Filtering;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class WeaponFilterControl : BaseControl
    {
        #region Properties
        public ICommand HideContentCMD { get; set; }
        private SearchControl Parent;

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.WeaponFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private float damageMin;
        public float DamageMin
        {
            get { return damageMin; }
            set { damageMin = value; RaisePropertyChanged("DamageMin"); }
        }

        private float critMin;
        public float CritMin
        {
            get { return critMin; }
            set { critMin = value; RaisePropertyChanged("CritMin"); }
        }

        private float pdpsMin;
        public float PdpsMin
        {
            get { return pdpsMin; }
            set { pdpsMin = value; RaisePropertyChanged("PdpsMin"); }
        }

        private float apsMin;
        public float ApsMin
        {
            get { return apsMin; }
            set { apsMin = value; RaisePropertyChanged("ApsMin"); }
        }

        private float dpsMin;
        public float DpsMin
        {
            get { return dpsMin; }
            set { dpsMin = value; RaisePropertyChanged("DpsMin"); }
        }

        private float edpsMin;
        public float EdpsMin
        {
            get { return edpsMin; }
            set { edpsMin = value; RaisePropertyChanged("EdpsMin"); }
        }

        private float damageMax;
        public float DamageMax
        {
            get { return damageMax; }
            set { damageMax = value; RaisePropertyChanged("DamageMax"); }
        }

        private float critMax;
        public float CritMax
        {
            get { return critMax; }
            set { critMax = value; RaisePropertyChanged("CritMax"); }
        }

        private float pdpsMax;
        public float PdpsMax
        {
            get { return pdpsMax; }
            set { pdpsMax = value; RaisePropertyChanged("PdpsMax"); }
        }

        private float apsMax;
        public float ApsMax
        {
            get { return apsMax; }
            set { apsMax = value; RaisePropertyChanged("ApsMax"); }
        }

        private float dpsMax;
        public float DpsMax
        {
            get { return dpsMax; }
            set { dpsMax = value; RaisePropertyChanged("DpsMax"); }
        }

        private float edpsMax;
        public float EdpsMax
        {
            get { return edpsMax; }
            set { edpsMax = value; RaisePropertyChanged("EdpsMax"); }
        }
        #endregion Properties

        public WeaponFilterControl(object parent)
        {
            Parent = parent as SearchControl;
            HideContentCMD = new RelayCommand(HideContent);
        }

        public void HideContent(object o)
        {
            Parent.IsFilterContentHidden = true;
        }

        public dynamic GetFilter()
        {
            if (FilterEnabled)
            {
                return new
                {
                    disabled = false,
                    filters = new
                    {
                        damage = new { min = DamageMin, max = DamageMax },
                        dps = new { min = DpsMin, max = DpsMax },
                        aps = new { min = ApsMin, max = ApsMax },
                        pdps = new { min = PdpsMin, max = PdpsMax },
                        edps = new { min = EdpsMin, max = EdpsMax },
                        crit = new { min = CritMin, max = CritMax }
                    }
                };
            }

            return null;
        }

    }
}
