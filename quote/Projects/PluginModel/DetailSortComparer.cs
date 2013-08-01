namespace Model.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    public class DetailSortComparer : IComparer<Detail>
    {
        private PropertyDescriptor propDesc = null;
        
        private ListSortDirection direction = ListSortDirection.Ascending;
        
        public DetailSortComparer(PropertyDescriptor propDescriptor, ListSortDirection direction)
        {
            this.propDesc = propDescriptor;
            this.direction = direction;
        }
        
        public int Compare(Detail x, Detail y)
        {
            int retValue = 0;
            object leftValue = this.propDesc.GetValue(x);
            object rightValue = this.propDesc.GetValue(y);
            retValue = CompareValues(leftValue, rightValue, this.direction);
            return retValue;
        }
        
        private static bool IsNumeric(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return true;
            }
            
            return false;
        }

        private static int CompareValues(object x, object y, ListSortDirection direction)
        {
            int retValue = 0;
            
            if (IsNumeric(x.ToString()) & IsNumeric(y.ToString())) 
            {
                double left = Convert.ToDouble(x, CultureInfo.CurrentCulture);
                double right = Convert.ToDouble(y, CultureInfo.CurrentCulture);
                
                retValue = left.CompareTo(right);
            } 
            else if (x == null) 
            {
                retValue = 0;
            } 
            else if (y == null) 
            {
                retValue = 0;
            } 
            else 
            {
                retValue = string.Compare(x.ToString(), y.ToString(), true, CultureInfo.CurrentCulture);
            }
            
            if (direction == ListSortDirection.Descending) 
            {
                retValue *= -1;
            }
            
            return retValue;
        }
    }
}