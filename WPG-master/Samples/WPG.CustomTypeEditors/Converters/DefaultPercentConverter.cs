﻿using System;
using System.Windows.Data;

namespace Sample4.CustomTypeEditors
{
  public class DefaultPercentConverter : IValueConverter
  {
    #region IValueConverter Member



    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {

      if (value != null)
      {

        string val = value.ToString();

        val = val.Replace(".", ",");

        decimal dec = Decimal.Parse(val);



        //Komma verschieben

        int shiftFactor = (int)Math.Pow(10, 2);

        decimal tmp = dec * shiftFactor;



        //Runden: 0.5 addieren/subtrahieren und dann Nachkommastellen abschneiden

        decimal diff = (tmp >= 0 ? 0.5m : -0.5m);

        tmp = (long)(tmp + diff);



        //Komma wieder verschieben

        dec = (tmp / shiftFactor);

        val = dec.ToString();



        int index = val.IndexOf(",");

        if (index == -1)

          val += ",00";



        val += "%";

        return val;

      }

      return "0,00%";

    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value != null)
      {
        string val = value.ToString();
        val = val.Replace("%", "");
        //decimal dec = Decimal.Parse(val);
        //return dec;
      }
      return value;
    }

    #endregion


  }
}
