


/* --> cTrader Guru | Template 'Extend cBot' 1.0.9

    Homepage    : https://ctrader.guru/
    Telegram    : https://t.me/ctraderguru
    Twitter     : https://twitter.com/cTraderGURU/
    Facebook    : https://www.facebook.com/ctrader.guru/
    YouTube     : https://www.youtube.com/cTraderGURU
    GitHub      : https://github.com/ctrader-guru


    Edit only   :

        . NAME and VERSION of this cbot according to cBot Name "public class ExtendcBot : Strategy{...}"
        . All methods of "public class Strategy : Robot{...}"
        . Then intialize "public void StrategyInitialize(){...}";
        . If necessary, add the configuration parameters of any indicators

*/



using System;
using System.Collections.Generic;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using System.Globalization;



namespace cAlgo
{

    public static class Extensions
    {

        #region Enum

        public enum ColorNameEnum
        {

            AliceBlue,
            AntiqueWhite,
            Aqua,
            Aquamarine,
            Azure,
            Beige,
            Bisque,
            Black,
            BlanchedAlmond,
            Blue,
            BlueViolet,
            Brown,
            BurlyWood,
            CadetBlue,
            Chartreuse,
            Chocolate,
            Coral,
            CornflowerBlue,
            Cornsilk,
            Crimson,
            Cyan,
            DarkBlue,
            DarkCyan,
            DarkGoldenrod,
            DarkGray,
            DarkGreen,
            DarkKhaki,
            DarkMagenta,
            DarkOliveGreen,
            DarkOrange,
            DarkOrchid,
            DarkRed,
            DarkSalmon,
            DarkSeaGreen,
            DarkSlateBlue,
            DarkSlateGray,
            DarkTurquoise,
            DarkViolet,
            DeepPink,
            DeepSkyBlue,
            DimGray,
            DodgerBlue,
            Firebrick,
            FloralWhite,
            ForestGreen,
            Fuchsia,
            Gainsboro,
            GhostWhite,
            Gold,
            Goldenrod,
            Gray,
            Green,
            GreenYellow,
            Honeydew,
            HotPink,
            IndianRed,
            Indigo,
            Ivory,
            Khaki,
            Lavender,
            LavenderBlush,
            LawnGreen,
            LemonChiffon,
            LightBlue,
            LightCoral,
            LightCyan,
            LightGoldenrodYellow,
            LightGray,
            LightGreen,
            LightPink,
            LightSalmon,
            LightSeaGreen,
            LightSkyBlue,
            LightSlateGray,
            LightSteelBlue,
            LightYellow,
            Lime,
            LimeGreen,
            Linen,
            Magenta,
            Maroon,
            MediumAquamarine,
            MediumBlue,
            MediumOrchid,
            MediumPurple,
            MediumSeaGreen,
            MediumSlateBlue,
            MediumSpringGreen,
            MediumTurquoise,
            MediumVioletRed,
            MidnightBlue,
            MintCream,
            MistyRose,
            Moccasin,
            NavajoWhite,
            Navy,
            OldLace,
            Olive,
            OliveDrab,
            Orange,
            OrangeRed,
            Orchid,
            PaleGoldenrod,
            PaleGreen,
            PaleTurquoise,
            PaleVioletRed,
            PapayaWhip,
            PeachPuff,
            Peru,
            Pink,
            Plum,
            PowderBlue,
            Purple,
            Red,
            RosyBrown,
            RoyalBlue,
            SaddleBrown,
            Salmon,
            SandyBrown,
            SeaGreen,
            SeaShell,
            Sienna,
            Silver,
            SkyBlue,
            SlateBlue,
            SlateGray,
            Snow,
            SpringGreen,
            SteelBlue,
            Tan,
            Teal,
            Thistle,
            Tomato,
            Transparent,
            Turquoise,
            Violet,
            Wheat,
            White,
            WhiteSmoke,
            Yellow,
            YellowGreen

        }

        public enum CapitalTo
        {

            Balance,
            Equity

        }

        public enum OpenTradeType
        {

            All,
            Buy,
            Sell

        }

        #endregion

        #region Helper

