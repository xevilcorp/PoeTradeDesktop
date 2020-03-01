using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoeTradeDesktop.Controllers.FilterTabs
{
    public class MiscellaneousFilterControl : BaseControl
    {
        #region Properties
        public ICommand HideContentCMD { get; set; }
        private SearchControl Parent;

        private bool filterEnabled;
        public bool FilterEnabled
        {
            get { return filterEnabled; }
            set { filterEnabled = value; Parent.MiscellaneousFilterEnabled = value; RaisePropertyChanged("FilterEnabled"); }
        }

        private float? qualityMin;
        public float? QualityMin
        {
            get { return qualityMin; }
            set { qualityMin = value; RaisePropertyChanged("QualityMin"); }
        }

        private int? itemLevelMin;
        public int? ItemLevelMin
        {
            get { return itemLevelMin; }
            set { itemLevelMin = value; RaisePropertyChanged("ItemLevelMin"); }
        }

        private int? gemLevelMin;
        public int? GemLevelMin
        {
            get { return gemLevelMin; }
            set { gemLevelMin = value; RaisePropertyChanged("GemLevelMin"); }
        }

        private float? gemXpMin;
        public float? GemXpMin
        {
            get { return gemXpMin; }
            set { gemXpMin = value; RaisePropertyChanged("GemXpMin"); }
        }

        private int? talismanTierMin;
        public int? TalismanTierMin
        {
            get { return talismanTierMin; }
            set { talismanTierMin = value; RaisePropertyChanged("TalismanTierMin"); }
        }

        private int? talismanTierMax;
        public int? TalismanTierMax
        {
            get { return talismanTierMax; }
            set { talismanTierMax = value; RaisePropertyChanged("TalismanTierMax"); }
        }


        private float? qualityMax;
        public float? QualityMax
        {
            get { return qualityMax; }
            set { qualityMax = value; RaisePropertyChanged("QualityMax"); }
        }

        private int? itemLevelMax;
        public int? ItemLevelMax
        {
            get { return itemLevelMax; }
            set { itemLevelMax = value; RaisePropertyChanged("ItemLevelMax"); }
        }

        private int? gemLevelMax;
        public int? GemLevelMax
        {
            get { return gemLevelMax; }
            set { gemLevelMax = value; RaisePropertyChanged("GemLevelMax"); }
        }

        private float? gemXpMax;
        public float? GemXpMax
        {
            get { return gemXpMax; }
            set { gemXpMax = value; RaisePropertyChanged("GemXpMax"); }
        }

        private int shaperIndex;
        public int ShaperIndex
        {
            get { return shaperIndex; }
            set { shaperIndex = value; RaisePropertyChanged("ShaperIndex"); }
        }

        private int elderIndex;
        public int ElderIndex
        {
            get { return elderIndex; }
            set { elderIndex = value; RaisePropertyChanged("ElderIndex"); }
        }

        private int crusaderIndex;
        public int CrusaderIndex
        {
            get { return crusaderIndex; }
            set { crusaderIndex = value; RaisePropertyChanged("CrusaderIndex"); }
        }


        private int redeemerIndex;
        public int RedeemerIndex
        {
            get { return redeemerIndex; }
            set { redeemerIndex = value; RaisePropertyChanged("RedeemerIndex"); }
        }

        private int hunterIndex;
        public int HunterIndex
        {
            get { return hunterIndex; }
            set { hunterIndex = value; RaisePropertyChanged("HunterIndex"); }
        }

        private int warlordIndex;
        public int WarlordIndex
        {
            get { return warlordIndex; }
            set { warlordIndex = value; RaisePropertyChanged("WarlordIndex"); }
        }

        private int fracturedIndex;
        public int FracturedIndex
        {
            get { return fracturedIndex; }
            set { fracturedIndex = value; RaisePropertyChanged("FracturedIndex"); }
        }

        private int synthesisedIndex;
        public int SynthesisedIndex
        {
            get { return synthesisedIndex; }
            set { synthesisedIndex = value; RaisePropertyChanged("SynthesisedIndex"); }
        }

        private int alternateArtIndex;
        public int AlternateArtIndex
        {
            get { return alternateArtIndex; }
            set { alternateArtIndex = value; RaisePropertyChanged("AlternateArtIndex"); }
        }

        private int identifiedIndex;
        public int IdentifiedIndex
        {
            get { return identifiedIndex; }
            set { identifiedIndex = value; RaisePropertyChanged("IdentifiedIndex"); }
        }

        private int corruptedIndex;
        public int CorruptedIndex
        {
            get { return corruptedIndex; }
            set { corruptedIndex = value; RaisePropertyChanged("CorruptedIndex"); }
        }

        private int mirroredIndex;
        public int MirroredIndex
        {
            get { return mirroredIndex; }
            set { mirroredIndex = value; RaisePropertyChanged("MirroredIndex"); }
        }

        private int craftedIndex;
        public int CraftedIndex
        {
            get { return craftedIndex; }
            set { craftedIndex = value; RaisePropertyChanged("CraftedIndex"); }
        }

        private int veiledIndex;
        public int VeiledIndex
        {
            get { return veiledIndex; }
            set { veiledIndex = value; RaisePropertyChanged("VeiledIndex"); }
        }

        private int enchantedIndex;
        public int EnchantedIndex
        {
            get { return enchantedIndex; }
            set { enchantedIndex = value; RaisePropertyChanged("EnchantedIndex"); }
        }

        #endregion Properties

        public MiscellaneousFilterControl(object parent)
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
                        gem_level = new { min = ZeroIsNull(GemLevelMin), max = ZeroIsNull(GemLevelMax) },
                        quality = new { min = ZeroIsNull(QualityMin), max = ZeroIsNull(QualityMax) },
                        ilvl = new { min = ZeroIsNull(ItemLevelMin), max = ZeroIsNull(ItemLevelMax) },
                        gem_level_progress = new { min = ZeroIsNull(GemXpMin), max = ZeroIsNull(GemXpMax) },
                        talisman_tier = new { min = ZeroIsNull(TalismanTierMin), max = ZeroIsNull(TalismanTierMax) },
                        shaper_item = IndexToAnyYesNo(ShaperIndex),
                        elder_item = IndexToAnyYesNo(ElderIndex),
                        crusader_item = IndexToAnyYesNo(CrusaderIndex),
                        redeemer_item = IndexToAnyYesNo(RedeemerIndex),
                        fractured_item = IndexToAnyYesNo(FracturedIndex),
                        synthesised_item = IndexToAnyYesNo(SynthesisedIndex),
                        hunter_item = IndexToAnyYesNo(HunterIndex),
                        warlord_item = IndexToAnyYesNo(WarlordIndex),
                        alternate_art = IndexToAnyYesNo(AlternateArtIndex),
                        identified = IndexToAnyYesNo(IdentifiedIndex),
                        mirrored = IndexToAnyYesNo(MirroredIndex),
                        veiled = IndexToAnyYesNo(VeiledIndex),
                        corrupted = IndexToAnyYesNo(CorruptedIndex),
                        crafted = IndexToAnyYesNo(CraftedIndex),
                        enchanted = IndexToAnyYesNo(EnchantedIndex)
                    }
                };
            }

            return null;
        }

        private object IndexToAnyYesNo(int index)
        {
            if (index == 0) { return null; }
            if (index == 1) { return new { option = true }; }
            if (index == 2) { return new { option = false }; }
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
