using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class ArmourFilterControl : BaseControl
    {
        #region Properties
        public ICommand HideContentCMD { get; set; }
        private SearchControl Parent;

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.ArmourFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private float? arMin;
        public float? ArMin
        {
            get { return arMin; }
            set { arMin = value; RaisePropertyChanged("ArMin"); }
        }

        private float? esMin;
        public float? EsMin
        {
            get { return esMin; }
            set { esMin = value; RaisePropertyChanged("EsMin"); }
        }

        private float? evMin;
        public float? EvMin
        {
            get { return evMin; }
            set { evMin = value; RaisePropertyChanged("EvMin"); }
        }

        private float? blockMin;
        public float? BlockMin
        {
            get { return blockMin; }
            set { blockMin = value; RaisePropertyChanged("BlockMin"); }
        }

        private float? arMax;
        public float? ArMax
        {
            get { return arMax; }
            set { arMax = value; RaisePropertyChanged("ArMax"); }
        }

        private float? esMax;
        public float? EsMax
        {
            get { return esMax; }
            set { esMax = value; RaisePropertyChanged("EsMax"); }
        }

        private float? evMax;
        public float? EvMax
        {
            get { return evMax; }
            set { evMax = value; RaisePropertyChanged("EvMax"); }
        }

        private float? blockMax;
        public float? BlockMax
        {
            get { return blockMax; }
            set { blockMax = value; RaisePropertyChanged("BlockMax"); }
        }
        #endregion Properties

        public ArmourFilterControl(object parent)
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
                    filters =new
                    {
                        ar = new { min = ZeroIsNull(ArMin), max = ZeroIsNull(ArMax) },
                        es = new { min = ZeroIsNull(EsMin), max = ZeroIsNull(EsMax) },
                        ev = new { min = ZeroIsNull(EvMin), max = ZeroIsNull(EvMax) },
                        block = new { min = ZeroIsNull(BlockMin), max = ZeroIsNull(BlockMax) }
                    }
                };
            }

            return null;
        }

        private float? ZeroIsNull(float? n)
        {
            return n != 0 ? n : null;
        }

    }
}