        public static Color ColorFromEnum(ColorNameEnum colorName)
        {

            return Color.FromName(colorName.ToString("G"));

        }

        #endregion

        #region Bars

        public static int GetIndexByDate(this Bars thisBars, DateTime thisTime)
        {

            for (int i = thisBars.OpenTimes.Count - 1; i >= 0; i--)
            {

                if (thisTime == thisBars.OpenTimes[i])
                    return i;

            }

            return -1;

        }

        public static double LastGAP(this Bars thisBars, int thisDigits = 5)
        {

            return Math.Round(Math.Abs(thisBars.ClosePrices.Last(1) - thisBars.OpenPrices.Last(0)), thisDigits);

        }

        #endregion

        #region Bar

        public static double Body(this Bar thisBar, int thisDigits = 5)
        {

            return Math.Round(Math.Abs(thisBar.Close - thisBar.Open), thisDigits);

        }

        public static bool IsBullish(this Bar thisBar)
        {

            return thisBar.Close > thisBar.Open;

        }

        public static bool IsBearish(this Bar thisBar)
        {

            return thisBar.Close < thisBar.Open;

        }

        public static bool IsDoji(this Bar thisBar)
        {

            return thisBar.Close == thisBar.Open;

        }

        #endregion

        #region Symbol

        public static double PipsToDigits(this Symbol thisSymbol, double Pips)
        {

            return Math.Round(Pips * thisSymbol.PipSize, thisSymbol.Digits);

        }

        public static double RealSpread(this Symbol thisSymbol)
        {

            return Math.Round(thisSymbol.Spread / thisSymbol.PipSize, 2);

        }

        #endregion

        #region TimeFrame

        public static int ToMinutes(this TimeFrame thisTimeFrame)
        {

            if (thisTimeFrame == TimeFrame.Daily)
                return 60 * 24;
            if (thisTimeFrame == TimeFrame.Day2)
                return 60 * 24 * 2;
            if (thisTimeFrame == TimeFrame.Day3)
                return 60 * 24 * 3;
            if (thisTimeFrame == TimeFrame.Hour)
                return 60;
            if (thisTimeFrame == TimeFrame.Hour12)
                return 60 * 12;
            if (thisTimeFrame == TimeFrame.Hour2)
                return 60 * 2;
            if (thisTimeFrame == TimeFrame.Hour3)
                return 60 * 3;
            if (thisTimeFrame == TimeFrame.Hour4)
                return 60 * 4;
            if (thisTimeFrame == TimeFrame.Hour6)
                return 60 * 6;
            if (thisTimeFrame == TimeFrame.Hour8)
                return 60 * 8;
            if (thisTimeFrame == TimeFrame.Minute)
                return 1;
            if (thisTimeFrame == TimeFrame.Minute10)
                return 10;
            if (thisTimeFrame == TimeFrame.Minute15)
                return 15;
            if (thisTimeFrame == TimeFrame.Minute2)
                return 2;
            if (thisTimeFrame == TimeFrame.Minute20)
                return 20;
            if (thisTimeFrame == TimeFrame.Minute3)
                return 3;
            if (thisTimeFrame == TimeFrame.Minute30)
                return 30;
            if (thisTimeFrame == TimeFrame.Minute4)
                return 4;
            if (thisTimeFrame == TimeFrame.Minute45)
                return 45;
            if (thisTimeFrame == TimeFrame.Minute5)
                return 5;
            if (thisTimeFrame == TimeFrame.Minute6)
                return 6;
            if (thisTimeFrame == TimeFrame.Minute7)
                return 7;
            if (thisTimeFrame == TimeFrame.Minute8)
                return 8;
            if (thisTimeFrame == TimeFrame.Minute9)
                return 9;
            if (thisTimeFrame == TimeFrame.Monthly)
                return 60 * 24 * 30;
            if (thisTimeFrame == TimeFrame.Weekly)
                return 60 * 24 * 7;

            return 0;

        }

        #endregion

        #region DateTime

