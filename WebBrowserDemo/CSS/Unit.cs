using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CascadingStyleSheets
{
    public enum UnitType
    {
        Pixel = 0,
        Mm = 1,
        Cm = 2,
        Inch = 3,
        Point = 4,
        Pica = 5,
        Em = 6,
        Ex = 7, 
        Percentage = 8
    }


    public class Unit
    {
        private static readonly string[] fontSizeUnits = new string[] { "px", "mm", "cm", "in", "pt", "pc", "em", "ex", "%" };

        private UnitType type = UnitType.Pixel;
        private double value = 0;

        public static Unit Parse(string value)
        {
            if (value == null || value == "")
                throw new ArgumentException("Value is empty in Unit.cs");

            value.Trim();

            char current;
            StringBuilder number = new StringBuilder();
            StringBuilder type = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                current = value[i];
                if (char.IsDigit(current) || current == '.')
                {
                    number.Append(current);
                }
                else
                {
                    type.Append(current);
                }
            }
            double numResult = 8;
            string numStr = number.ToString();
            if (numStr.StartsWith(".")) numStr = '0' + numStr;
            if (!double.TryParse(numStr, out numResult))
            {
                Logger.RecordWarning("Unit value invalid in Unit.cs: " + numStr);
            }

            string typeResult = type.ToString().Trim();
            for (int i = 0; i < fontSizeUnits.Length; i++)
            {
                if (typeResult == fontSizeUnits[i])
                {
                    UnitType typeE = (UnitType)i;
                    bool isPercent = typeE == UnitType.Percentage;

                    return new Unit(isPercent ? numResult * 0.01 : numResult, typeE);
                }
            }
            return new Unit(numResult, UnitType.Pixel);
        }
         
        public Unit(double value, UnitType type)
        {
            if ((value < -32768.0) || (value > 32767.0))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            if (type == UnitType.Pixel)
            {
                this.value = (int) value;
            }
            else
            {
                this.value = value;
            }
            this.type = type;
        }

        public UnitType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
 
        public double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
 
        public bool IsRelative
        {
            get
            {
                return
                    type == UnitType.Percentage ||
                    type == UnitType.Em ||
                    type == UnitType.Ex;
            }
        }

        public override string ToString()
        {
            return value.ToString() + fontSizeUnits[(int)type];
        }

    }
}
