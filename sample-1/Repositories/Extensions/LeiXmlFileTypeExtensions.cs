using Nsd.Repository.Ef.Model.Entities;
using System;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiXmlFileTypeExtensions
    {
        private const string unsupportedXmlFileTypeExceptionMessage = "Неподдерживаемый формат XML файла для выгрузки LEI";

        public static string ToFriendlyString(this LeiXmlFileType leiXmlFileType)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Full:
                    return "FULL";
                case LeiXmlFileType.Delta:
                    return "DELTA";
                case LeiXmlFileType.Public:
                    return "RR_PUBLIC";
                case LeiXmlFileType.Private:
                    return "RR_PRIVATE";
                case LeiXmlFileType.NonPublic:
                    return "RR_NON_PUBLIC";
                case LeiXmlFileType.RepEx:
                    return "REPEX";
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }

        public static string ToDisplayString(this LeiXmlFileType leiXmlFileType)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Full:
                    return "FULL";
                case LeiXmlFileType.Delta:
                    return "DELTA";
                case LeiXmlFileType.Public:
                    return "RR-CDF PUBLIC";
                case LeiXmlFileType.Private:
                    return "RR-CDF PRIVATE";
                case LeiXmlFileType.NonPublic:
                    return "RR-CDF NON_PUBLIC";
                case LeiXmlFileType.RepEx:
                    return "REPEX";
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }

        public static string GetFileFormat(this LeiXmlFileType leiXmlFileType)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Full:
                    return "CDF FULL";
                case LeiXmlFileType.Delta:
                    return "DELTA";
                case LeiXmlFileType.Public:
                    return "LEVEL2: RR-CDF PUBLIC";
                case LeiXmlFileType.Private:
                    return "LEVEL2: RR-CDF PRIVATE";
                case LeiXmlFileType.NonPublic:
                    return "LEVEL2: RR-CDF NON_PUBLIC";
                case LeiXmlFileType.RepEx:
                    return "LEVEL2: REPORT EXCEPTIONS";
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }

        public static string GetFilePostfix(this LeiXmlFileType leiXmlFileType)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Full:
                    return "FULL";
                case LeiXmlFileType.Delta:
                    return "DELTA";
                case LeiXmlFileType.Public:
                    return "PUBLIC";
                case LeiXmlFileType.Private:
                    return "PRIVATE";
                case LeiXmlFileType.NonPublic:
                    return "NON_PUBLIC";
                case LeiXmlFileType.RepEx:
                    return "REPEX";
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }

        public static string GetUrlPostfix(this LeiXmlFileType leiXmlFileType, LeiVersion leiVersion)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Full:
                    {
                        switch (leiVersion)
                        {
                            case LeiVersion.One: return "/cdf-full-files/";
                            case LeiVersion.Two: return "/lei2-full-files/";
                            default:
                                throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
                        }
                    };
                case LeiXmlFileType.Delta: return "/cdf-full-files/";
                case LeiXmlFileType.Public: return "/rr-full-files/";
                case LeiXmlFileType.Private: return "/rr-internal-full-files/";
                case LeiXmlFileType.NonPublic: return "/pni-full-files/";
                case LeiXmlFileType.RepEx: return "/rr-exceptions-full-files/";
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }

        public static string GetFileContent(this LeiXmlFileType leiXmlFileType)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Full:
                    return "LOU_FULL_PUBLISHED";
                case LeiXmlFileType.Delta:
                    return "LOU_DELTA_PUBLISHED";
                case LeiXmlFileType.Public:
                case LeiXmlFileType.RepEx:
                    return "LOU_FULL_PUBLISHED";
                case LeiXmlFileType.Private:
                case LeiXmlFileType.NonPublic:
                    return "LOU_FULL_INTERNAL";
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }

        /*public ILeiXmlService GetXmlManager(this LeiXmlFileType leiXmlFileType, Tokenizer500 tokenizer, string leiOriginator)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Public:
                case LeiXmlFileType.Private:
                    return new LeiLevelPublicPrivateXmlManager(tokenizer, leiOriginator, updateProgressAction);
                case LeiXmlFileType.RepEx:
                    return new LeiLevel2RepexXmlManager(tokenizer, leiOriginator, updateProgressAction);
                case LeiXmlFileType.NonPublic:
                    return new LeiLevel2NonPublicXmlManager(tokenizer, leiOriginator, updateProgressAction);
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }*/

        /*public static XDocument GetXsdScheme(this LeiXmlFileType leiXmlFileType)
        {
            switch (leiXmlFileType)
            {
                case LeiXmlFileType.Public:
                case LeiXmlFileType.Private:
                    return XDocument.Parse(LeiResource._2017_03_16_RR_CDF_1_0_XML_Schema_v1_1);
                case LeiXmlFileType.NonPublic:
                    return XDocument.Parse(LeiResource._2017_03_16_Parent_Reference_Data_Format_1_1);
                case LeiXmlFileType.RepEx:
                    return XDocument.Parse(LeiResource._2017_03_16_Reporting_Exceptions_Format_1_0_XML_Schema_v1_1);
                default:
                    throw new ArgumentException(unsupportedXmlFileTypeExceptionMessage);
            }
        }*/
    }
}