        public static double ToDouble(this DateTime thisDateTime, string Culture = "en-EN")
        {

            string nowHour = (thisDateTime.Hour < 10) ? string.Format("0{0}", thisDateTime.Hour) : string.Format("{0}", thisDateTime.Hour);
            string nowMinute = (thisDateTime.Minute < 10) ? string.Format("0{0}", thisDateTime.Minute) : string.Format("{0}", thisDateTime.Minute);

            return string.Format("{0}.{1}", nowHour, nowMinute).ToDouble(Culture);

        }

        #endregion

        #region String

        public static double ToDouble(this string thisString, string Culture = "en-EN")
        {


            var culture = CultureInfo.GetCultureInfo(Culture);
            return double.Parse(thisString.Replace(',', '.').ToString(CultureInfo.InvariantCulture), culture);

        }

        #endregion

        #region Class

        public class MonenyManagement
        {

            private readonly double _minSize = 0.01;
            private double _percentage = 0;
            private double _fixedSize = 0;
            private double _pipToCalc = 30;

            private readonly IAccount _account = null;
            public readonly Symbol Symbol;

            public CapitalTo CapitalType = CapitalTo.Balance;

            public double Percentage
            {

                get { return _percentage; }


                set { _percentage = (value > 0 && value <= 100) ? value : 0; }
            }

            public double FixedSize
            {

                get { return _fixedSize; }



                set { _fixedSize = (value >= _minSize) ? value : 0; }
            }

            public double PipToCalc
            {

                get { return _pipToCalc; }

                set { _pipToCalc = (value > 0) ? value : 100; }
            }


            public double Capital
            {

                get
                {

                    switch (CapitalType)
                    {

                        case CapitalTo.Equity:

                            return _account.Equity;
                        default:


                            return _account.Balance;

                    }

                }
            }

            public MonenyManagement(IAccount NewAccount, CapitalTo NewCapitalTo, double NewPercentage, double NewFixedSize, double NewPipToCalc, Symbol NewSymbol)
            {

                _account = NewAccount;

                Symbol = NewSymbol;

                CapitalType = NewCapitalTo;
                Percentage = NewPercentage;
                FixedSize = NewFixedSize;
                PipToCalc = NewPipToCalc;

            }

            public double GetLotSize()
            {

                if (FixedSize > 0)
                    return FixedSize;

                double moneyrisk = Capital / 100 * Percentage;

                double sl_double = PipToCalc * Symbol.PipSize;

                // --> 0.01 = microlotto double lots = Math.Round(Symbol.VolumeInUnitsToQuantity(moneyrisk / ((sl_double * Symbol.TickValue) / Symbol.TickSize)), 2);
                // --> volume 1K = 1000 Math.Round((moneyrisk / ((sl_double * Symbol.TickValue) / Symbol.TickSize)), 2);
                double lots = Math.Round(Symbol.VolumeInUnitsToQuantity(moneyrisk / ((sl_double * Symbol.TickValue) / Symbol.TickSize)), 2);

                if (lots < _minSize)
                    return _minSize;

                return lots;

            }

        }

        #endregion

    }

}



namespace cAlgo.Robots
{

    public class Strategy : Robot
    {

        public ExponentialMovingAverage FastEMA;
        public ExponentialMovingAverage SlowEMA;

        /* Override Example
        public virtual double EMAFilter { get; set; } // <-- If you want to use parameters, you have to overwrite them.
        */
        public bool TriggerBuy
        {



            get { return FastEMA.Result.Last(2) < SlowEMA.Result.Last(2) && FastEMA.Result.Last(1) > SlowEMA.Result.Last(1); }
        }



        public bool TriggerSell
        {



            get { return FastEMA.Result.Last(2) > SlowEMA.Result.Last(2) && FastEMA.Result.Last(1) < SlowEMA.Result.Last(1); }
        }



        public bool FilterBuy
        {



            get { return true; }
        }


        public bool FilterSell
        {



            get { return true; }
        }


        public bool Buy
        {



            get { return FilterBuy && TriggerBuy; }
        }


