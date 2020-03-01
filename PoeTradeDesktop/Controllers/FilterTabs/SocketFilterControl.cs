using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class SocketFilterControl : BaseControl
    {
        #region Properties
        public ICommand HideContentCMD { get; set; }
        private SearchControl Parent;

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.SocketFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private int? socketR;
        public int? SocketR
        {
            get { return socketR; }
            set { socketR = value; RaisePropertyChanged("SocketR"); }
        }
        private int? socketG;
        public int? SocketG
        {
            get { return socketG; }
            set { socketG = value; RaisePropertyChanged("SocketG"); }
        }
        private int? socketB;
        public int? SocketB
        {
            get { return socketB; }
            set { socketB = value; RaisePropertyChanged("SocketB"); }
        }
        private int? socketW;
        public int? SocketW
        {
            get { return socketW; }
            set { socketW = value; RaisePropertyChanged("SocketW"); }
        }
        private int? socketMin;
        public int? SocketMin
        {
            get { return socketMin; }
            set { socketMin = value; RaisePropertyChanged("SocketMin"); }
        }
        private int? socketMax;
        public int? SocketMax
        {
            get { return socketMax; }
            set { socketMax = value; RaisePropertyChanged("SocketMax"); }
        }

        private int? linkR;
        public int? LinkR
        {
            get { return linkR; }
            set { linkR = value; RaisePropertyChanged("LinkR"); }
        }
        private int? linkG;
        public int? LinkG
        {
            get { return linkG; }
            set { linkG = value; RaisePropertyChanged("LinkG"); }
        }
        private int? linkB;
        public int? LinkB
        {
            get { return linkB; }
            set { linkB = value; RaisePropertyChanged("LinkB"); }
        }
        private int? linkW;
        public int? LinkW
        {
            get { return linkW; }
            set { linkW = value; RaisePropertyChanged("LinkW"); }
        }
        private int? linkMin;
        public int? LinkMin
        {
            get { return linkMin; }
            set { linkMin = value; RaisePropertyChanged("LinkMin"); }
        }
        private int? linkMax;
        public int? LinkMax
        {
            get { return linkMax; }
            set { linkMax = value; RaisePropertyChanged("LinkMax"); }
        }

        #endregion Properties

        public SocketFilterControl(object parent)
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
                        sockets = new { 
                            r = ZeroIsNull(SocketR), 
                            g = ZeroIsNull(SocketG),
                            b = ZeroIsNull(SocketB),
                            w = ZeroIsNull(SocketW),
                            min = ZeroIsNull(SocketMin),
                            max = ZeroIsNull(SocketMax) 
                        },
                        links = new
                        {
                            r = ZeroIsNull(LinkR),
                            g = ZeroIsNull(LinkG),
                            b = ZeroIsNull(LinkB),
                            w = ZeroIsNull(LinkW),
                            min = ZeroIsNull(LinkMin),
                            max = ZeroIsNull(LinkMax)
                        }
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
