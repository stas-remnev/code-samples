namespace Nsd.Service.CorpDb.Services.Companies.Model
{
    public class CompanyInfoServiceModel
    {
        /// <summary>
        /// Код Эмитента
        /// </summary>
        public string IssuerCode { get; set; }
    }

    public class CompanyFullInfoServiceModel
    {
        /// <summary>
        /// Код НРД организации
        /// </summary>
        public string NdcCode { get; set; }

        /// <summary>
        /// Код Эмитента
        /// </summary>
        public string IssuerCode { get; set; }

        /// <summary>
        /// Код сектора экономики
        /// </summary>
        public string ForeignEconomySectorCode { get; set; }
    }
    public class CompanyBySearchStringInfoServiceModel
    {

        /// <summary>
        /// ID компании
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Код НРД организации
        /// </summary>
        public string NdcCmpCode { get; set; }
    }

    public class CompanyBaseIssuerInfoServiceModel
    {
        /// <summary>
        /// Код НРД депозитарной расписки
        /// </summary>
        public string NdcCode { get; set; }

        /// <summary>
        /// Номер гос.регистрации базовой ц. б
        /// </summary>
        public string StateRegNumber { get; set; }

        /// <summary>
        /// Код НРД эмитента базовой ц. б.
        /// </summary>
        public string CmpNdcCode { get; set; }

        /// <summary>
        /// Признак эмитента базовой ц.б
        /// </summary>
        public string IsBaseResident { get; set; }

        /// <summary>
        /// Наименование эмитента базовой ц. б.
        /// </summary>
        public string BaseResidentName { get; set; }

        /// <summary>
        /// ИНН эмитента базовой ц. б.  (для резидента)
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП эмитента базовой ц. б.
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН эмитента базовой ц. б.
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// Код ОКСМ страны эмитента базовой ц. б.
        /// </summary>
        public string OksmCode { get; set; }
        
        /// <summary>
        /// TIN / номер налоговой регистрации / регистрационный номер в стране регистрации эмитента базовой ц. б. (для нерезидента)
        /// </summary>
        public string Tin { get; set; }

        /// <summary>
        /// Код сектора экономики
        /// </summary>
        public string ForeignEconomySectorCode { get; set; }
    }
}