        public bool Sell
        {



            get { return FilterSell && TriggerSell; }
        }


    }

    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class EMAPower : Strategy
    {

        #region Identity

        public const string NAME = "EMA Power";

        public const string VERSION = "1.0.3";

        #endregion

        #region Params

        #region Identity

        [Parameter(NAME + " " + VERSION, Group = "Identity", DefaultValue = "https://www.google.com/search?q=ctrader+guru+ema+power")]
        public string ProductInfo { get; set; }

        [Parameter("Label ( Magic Name )", Group = "Identity", DefaultValue = NAME)]
        public string MyLabel { get; set; }

        [Parameter("Preset information", Group = "Identity", DefaultValue = "USDJPY 5m | 29.04.2021 to 29.04.2022 | €1000")]
        public string PresetInfo { get; set; }

        #endregion

        #region Strategy

        [Parameter("Open Trade Type", Group = "Strategy", DefaultValue = Extensions.OpenTradeType.Buy)]
        public Extensions.OpenTradeType MyOpenTradeType { get; set; }

        [Parameter("Stop Loss (money)", Group = "Strategy", DefaultValue = 30, MinValue = 0.1, Step = 0.1)]
        public double StopLossMoney { get; set; }

        [Parameter("Take Profit (money)", Group = "Strategy", DefaultValue = 150, MinValue = 0.1, Step = 0.1)]
        public double TakeProfitMoney { get; set; }

        [Parameter("Close On Trigger?", Group = "Strategy", DefaultValue = false)]
        public bool CloseOnTrigger { get; set; }

        [Parameter("Use Deviation Martingala? (bypass all)", Group = "Strategy", DefaultValue = true)]
        public bool UseDM { get; set; }

        #endregion

        #region Pausa

        [Parameter("Close All At (20.59 = 20:59, 0 = disabled)", Group = "Pause", DefaultValue = 0, MinValue = 0, MaxValue = 23.59, Step = 0.01)]
        public double CloseAllAt { get; set; }

        [Parameter("From (18.0 = 18:00)", Group = "Pause", DefaultValue = 0, MinValue = 0, MaxValue = 23.59, Step = 0.01)]
        public double PauseFrom { get; set; }

        [Parameter("To (8.20 = 08:20)", Group = "Pause", DefaultValue = 0, MinValue = 0, MaxValue = 23.59, Step = 0.01)]
        public double PauseTo { get; set; }

        public bool IAmInPause
        {

            get
            {

                if (PauseFrom == 0 && PauseTo == 0)
                    return false;

                double now = Server.Time.ToDouble();

                bool intraday = (PauseFrom < PauseTo && now >= PauseFrom && now <= PauseTo);
                bool overnight = (PauseFrom > PauseTo && ((now >= PauseFrom && now <= 23.59) || now <= PauseTo));

                return intraday || overnight;

            }
        }


        #endregion

        #region Filters

        [Parameter("Max Spread allowed", Group = "Filters", DefaultValue = 1.5, MinValue = 0.1, Step = 0.1)]
        public double SpreadToTrigger { get; set; }

        [Parameter("Max GAP Allowed (pips)", Group = "Filters", DefaultValue = 2, MinValue = 0, Step = 0.01)]
        public double GAP { get; set; }

        [Parameter("Max Number of Trades", Group = "Filters", DefaultValue = 1, MinValue = 1, Step = 1)]
        public int MaxTrades { get; set; }

        #endregion

        #region Money Target

        [Parameter("Percentage (zero = disabled)", Group = "Money Target", DefaultValue = 0, MinValue = 0, Step = 0.1)]
        public double MoneyTargetPercentage { get; set; }
        public double MoneyTarget
        {



            get { return Math.Round((Account.Balance / 100) * MoneyTargetPercentage, 2); }
        }


        [Parameter("Minimum Trades to Activation", Group = "Money Target", DefaultValue = 1, MinValue = 1, Step = 1)]
        public int MoneyTargetTrades { get; set; }

        #endregion

        #region Money Management

        [Parameter("Fixed Lots (bypass all Capital)", Group = "Money Management", DefaultValue = 0, MinValue = 0, Step = 0.01)]
        public double FixedLots { get; set; }

        [Parameter("Capital", Group = "Money Management", DefaultValue = Extensions.CapitalTo.Balance)]
        public Extensions.CapitalTo MyCapital { get; set; }

        [Parameter("% Risk", Group = "Money Management", DefaultValue = 1, MinValue = 0.1, Step = 0.1)]
        public double MyRisk { get; set; }

        [Parameter("Pips To Calculate ( empty = stoploss )", Group = "Money Management", DefaultValue = 30, MinValue = 0, Step = 0.1)]
        public double FakeSL { get; set; }

        [Parameter("% Max (zero = disabled)", Group = "Drawdown", DefaultValue = 0, MinValue = 0, MaxValue = 100, Step = 0.1)]
        public double DDPercentage { get; set; }

        #endregion

        #region Indicators Setup

        [Parameter("Fast", Group = "EMA", DefaultValue = 30, MinValue = 1)]
        public int PeriodFastEMA { get; set; }

        [Parameter("Slow", Group = "EMA", DefaultValue = 50, MinValue = 2)]
        public int PeriodSlowEMA { get; set; }

        /* Override Example
        [Parameter("Filter", Group = "EMA", DefaultValue = 20, MinValue = 2)]
        public override double EMAFilter { get; set; }
        */

        #endregion

        #region Deviation Martingala

        [Parameter("Multiplier (zero = disabled)", Group = "Deviation Martingala", DefaultValue = 1.5, MinValue = 0, Step = 0.1)]
        public double DMMultiplier { get; set; }

        [Parameter("Max Consecutive Loss (zero = infinite)", Group = "Deviation Martingala", DefaultValue = 6, MinValue = 0, Step = 1)]
        public int DMMaxLoss { get; set; }

        #endregion

        #endregion

        #region Property

        // Rappresenta una posizione virtuale (ghost) gestita in memoria.
        // La posizione reale aperta sul broker è sempre nella direzione opposta.
        private class GhostPosition
        {

            public TradeType TradeType { get; set; }
            public double EntryPrice { get; set; }
            public DateTime EntryTime { get; set; }
            public double Quantity { get; set; }
            public double VolumeInUnits { get; set; }
            public int RealPositionId { get; set; }
            public double? FinalNetProfit { get; set; }
            public double? ExitPrice { get; set; }

            public double GetNetProfit(Symbol symbol)
            {

                double currentPrice = TradeType == TradeType.Buy ? symbol.Bid : symbol.Ask;
                double priceDiff = TradeType == TradeType.Buy
                    ? currentPrice - EntryPrice
                    : EntryPrice - currentPrice;

                return (priceDiff / symbol.PipSize) * symbol.PipValue * Quantity;

            }

        }

        public bool OpenedInThisBar = false;

        public double StrategyNetProfit = 0;

        private readonly List<GhostPosition> _ghostPositions = new List<GhostPosition>();

        private bool _closingAll = false;

        Extensions.MonenyManagement MonenyManagement1;

        public int ConsecutiveLoss = 0;

        public double CumulativeLoss = 0;

        #endregion

        #region cBot Events

        public void StrategyInitialize()
        {

            FastEMA = Indicators.ExponentialMovingAverage(Bars.ClosePrices, PeriodFastEMA);
            SlowEMA = Indicators.ExponentialMovingAverage(Bars.ClosePrices, PeriodSlowEMA);

        }

        public void StrategyRun()
        {

            bool UsingRecovery = UseDM && DMMultiplier > 0 && ConsecutiveLoss > 0;
            bool SharedConditions = !UsingRecovery && !OpenedInThisBar && _ghostPositions.Count < MaxTrades && Bars.LastGAP(Symbol.Digits) <= Symbol.PipsToDigits(GAP) && Symbol.RealSpread() <= SpreadToTrigger;

            if (Buy && Sell)
            {

                Print("Trigger Buy and Sell, strategy error.");
                return;

            }

            MonenyManagement1 = new Extensions.MonenyManagement(Account, MyCapital, MyRisk, FixedLots, FakeSL, Symbol);
            double lotSize = MonenyManagement1.GetLotSize();
            double volumeInUnits = Symbol.QuantityToVolumeInUnits(lotSize);

            if (Buy)
            {

                if (SharedConditions && MyOpenTradeType != Extensions.OpenTradeType.Sell)
                {

                    OpenGhostPosition(TradeType.Buy, volumeInUnits, lotSize, useRange: true);
                    Print("Ghost Buy on trigger, consecutive loss {0}", ConsecutiveLoss);

                }

            }
            else if (Sell)
            {

                if (SharedConditions && MyOpenTradeType != Extensions.OpenTradeType.Buy)
                {

                    OpenGhostPosition(TradeType.Sell, volumeInUnits, lotSize, useRange: true);
                    Print("Ghost Sell on trigger, consecutive loss {0}", ConsecutiveLoss);

                }

            }

        }

        protected override void OnStart()
        {

            Print(NAME, " ", VERSION);

            Positions.Opened += OnOpenPositions;
            Positions.Closed += OnClosePositions;

            StrategyInitialize();

        }

        protected override void OnTick()
        {

            if (CloseAllAt > 0 && Server.Time.ToDouble() >= CloseAllAt)
            {

                _closingAll = true;

                foreach (var ghost in _ghostPositions)
                {
                    double exitPrice = ghost.TradeType == TradeType.Buy ? Symbol.Bid : Symbol.Ask;
                    DrawGhostLine(ghost, exitPrice, ghost.GetNetProfit(Symbol));
                }

                foreach (var position in Positions.FindAll(MyLabel, SymbolName))
                    position.Close();

                _ghostPositions.Clear();
                ConsecutiveLoss = 0;
                CumulativeLoss = 0;
                _closingAll = false;

                return;

            }

            bool UsingRecovery = UseDM && DMMultiplier > 0 && ConsecutiveLoss > 0;

            // Calcola ghost equity complessiva (somma P&L floating di tutte le ghost)
            StrategyNetProfit = 0;
            foreach (var ghost in _ghostPositions)
                StrategyNetProfit += ghost.GetNetProfit(Symbol);

            bool OnMoneyTargetClose = MoneyTargetPercentage > 0 && _ghostPositions.Count >= MoneyTargetTrades && StrategyNetProfit >= MoneyTarget;
            double DDControl = Math.Round((Account.Balance / 100) * DDPercentage, 2) * -1;
            bool OnDrawDownClose = DDControl < 0 && StrategyNetProfit <= DDControl;

            // Valuta ogni ghost: SL/TP e trigger close
            var ghostsToClose = new List<GhostPosition>();

            foreach (var ghost in _ghostPositions)
            {

                double ghostNetProfit = ghost.GetNetProfit(Symbol);

                bool OnSLClose = ghostNetProfit <= -StopLossMoney;
                bool OnTPClose = ghostNetProfit >= (CumulativeLoss + TakeProfitMoney);

                if (OnSLClose || OnTPClose)
                {

                    ghost.FinalNetProfit = ghostNetProfit;
                    ghost.ExitPrice = ghost.TradeType == TradeType.Buy ? Symbol.Bid : Symbol.Ask;
                    ghostsToClose.Add(ghost);
                    continue;

                }

                if (!UsingRecovery)
                {

                    bool OnTriggerClose = CloseOnTrigger && ((Buy && ghost.TradeType == TradeType.Sell) || (Sell && ghost.TradeType == TradeType.Buy));

                    if (OnTriggerClose || OnMoneyTargetClose || OnDrawDownClose)
                    {

                        ghost.FinalNetProfit = ghostNetProfit;
                        ghost.ExitPrice = ghost.TradeType == TradeType.Buy ? Symbol.Bid : Symbol.Ask;
                        ghostsToClose.Add(ghost);
                        continue;

                    }

                }

            }

            // Chiude le posizioni reali (invertite) per ogni ghost che ha triggerato
            foreach (var ghost in ghostsToClose)
            {

                Position realPos = null;
                foreach (var p in Positions)
                {
                    if (p.Id == ghost.RealPositionId)
                    {
                        realPos = p;
                        break;
                    }
                }

                if (realPos != null)
                    realPos.Close();
                else
                    _ghostPositions.Remove(ghost);

            }

            StrategyRun();

        }

        protected override void OnBar()
        {

            OpenedInThisBar = false;

        }

        protected override void OnStop()
        {

            Positions.Opened -= OnOpenPositions;
            Positions.Closed -= OnClosePositions;

        }

        #endregion

        #region Methods

        private void OpenGhostPosition(TradeType ghostType, double volumeInUnits, double quantity, bool useRange = false)
        {

            double ghostEntryPrice = ghostType == TradeType.Buy ? Ask : Bid;
            TradeType realType = ghostType == TradeType.Buy ? TradeType.Sell : TradeType.Buy;

            TradeResult result;

            if (useRange)
            {
                double targetPrice = realType == TradeType.Buy ? Ask : Bid;
                result = ExecuteMarketRangeOrder(realType, SymbolName, volumeInUnits, 2, targetPrice, MyLabel, 0, 0);
            }
            else
            {
                result = ExecuteMarketOrder(realType, SymbolName, volumeInUnits, MyLabel, 0, 0);
            }

            if (result.IsSuccessful)
            {

                _ghostPositions.Add(new GhostPosition
                {
                    TradeType = ghostType,
                    EntryPrice = ghostEntryPrice,
                    EntryTime = Server.Time,
                    Quantity = quantity,
                    VolumeInUnits = volumeInUnits,
                    RealPositionId = result.Position.Id
                });

                Print("Ghost {0} entry {1:F2} → Real {2} | Qty {3}", ghostType, ghostEntryPrice, realType, quantity);

            }
            else
            {

                Print("Ghost open FAILED: {0}", result.Error);

            }

        }

        private void DrawGhostLine(GhostPosition ghost, double exitPrice, double ghostProfit)
        {

            Color lineColor = ghostProfit >= 0 ? Color.Green : Color.Red;
            string name = string.Format("ghost_{0}", ghost.RealPositionId);
            Chart.DrawTrendLine(name, ghost.EntryTime, ghost.EntryPrice, Server.Time, exitPrice, lineColor);

        }

        private void OnOpenPositions(PositionOpenedEventArgs eventArgs)
        {

            Position position = eventArgs.Position;
            if (position.SymbolName != SymbolName || position.Label != MyLabel)
                return;

            OpenedInThisBar = true;

        }

        private void OnClosePositions(PositionClosedEventArgs eventArgs)
        {

            Position position = eventArgs.Position;
            if (position.SymbolName != SymbolName || position.Label != MyLabel)
                return;

            // Trova il ghost agganciato alla posizione reale chiusa
            GhostPosition ghost = null;
            foreach (var g in _ghostPositions)
            {
                if (g.RealPositionId == position.Id)
                {
                    ghost = g;
                    break;
                }
            }

            if (ghost == null) return;

            _ghostPositions.Remove(ghost);

            if (_closingAll) return;

            // P&L ghost: usa il valore registrato al momento del trigger SL/TP,
            // oppure stima dall'inverso della posizione reale chiusa
            double ghostFinalProfit = ghost.FinalNetProfit.HasValue
                ? ghost.FinalNetProfit.Value
                : -position.NetProfit;

            double exitPrice = ghost.ExitPrice.HasValue
                ? ghost.ExitPrice.Value
                : (ghost.TradeType == TradeType.Buy ? Symbol.Bid : Symbol.Ask);

            DrawGhostLine(ghost, exitPrice, ghostFinalProfit);

            if (ghostFinalProfit < 0)
            {

                ConsecutiveLoss++;
                CumulativeLoss += Math.Abs(ghostFinalProfit);

                bool UseRecovery = UseDM && DMMultiplier > 0 && (DMMaxLoss == 0 || ConsecutiveLoss < DMMaxLoss);

                if (UseRecovery)
                {

                    TradeType nextGhostType = ghost.TradeType == TradeType.Sell ? TradeType.Buy : TradeType.Sell;
                    double newQuantity = Math.Round(ghost.Quantity * DMMultiplier, 2);
                    double newVolume = Symbol.QuantityToVolumeInUnits(newQuantity);

                    OpenGhostPosition(nextGhostType, newVolume, newQuantity);
                    Print("Ghost Martingala Deviation, consecutive loss {0}, cumulative ghost loss {1}", ConsecutiveLoss, CumulativeLoss);

                }
                else
                {

                    ConsecutiveLoss = 0;
                    CumulativeLoss = 0;

                }

            }
            else
            {

                ConsecutiveLoss = 0;
                CumulativeLoss = 0;

            }

        }

        #endregion

    }

}
