using PoeTradeDesktop.Schemes.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class MapFilterControl : BaseControl
    {
        #region Properties
        public ICommand HideContentCMD { get; set; }
        private SearchControl Parent;

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.MapFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }
      
        private int? mapTierMin;
        public int? MapTierMin
        {
            get { return mapTierMin; }
            set { mapTierMin = value; RaisePropertyChanged("MapTierMin"); }
        }
        private int? mapPacksizeMin;
        public int? MapPacksizeMin
        {
            get { return mapPacksizeMin; }
            set { mapPacksizeMin = value; RaisePropertyChanged("MapPacksizeMin"); }
        }
        private float? mapIIRMin;
        public float? MapIIRMin
        {
            get { return mapIIRMin; }
            set { mapIIRMin = value; RaisePropertyChanged("MapIIRMin"); }
        }
        private float? mapIIQMin;
        public float? MapIIQMin
        {
            get { return mapIIQMin; }
            set { mapIIQMin = value; RaisePropertyChanged("MapIIQMin"); }
        }
        private int? mapTierMax;
        public int? MapTierMax
        {
            get { return mapTierMax; }
            set { mapTierMax = value; RaisePropertyChanged("MapTierMax"); }
        }
        private int? mapPacksizeMax;
        public int? MapPacksizeMax
        {
            get { return mapPacksizeMax; }
            set { mapPacksizeMax = value; RaisePropertyChanged("MapPacksizeMax"); }
        }
        private float? mapIIRMax;
        public float? MapIIRMax
        {
            get { return mapIIRMax; }
            set { mapIIRMax = value; RaisePropertyChanged("MapIIRMax"); }
        }
        private float? mapIIQMax;
        public float? MapIIQMax
        {
            get { return mapIIQMax; }
            set { mapIIQMax = value; RaisePropertyChanged("MapIIQMax"); }
        }

        private int mapShapedIndex;
        public int MapShapedIndex
        {
            get { return mapShapedIndex; }
            set { mapShapedIndex = value; RaisePropertyChanged("MapShapedIndex"); }
        }

        private int mapElderIndex;
        public int MapElderIndex
        {
            get { return mapElderIndex; }
            set { mapElderIndex = value; RaisePropertyChanged("MapElderIndex"); }
        }

        private int mapBlightedIndex;
        public int MapBlightedIndex
        {
            get { return mapBlightedIndex; }
            set { mapBlightedIndex = value; RaisePropertyChanged("MapBlightedIndex"); }
        }

        private List<MapSeries> mapSeriesSource;
        public List<MapSeries> MapSeriesSource
        {
            get { return mapSeriesSource; }
            set { mapSeriesSource = value; RaisePropertyChanged("MapSeriesSource"); }
        }

        private MapSeries selectedMapSerie;
        public MapSeries SelectedMapSerie
        {
            get { return selectedMapSerie; }
            set { selectedMapSerie = value; RaisePropertyChanged("SelectedMapSerie"); }
        }


        #endregion Properties

        public MapFilterControl(object parent)
        {
            Parent = parent as SearchControl;
            HideContentCMD = new RelayCommand(HideContent);
            MapSeriesSource = MapSeries.Load();
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
                        map_tier = new { min = ZeroIsNull(MapTierMin), max = ZeroIsNull(MapTierMax) },
                        map_packsize = new { min = ZeroIsNull(MapPacksizeMin), max = ZeroIsNull(MapPacksizeMax) },
                        map_iir= new { min = ZeroIsNull(MapIIQMin), max = ZeroIsNull(MapIIQMax) },
                        map_iiq = new { min = ZeroIsNull(MapIIRMin), max = ZeroIsNull(MapIIRMax) },
                        map_shaped = IndexToAnyYesNo(MapShapedIndex),
                        map_elder = IndexToAnyYesNo(MapElderIndex),
                        map_blighted = IndexToAnyYesNo(MapBlightedIndex),
                        map_series = new { option = SelectedMapSerie.Id}
                    }
                };
            }

            return null;
        }

        private object IndexToAnyYesNo(int index)
        {
            if(index == 0) { return null; }
            if(index == 1) { return new { option = true}; }
            if(index == 2) { return new { option = false }; }
            return null;
        }

        private float? ZeroIsNull(float? n)
        {
            return n != 0 ? n : null;
        }

        private int? ZeroIsNull(int? n)
        {
            return n != 0 ? n : null;
        }

    }
}
