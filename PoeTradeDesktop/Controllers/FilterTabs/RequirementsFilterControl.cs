using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class RequirementsFilterControl : BaseControl
    {
        #region Properties
        public ICommand HideContentCMD { get; set; }
        private SearchControl Parent;

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.RequirementFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private int? lvlMin;
        public int? LvlMin
        {
            get { return lvlMin; }
            set { lvlMin = value; RaisePropertyChanged("LvlMin"); }
        }

        private int? dexMin;
        public int? DexMin
        {
            get { return dexMin; }
            set { dexMin = value; RaisePropertyChanged("DexMin"); }
        }

        private int? strMin;
        public int? StrMin
        {
            get { return strMin; }
            set { strMin = value; RaisePropertyChanged("StrMin"); }
        }

        private int? inteMin;
        public int? InteMin
        {
            get { return inteMin; }
            set { inteMin = value; RaisePropertyChanged("InteMin"); }
        }

        private int? lvlMax;
        public int? LvlMax
        {
            get { return lvlMax; }
            set { lvlMax = value; RaisePropertyChanged("LvlMax"); }
        }

        private int? dexMax;
        public int? DexMax
        {
            get { return dexMax; }
            set { dexMax = value; RaisePropertyChanged("DexMax"); }
        }

        private int? strMax;
        public int? StrMax
        {
            get { return strMax; }
            set { strMax = value; RaisePropertyChanged("StrMax"); }
        }

        private int? inteMax;
        public int? InteMax
        {
            get { return inteMax; }
            set { inteMax = value; RaisePropertyChanged("InteMax"); }
        }


        #endregion Properties

        public RequirementsFilterControl(object parent)
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
                        lvl = new { min = ZeroIsNull(LvlMin), max = ZeroIsNull(LvlMax) },
                        str = new { min = ZeroIsNull(StrMin), max = ZeroIsNull(StrMax) },
                        dex = new { min = ZeroIsNull(DexMin), max = ZeroIsNull(DexMax) },
                        Int = new { min = ZeroIsNull(InteMin), max = ZeroIsNull(InteMax) }
                    }
                };
            }

            return null;
        }

        private int? ZeroIsNull(int? n)
        {
            return n != 0 ? n : null;
        }

    }
}
