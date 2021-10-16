using Nsd.Repository.Ef.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiVersionExtensions
    {
        private const string unsupportedVersionExceptionMessage = "Неподдерживаемая версия кодов LEI";

        public static string ToNumberString(this LeiVersion leiVersion)
        {
            switch (leiVersion)
            {
                case LeiVersion.One:
                case LeiVersion.OneLevel2:
                    return "1.0";
                case LeiVersion.Two:
                    return "2.0";
                default:
                    throw new ArgumentException(unsupportedVersionExceptionMessage);
            }
        }

        public static string GetFileNameSuffix(this LeiVersion leiVersion)
        {
            switch (leiVersion)
            {
                case LeiVersion.One:
                case LeiVersion.OneLevel2:
                    return string.Empty;
                case LeiVersion.Two:
                    return "_CDF2_0";
                default:
                    throw new ArgumentException(unsupportedVersionExceptionMessage);
            }
        }
    }
}
