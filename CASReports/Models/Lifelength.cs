using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CASReports.Models
{
    /// <summary>
    /// Описывает наработку (ресурс)
    /// </summary>
    [Serializable]
    public class Lifelength
    {

        /*
         * Свойства
         */

        #region public Int32? Cycles { get; set; }
        /// <summary>
        /// Количество циклов
        /// </summary>
        public Int32? Cycles { get; set; }
        #endregion

        #region public Int32? Hours { get; set; }
        /// <summary>
        /// Количество часов 
        /// </summary>
        public Int32? Hours
        {
            get { return TotalMinutes != null ? new Int32?(TotalMinutes.Value / 60) : null; }
            set
            {
                // Если задаваемое значение null, то просто присваиваем null, не обращая внимания на прошлое заданное значение
                if (value == null)
                {
                    TotalMinutes = null;
                }
                // Если прошлое значение не было задано до этого
                else if (TotalMinutes == null)
                {
                    TotalMinutes = value.Value * 60;
                }
                // Если значение уже было задано до этого
                else
                {
                    TotalMinutes = value.Value * 60 + Minutes;
                }
            }
        }
        #endregion

        #region public Int32? Minutes { get; set; }
        /// <summary>
        /// Количество минут 
        /// </summary>
        public Int32? Minutes
        {
            get { return TotalMinutes != null ? new Int32?(TotalMinutes.Value % 60) : null; }
            set
            {
                // Если значение null, то просто присваиваем null, не обращая внимания на прошлое заданное значение
                if (value == null)
                {
                    TotalMinutes = null;
                }
                // Если не null, но прошлое значение не было задано 
                else if (TotalMinutes == null)
                {
                    TotalMinutes = value.Value;
                }
                // Если новое значение не пустое и значение было задано до этого
                else
                {
                    TotalMinutes = Hours * 60 + value.Value;
                }
            }
        }
        #endregion

        #region public Int32? TotalMinutes { get; set; }
        /// <summary>
        /// Полное количество минут (оно и хранится в базе данных)
        /// </summary>
        public Int32? TotalMinutes { get; set; }
        #endregion

        #region public Int32? Days { get; set; }
        /// <summary>
        /// Количество дней
        /// </summary>
        public Int32? Days
        {
            get { return CalendarSpan.Days; }
            set
            {
                CalendarSpan.CalendarValue = value;
                CalendarSpan.CalendarType = CalendarTypes.Days;
            }
        }
        #endregion

        #region public Int32? CalendarValue { get; set; }
        /// <summary>
        /// Календарное значение
        /// </summary>
        public Int32? CalendarValue
        {
            get { return _calendarSpan != null ? _calendarSpan.CalendarValue : null; }
            set { CalendarSpan.CalendarValue = value; }
        }
        #endregion

        #region public CalendarTypes CalendarType { get; set; }
        /// <summary>
        /// Тип календаря
        /// </summary>
        public CalendarTypes CalendarType
        {
            get { return _calendarSpan != null ? _calendarSpan.CalendarType : CalendarTypes.Days; }
            set { CalendarSpan.CalendarType = value; }
        }
        #endregion

        #region public CalendarSpan CalendarSpan { get; set; }

        private CalendarSpan _calendarSpan;

        /// <summary>
        /// Тип календаря
        /// </summary>
        public CalendarSpan CalendarSpan
        {
            get { return _calendarSpan ?? (_calendarSpan = new CalendarSpan()); }
            set { _calendarSpan = value; }
        }
        #endregion

        /*
         * Статические объекты
         */

        #region public static Lifelength Zero { get; }
        /// <summary>
        /// Возвращает наработку (ресурс) с нулевыми (но не пустыми) параметрами
        /// </summary>
        public static Lifelength Zero
        {
            get
            {
                return new Lifelength(0, 0, 0);
            }
        }
        #endregion

        #region public static Lifelength Null { get; }
        /// <summary>
        /// Возвращает наработку (ресурс) с пустыми параметрами 
        /// </summary>
        public static Lifelength Null
        {
            get
            {
                return new Lifelength();
            }
        }
        #endregion

        #region public static int SerializedDataLength { get; }
        /// <summary>
        /// Gets length of any serialized <see cref="Lifelength"/>
        /// </summary>
        public static int SerializedDataLength
        {
            get { return 21; }
        }
        #endregion

        /*
         * Конструктор 
         */

        #region public Lifelength()
        /// <summary>
        /// Создает наработку (ресурс) с пустыми параметрами
        /// </summary>
        public Lifelength()
        {
        }
        #endregion

        #region public Lifelength(Int32? days, Int32? cycles, Int32? totalMinutes)
        /// <summary>
        /// Создает наработку (ресурс) с известными параметрами
        /// </summary>
        /// <param name="days"></param>
        /// <param name="cycles"></param>
        /// <param name="totalMinutes"></param>
        public Lifelength(Int32? days, Int32? cycles, Int32? totalMinutes)
        {
            Days = days;
            Cycles = cycles;
            TotalMinutes = totalMinutes;
        }
        #endregion

        #region public Lifelength(Lifelength source)
        /// <summary>
        /// Копирует наработку (Создает новую наработку с такими же параметрами)
        /// </summary>
        /// <param name="source"></param>
        public Lifelength(Lifelength source)
        {
            if (source == null) return;
            //
            Cycles = source.Cycles;
            CalendarSpan = new CalendarSpan(source.CalendarSpan);
            //Days = source.Days;
            TotalMinutes = source.TotalMinutes;
        }
        #endregion

        /*
         * Методы
         */

        /*
         * Арифметика 
         */

        /*
         * Вывод данных 
         */

        #region public int? GetSubresource(SubResource subResource)
        /// <summary>
        /// Возвращает нужную часть от текущей наработки
        /// </summary>
        /// <param name="subResource">определяет, какую часть наработки показать</param>
        /// <returns>часы или циклы или дни в звисимости от значения переданного параметра</returns>
        public int? GetSubresource(LifelengthSubResource subResource)
        {
            switch (subResource)
            {
                case LifelengthSubResource.Minutes:
                    return TotalMinutes;
                case LifelengthSubResource.Hours:
                    return Hours;
                case LifelengthSubResource.Cycles:
                    return Cycles;
                case LifelengthSubResource.Calendar:
                    return Days;
                default:
                    return null;
            }
        }
        #endregion

        #region public string ToString(LifelengthSubResource subResource, string format)

        /// <summary>
        /// Возвращает нужную часть от текущей наработки в виде строки
        /// </summary>
        /// <param name="subResource">определяет, какую часть наработки показать</param>
        /// <param name="advanced">Выводит значение в расширенном формате (применяется в часам/минутам)</param>
        /// <param name="format"></param>
        /// <returns>часы или циклы или дни в виде строки в звисимости от значения переданного параметра</returns>
        public string ToString(LifelengthSubResource subResource, bool advanced = false, string format = "default")
        {
            switch (subResource)
            {
                case LifelengthSubResource.Minutes:
                    return ToHoursMinutesFormat(format == "default" ? "hrs" : format);
                case LifelengthSubResource.Hours:
                    return advanced
                        ? ToHoursMinutesFormat(format == "default" ? "hrs" : format)
                        : Hours + (format == "default" ? "FH" : format);
                case LifelengthSubResource.Cycles:
                    return Cycles + (format == "default" ? "FC" : format);
                case LifelengthSubResource.Calendar:
                    return Days + (format == "default" ? "d" : format);
                default:
                    return "";
            }
        }
        #endregion

        #region public override string ToString()
        /// <summary>
        /// Представляет наработку в виде строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string sh = Hours != null ? Hours + ((Cycles != null || Days != null) ? "FH/" : "FH") : "";
            string sc = Cycles != null ? Cycles + (Days != null ? "FC/" : "FC") : "";
            //string sd = Days != null ? Days + "d " : "";
            string sd = "";
            if (CalendarValue != null)
            {
                switch (CalendarSpan.CalendarType)
                {
                    case CalendarTypes.Years:
                        sd = CalendarValue + "Y ";
                        break;
                    case CalendarTypes.Months:
                        sd = CalendarValue + "MO ";
                        break;
                    case CalendarTypes.Days:
                        sd = CalendarValue + "d ";
                        break;
                }
            }

            return (sh + sc + sd).Trim();
        }

        #endregion

        #region public string ToStrings()
        /// <summary>
        /// Представляет наработку в виде строки (Каждый параметр выводится в новой строке)
        /// </summary>
        /// <returns></returns>
        public string ToStrings()
        {
            string sh = Hours != null && Hours != 0 ? Hours + "FH" : "";
            string sc = Cycles != null && Cycles != 0 ? Cycles + "FC" : "";
            //string sd = Days != null ? Days + "d " : "";
            string sd = "";
            if (CalendarValue != null)
            {
                switch (CalendarSpan.CalendarType)
                {
                    case CalendarTypes.Years:
                        sd = CalendarValue + "Y ";
                        break;
                    case CalendarTypes.Months:
                        sd = CalendarValue + "MO ";
                        break;
                    case CalendarTypes.Days:
                        sd = CalendarValue + "d ";
                        break;
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(sh))
                stringBuilder.AppendLine(sh);
            if (!string.IsNullOrEmpty(sc))
                stringBuilder.AppendLine(sc);
            if (!string.IsNullOrEmpty(sd))
                stringBuilder.AppendLine(sd);
            return stringBuilder.ToString();
        }

        #endregion

        #region public string ToString(DateTime approx)

        public string ToString(DateTime approx)
        {
            return approx.ToShortDateString() + "/" + ToString();
        }

        #endregion

        #region public string ToHoursMinutesFormat()

        /// <summary>
        /// Создается отображение наработки в виде: 123:12 hrs
        /// </summary>
        /// <returns></returns>
        public string ToHoursMinutesFormat(string hoursFormat = " hrs")
        {
            string res = "";
            if (TotalMinutes != null)
                res += Hours + ":" + (TotalMinutes - Hours * 60) + hoursFormat;
            return res;
        }

        #endregion

        #region public string ToHoursMinutesAndCyclesFormat(string hoursFormat = " hrs", string cyclesFormat = " cyc", string delimeter = "/")

        /// <summary>
        /// Создается отображение наработки в виде: 123:12 hrs/25 cyc
        /// </summary>
        /// <returns></returns>
        public string ToHoursMinutesAndCyclesFormat(string hoursFormat = " hrs", string cyclesFormat = " cyc", string delimeter = "/")
        {
            string res = "";
            if (TotalMinutes != null)
                res += Hours + ":" + (TotalMinutes - Hours * 60) + hoursFormat;
            if (Cycles != null)
            {
                if (TotalMinutes != null)
                    res += delimeter + Cycles + cyclesFormat;
                else
                    res += Cycles + cyclesFormat;
            }
            return res;
        }

        #endregion

        #region public string ToHoursMinutesAndCyclesStrings(string hoursFormat = " hrs", string cyclesFormat = " cyc")

        /// <summary>
        /// Создается отображение наработки в виде: 123:12 hrs 25 cyc (Каждый параметр выводится в новой строке)
        /// </summary>
        /// <returns></returns>
        public string ToHoursMinutesAndCyclesStrings(string hoursFormat = " hrs", string cyclesFormat = " cyc")
        {
            StringBuilder res = new StringBuilder();
            if (TotalMinutes != null && TotalMinutes != 0)
                res.AppendLine(Hours + ":" + (TotalMinutes - Hours * 60) + hoursFormat);
            if (Cycles != null && Cycles != 0)
            {
                if (TotalMinutes != null)
                    res.AppendLine(Cycles + cyclesFormat);
                else
                    res.AppendLine(Cycles + cyclesFormat);
            }
            return res.ToString();
        }

        #endregion

        #region public string ToHoursAndCyclesFormat(string hoursModifier = "h", string cyclesModifier = "c")

        /// <summary>
        /// Создается отображение наработки в виде ? h ? c
        /// </summary>
        /// <returns></returns>
        public string ToHoursAndCyclesFormat(string hoursModifier = "h", string cyclesModifier = "c")
        {
            string res = "";
            if (TotalMinutes != null)
                res += Hours + " " + hoursModifier;
            if (Cycles != null)
            {
                if (TotalMinutes != null)
                    res += " " + Cycles + " " + cyclesModifier;
                else
                    res += Cycles + " " + cyclesModifier;
            }
            return res;
        }

        #endregion

        #region public string ToRepeatIntervalsFormat()

        /// <summary>
        /// Создается отбражение наработки в виде "? h ? c ? y ? m ? d"
        /// </summary>
        /// <returns></returns>
        public string ToRepeatIntervalsFormat()
        {
            //string res = "";
            //if (TotalMinutes!=null && Hours != 0)
            //{
            //    res += Hours + " h |";
            //}

            //if (Cycles!=null && Cycles != 0)
            //{
            //    res += Cycles + " c |";
            //}

            //if (Days!=null)
            //{
            //    if (Days % 365 == 0)
            //        res += Days / 365 + "y";
            //    else if (Days % 360 == 0)
            //        res += Days / 360 + "y";
            //    else if (Days % 30 == 0)
            //        res += Days / 30 + "m";
            //    else
            //        res += Days + "d";
            //}

            return ToString();
        }

        #endregion
    }

    [Serializable]
    public class CalendarSpan
    {
        private int? _calendarValue;
        private CalendarTypes _calendarType;

        public int? CalendarValue
        {
            get { return _calendarValue; }
            set { _calendarValue = value; }
        }

        public CalendarTypes CalendarType
        {
            get { return _calendarType; }
            set { _calendarType = value; }
        }

        #region public Int32? Days

        public Int32? Days
        {
            get
            {
                if (_calendarValue == null)
                    return null;

                int value;
                switch (_calendarType)
                {
                    case CalendarTypes.Months:
                        value = (int)Math.Round(Convert.ToDouble(_calendarValue * 30.4375));
                        break;
                    case CalendarTypes.Years:
                        value = (int)Math.Round(Convert.ToDouble(_calendarValue * 365.25));
                        break;
                    default:
                        value = Convert.ToInt32(_calendarValue);
                        break;
                }
                return value;
            }
        }
        #endregion

        #region public Int32? Months

        public Int32? Months
        {
            get
            {
                if (_calendarValue == null)
                    return null;

                int value;
                switch (_calendarType)
                {
                    case CalendarTypes.Months:
                        value = Convert.ToInt32(_calendarValue);
                        break;
                    case CalendarTypes.Years:
                        value = Convert.ToInt32(_calendarValue * 12);
                        break;
                    default:
                        value = (int)Math.Round(Convert.ToDouble(_calendarValue * 30.4375));
                        break;
                }
                return value;
            }
        }
        #endregion

        #region public Int32? Years

        public Int32? Years
        {
            get
            {
                if (_calendarValue == null)
                    return null;

                int value;
                switch (_calendarType)
                {
                    case CalendarTypes.Months:
                        value = (int)Math.Round(Convert.ToDouble(_calendarValue / 12));
                        break;
                    case CalendarTypes.Years:
                        value = Convert.ToInt32(_calendarValue);
                        break;
                    default:
                        value = (int)Math.Round(Convert.ToDouble(_calendarValue / 365.25));
                        break;
                }
                return value;
            }
        }
        #endregion

        #region public CalendarSpan()

        public CalendarSpan()
        {
            _calendarValue = null;
            _calendarType = CalendarTypes.Days;
        }
        #endregion

        #region public CalendarSpan(int? calandarValue, CalendarTypes calendarType)

        public CalendarSpan(int? calandarValue, CalendarTypes calendarType)
        {
            _calendarValue = calandarValue;
            _calendarType = calendarType;
        }
        #endregion

        #region public CalendarSpan(CalendarSpan source)
        public CalendarSpan(CalendarSpan source)
        {
            if (source != null)
            {
                _calendarValue = source.CalendarValue;
                _calendarType = source.CalendarType;
            }
            else
            {
                _calendarValue = null;
                _calendarType = CalendarTypes.Days;
            }
        }
        #endregion

        /*
         * Operator
         */

        #region public static bool operator > (CalendarSpan a, CalendarSpan b)

        public static bool operator >(CalendarSpan a, CalendarSpan b)
        {
            if (a == null || a.CalendarValue == null)
                return false;
            if (b == null || b.CalendarValue == null)
                return true;
            if (a.CalendarType == b.CalendarType)
            {
                return a.CalendarValue > b.CalendarValue;
            }

            double aValue, bValue;

            switch (a.CalendarType)
            {
                case CalendarTypes.Months:
                    aValue = Math.Round(System.Convert.ToDouble(a.CalendarValue * 30.4375));
                    break;
                case CalendarTypes.Years:
                    aValue = Math.Round(System.Convert.ToDouble(a.CalendarValue * 365.25));
                    break;
                default:
                    aValue = System.Convert.ToDouble(a.CalendarValue);
                    break;
            }

            switch (b.CalendarType)
            {
                case CalendarTypes.Months:
                    bValue = Math.Round(System.Convert.ToDouble(b.CalendarValue * 30.4375));
                    break;
                case CalendarTypes.Years:
                    bValue = Math.Round(System.Convert.ToDouble(b.CalendarValue * 365.25));
                    break;
                default:
                    bValue = System.Convert.ToDouble(b.CalendarValue);
                    break;
            }

            return aValue > bValue;
        }
        #endregion

        #region public static bool operator < (CalendarSpan a, CalendarSpan b)

        public static bool operator <(CalendarSpan a, CalendarSpan b)
        {
            if (b == null || b.CalendarValue == null)
                return false;
            if (a == null || a.CalendarValue == null)
                return true;
            if (a.CalendarType == b.CalendarType)
            {
                return a.CalendarValue < b.CalendarValue;
            }

            double aValue, bValue;

            if (a.CalendarType == CalendarTypes.Months)
                aValue = Math.Round(System.Convert.ToDouble(a.CalendarValue * 30.4375));
            else if (a.CalendarType == CalendarTypes.Years)
                aValue = Math.Round(System.Convert.ToDouble(a.CalendarValue * 365.25));
            else aValue = System.Convert.ToDouble(a.CalendarValue);

            if (b.CalendarType == CalendarTypes.Months)
                bValue = Math.Round(System.Convert.ToDouble(b.CalendarValue * 30.4375));
            else if (b.CalendarType == CalendarTypes.Years)
                bValue = Math.Round(System.Convert.ToDouble(b.CalendarValue * 365.25));
            else bValue = System.Convert.ToDouble(b.CalendarValue);

            return aValue < bValue;
        }
        #endregion
    }
}