using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace FTEReader.Converter
{
    //Boolean?类型与Bool类型的转换器
    public class converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Boolean? var = (Boolean?)value;
            bool result = (bool)var;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool var = (bool)value;
            Boolean? result = (Boolean?)var;
            return result;
        }
    }
}
