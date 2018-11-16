using System;
using System.Globalization;
using System.Windows.Data;

namespace MaintenancePlatform.ViewModels.Acquisition
{
    /// <summary>
    /// 楼层编号转换楼层名称
    /// <remarks>AndrewChien 2013-8-21 1:31:08</remarks>
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class ConvertToFloorName : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //var floorid = value.ToString();
                //var collection =
                //    ServiceHelper.ServiceClient.SelectAllFloor().Where(
                //        r => r.FloorID.ToString(CultureInfo.InvariantCulture) == floorid).ConvertTo();
                //var floorname = "";
                //if (collection.Count > 0)
                //{
                //    floorname = collection[0].FloorName;
                //}
                //return floorname;

                return "";
            }
            catch
            {
                return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value;
        }

        #endregion
    }

    /// <summary>
    /// 楼层编号转换楼层名称
    /// <remarks>AndrewChien 2013-8-21 1:31:08</remarks>
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class ConvertToBuildingName : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //var buildingid = value.ToString();
                //var collection =
                //    ServiceHelper.ServiceClient.SelectAllBuilding().Where(
                //        r => r.ID.ToString(CultureInfo.InvariantCulture) == buildingid).ConvertTo();
                //var buildingname = "";
                //if (collection.Count > 0)
                //{
                //    buildingname = collection[0].BuildingName;
                //}
                //return buildingname;

                return "";
            }
            catch
            {
                return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value;
        }

        #endregion
    }

    /// <summary>
    /// 对象编号转换对象名称
    /// <remarks>AndrewChien 2013-8-21 1:31:08</remarks>
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class ConvertDXBHToDXMC : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //var dxbh = value.ToString();
                //var collection =
                //    ServiceHelper.ServiceClient.SelectAllLOCATE_JKDX("", "").Where(
                //        r => r.DXBH.ToString(CultureInfo.InvariantCulture) == dxbh).ConvertTo();
                //var dxmc = "";
                //if (collection.Count > 0)
                //{
                //    dxmc = collection[0].DXMC;
                //}
                //return dxmc;

                return "";
            }
            catch
            {
                return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value;
        }

        #endregion
    }

    /// <summary>
    /// 外出登记做返回时间
    /// <remarks>AndrewChien 2013-8-21 1:31:08</remarks>
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class WCDJMakeReturnTime : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return DateTime.Parse("1900-1-1") == DateTime.Parse(value.ToString()) ? "" : value.ToString();
            }
            catch
            {
                return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return value;
        }

        #endregion
    }

    /// <summary>
    /// 根据ID和当前时间获得该ID提醒图片
    /// <remarks>AndrewChien 2013-8-21 1:31:08</remarks>
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class WCDJMakeAlertIMG : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                //var id = value.ToString();
                //var collection =
                //    ServiceHelper.ServiceClient.GetAllWCDJNew("", "1").Where(
                //        r => r.ID.ToString(CultureInfo.InvariantCulture) == id).ConvertTo();
                //if (collection.Count > 0 && collection[0].FHSJ == DateTime.Parse("1900-1-1") && collection[0].SJE < DateTime.Now)
                //{
                //    return "../../image/alarm.gif";
                //}
                //return "";

                return "";
            }
            catch
            {
                return "";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }

    [ValueConversion(typeof(System.Enum), typeof(string))]
    public class ConvertForGYLBKind : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                //return ServiceHelper.ServiceClient.GetGYLBByID(value.ToString())[0].Name;
                return "";
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return value;
        }

        #endregion
    }

    [ValueConversion(typeof(System.Enum), typeof(string))]
    public class ConvertForGYLBBelong : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                //return ServiceHelper.ServiceClient.GetGYLBSonByID(value.ToString())[0].Name;
                return "";
            }
            catch
            {
                return "";//包括value==0的情况
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return value;
        }

        #endregion
    }


    [ValueConversion(typeof(System.Enum), typeof(string))]
    public class ConvertForGYLBAvailable : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 1:
                    return "是";
                case 0:
                    return "否";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            return value;
        }

        #endregion
    }
}
