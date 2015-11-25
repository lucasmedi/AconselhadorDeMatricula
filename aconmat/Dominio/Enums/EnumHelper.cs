﻿using System;
using System.ComponentModel;

namespace Dominio.Enums
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }
    }
}