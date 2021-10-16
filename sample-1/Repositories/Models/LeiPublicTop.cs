using LinqToDB.Mapping;
using Nsd.Repository.Ef.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Nsd.Repository.Ef.Model.Entities;

namespace Nsd.Repository.Ef.Repositories.Lei.Models
{
    [AddToDbContext]
    public class LeiPublicTop
    {
        [DisplayName(@"Идентификатор кода LEI")]
        [Column("lei_code_id")]
        public int LeiCodeId { get; set; }

        [DisplayName(@"Не выгружать")]
        [Column("upload_stop")]
        public bool UploadStop { get; set; }

        [DisplayName(@"Отправлено в GLEIF на PreCheck")]
        [Column("sent_to_precheck_in_gleif")]
        public bool SentToPreCheckInGLEIF { get; set; }

        [DisplayName(@"Получен авто-акцепт")]
        [Column("auto_accept_received")]
        public bool AutoAcceptReceived { get; set; }

        [DisplayName(@"Акцепт проставлен пользователем")]
        [Column("accept_applied_by_user")]
        public bool AcceptAppliedByUser { get; set; }

        [Nullable]
        [DisplayName(@"Причина появления в журнале")]
        [Column("reason")]
        public string Reason { get; set; }

        [Nullable]
        [DisplayName(@"Статус кода")]
        [Column("lei_code_status")]
        public string LeiCodeStatus { get; set; }

        [Nullable]
        [DisplayName(@"Значение кода LEI")]
        [Column("lei_code")]
        public string LeiCode { get; set; }

        [DisplayName(@"Company_Id")]
        [Column("company_id")]
        public int CompanyId { get; set; }

        [DisplayName(@"Дата присвоения кода LEI")]
        [Column("lei_assignment_date")]
        public DateTime? LeiAssignmentDate { get; set; }

        [DisplayName(@"Дата обновления кода LEI")]
        [Column("lei_update_date")]
        public DateTime? LeiUpdateDate { get; set; }

        [DisplayName(@"Дата последней верификации")]
        [Column("lei_verification_date")]
        public DateTime? LeiVerificationDate { get; set; }

        [DisplayName(@"Дата следующей верификации")]
        [Column("lei_next_recertification_date")]
        public DateTime? LeiNextRecertificationDate { get; set; }

        [Nullable]
        [DisplayName(@"Статус кода")]
        [Column("registration_status")]
        public string RegistrationStatus { get; set; }

        [Column("rus_company")]
        [DisplayName(@"Организация - резидент")]
        public bool RusCompany { get; set; }

        [Column("is_inactive_cmp")]
        [DisplayName(@"Признак ""НЕДЕЙСТВУЮЩАЯ""")]
        public bool IsInactiveCmp { get; set; }

        [Nullable]
        [DisplayName(@"Состояние организации")]
        [Column("is_inactive_cmp_str")]
        public string IsInactiveCmpStr { get; set; }

        [Column("lei_applicant")]
        [DisplayName(@"Заявитель кода LEI")]
        public bool LeiApplicant { get; set; }

        [Column("investment_fund")]
        public bool InvestmentFund { get; set; }

        [Nullable]
        [DisplayName(@"Полное наименование")]
        [Column("full_name")]
        public string FullName { get; set; }

        [Nullable]
        [DisplayName(@"Краткое наименование")]
        [Column("short_name")]
        public string ShortName { get; set; }

        [Nullable]
        [DisplayName(@"Полное наименование на английском")]
        [Column("full_name_en")]
        public string FullNameEn { get; set; }

        [Nullable]
        [DisplayName(@"Краткое наименование на английском")]
        [Column("short_name_en")]
        public string ShortNameEn { get; set; }

        [Nullable]
        [DisplayName(@"Официальное наименование организации")]
        [Column("legal_name")]
        public string LegalName { get; set; }

        [Nullable]
        [DisplayName(@"Язык официального наименования организации")]
        [Column("legal_name_language_code")]
        public string LegalNameLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Предыдущее официальное наименование организации")]
        [Column("prev_legal_name")]
        public string PrevLegalName { get; set; }

        [Nullable]
        [DisplayName(@"Язык предыдущего официального наименования организации")]
        [Column("prev_legal_name_language_code")]
        public string PrevLegalNameLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Адрес основного общества")]
        [Column("basic_society_address")]
        public string BasicSocietyAddress { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса основного общества")]
        [Column("basic_society_country_code")]
        public string BasicSocietyCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса основного общества")]
        [Column("basic_society_city")]
        public string BasicSocietyCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса основного общества")]
        [Column("basic_society_post_index")]
        public string BasicSocietyPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса основного общества")]
        [Column("basic_society_region_code")]
        public string BasicSocietyRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса основного общества")]
        [Column("basic_society_language_code")]
        public string BasicSocietyLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества, Строка1")]
        [Column("basic_society_line1")]
        public string BasicSocietyLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества, Строка2")]
        [Column("basic_society_line2")]
        public string BasicSocietyLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества, Строка3")]
        [Column("basic_society_line3")]
        public string BasicSocietyLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества, Строка4")]
        [Column("basic_society_line4")]
        public string BasicSocietyLine4 { get; set; }

        [Nullable]
        [DisplayName("Адрес основного общества (ин.яз.)")]
        [Column("basic_society_en_address")]
        public string BasicSocietyEnAddress { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса основного общества (ин.яз)")]
        [Column("basic_society_en_country_code")]
        public string BasicSocietyEnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса основного общества (ин.яз)")]
        [Column("basic_society_en_city")]
        public string BasicSocietyEnCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса основного общества (ин.яз)")]
        [Column("basic_society_en_post_index")]
        public string BasicSocietyEnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса основного общества (ин.яз)")]
        [Column("basic_society_en_region_code")]
        public string BasicSocietyEnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса основного общества (ин.яз)")]
        [Column("basic_society_en_language_code")]
        public string BasicSocietyEnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества (ин.яз), Строка1")]
        [Column("basic_society_en_line1")]
        public string BasicSocietyEnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества (ин.яз), Строка2")]
        [Column("basic_society_en_line2")]
        public string BasicSocietyEnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества (ин.яз), Строка3")]
        [Column("basic_society_en_line3")]
        public string BasicSocietyEnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса основного общества (ин.яз), Строка4")]
        [Column("basic_society_en_line4")]
        public string BasicSocietyEnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Другое наименование организации")]
        [Column("other_entity_name")]
        public string OtherEntityName { get; set; }

        [DisplayName(@"Признак транслитерированности other_name")]
        [Column("transliterated")]
        public bool Transliterated { get; set; }

        [Nullable]
        [DisplayName(@"Адрес местонахождения (по анкете)")]
        [Column("place_addr")]
        public string PlaceAddr { get; set; }

        [Nullable]

        [DisplayName(@"Город адреса местонахождения (по анкете)")]
        [Column("place_addr_city")]
        public string PlaceAddrCity { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса местонахождения (по анкете)")]
        [Column("place_addr_country_code")]
        public string PlaceAddrCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса местонахождения (по анкете)")]
        [Column("place_addr_post_index")]
        public string PlaceAddrPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Почтовый индекс для тега HeadquartersAddress")]
        [Column("head_quarters_address")]
        public string HeadQuartersAddress { get; set; }

        [Nullable]
        [DisplayName(@"Почтовый индекс для тега LegalAddress")]
        [Column("legal_address")]
        public string LegalAddress { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса местонахождения (по анкете)")]
        [Column("place_addr_region_code")]
        public string PlaceAddrRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса местонахождения (по анкете)")]
        [Column("place_addr_language_code")]
        public string PlaceAddrLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (по анкете), Строка1")]
        [Column("place_addr_line1")]
        public string PlaceAddrLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (по анкете), Строка2")]
        [Column("place_addr_line2")]
        public string PlaceAddrLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (по анкете), Строка3")]
        [Column("place_addr_line3")]
        public string PlaceAddrLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (по анкете), Строка4")]
        [Column("place_addr_line4")]
        public string PlaceAddrLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Адрес местонахождения (ин.яз.)")]
        [Column("place_addr_en")]
        public string PlaceAddrEn { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса местонахождения (ин.яз.)")]
        [Column("place_addr_en_city")]
        public string PlaceAddrEnCity { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса местонахождения (ин.яз.)")]
        [Column("place_addr_en_country_code")]
        public string PlaceAddrEnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса местонахождения (ин.яз.)")]
        [Column("place_addr_en_post_index")]
        public string PlaceAddrEnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса местонахождения (ин.яз.)")]
        [Column("place_addr_en_region_code")]
        public string PlaceAddrEnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса местонахождения (ин.яз.)")]
        [Column("place_addr_en_language_code")]
        public string PlaceAddrEnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка1")]
        [Column("place_addr_en_line1")]
        public string PlaceAddrEnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка2")]
        [Column("place_addr_en_line2")]
        public string PlaceAddrEnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка3")]
        [Column("place_addr_en_line3")]
        public string PlaceAddrEnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка4")]
        [Column("place_addr_en_line4")]
        public string PlaceAddrEnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Адрес почтовый (по анкете)")]
        [Column("basic_society_or_post_address")]
        public string BasicSocietyOrPostAddress { get; set; }

        [Nullable]
        [DisplayName(@"Адрес почтовый (по анкете) без ""автозамены"" в случае его отсутствия")]
        [Column("post_address")]
        public string PostAddress { get; set; }

        [Nullable]
        [Column("mail_routing_address")]
        public string MailRoutingAddress { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса почтового (по анкете)")]
        [Column("post_address_country_code")]
        public string PostAddressCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса почтового (по анкете)")]
        [Column("post_address_city")]
        public string PostAddressCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса почтового (по анкете)")]
        [Column("post_address_post_index")]
        public string PostAddressPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса почтового (по анкете)")]
        [Column("post_address_region_code")]
        public string PostAddressRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса почтового (по анкете)")]
        [Column("post_address_language_code")]
        public string PostAddressLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (по анкете), Строка1")]
        [Column("post_address_line1")]
        public string PostAddressLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (по анкете), Строка2")]
        [Column("post_address_line2")]
        public string PostAddressLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (по анкете), Строка3")]
        [Column("post_address_line3")]
        public string PostAddressLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (по анкете), Строка4")]
        [Column("post_address_line4")]
        public string PostAddressLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Адрес почтовый (ин.яз.)")]
        [Column("basic_society_or_post_address_en")]
        public string BasicSocietyOrPostAddressEn { get; set; }

        [Nullable]

        [DisplayName(@"Город адреса почтового (ин.яз.)")]
        [Column("basic_society_or_post_address_en_city")]
        public string BasicSocietyOrPostAddressEnCity { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса почтового (ин.яз.)")]
        [Column("basic_society_or_post_address_en_country_code")]
        public string BasicSocietyOrPostAddressEnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса почтового (ин.яз.)")]
        [Column("basic_society_or_post_address_en_post_index")]
        public string BasicSocietyOrPostAddressEnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса почтового (ин.яз.)")]
        [Column("basic_society_or_post_address_en_region_code")]
        public string BasicSocietyOrPostAddressEnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса почтового (ин.яз.)")]
        [Column("basic_society_or_post_address_en_language_code")]
        public string BasicSocietyOrPostAddressEnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз.), Строка1")]
        [Column("basic_society_or_post_address_en_line1")]
        public string BasicSocietyOrPostAddressEnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз.), Строка2")]
        [Column("basic_society_or_post_address_en_line2")]
        public string BasicSocietyOrPostAddressEnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз.), Строка3")]
        [Column("basic_society_or_post_address_en_line3")]
        public string BasicSocietyOrPostAddressEnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз.), Строка4")]
        [Column("basic_society_or_post_address_en_line4")]
        public string BasicSocietyOrPostAddressEnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Адрес почтовый (ин.яз.) без ""автозамены"" в случае его отсутствия")]
        [Column("post_address_en")]
        public string PostAddressEn { get; set; }

        [Nullable]
        [Column("mail_routing_address_en")]
        public string MailRoutingAddressEn { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса почтового (ин.яз)")]
        [Column("post_address_en_country_code")]
        public string PostAddressEnCountryCode { get; set; }

        [Nullable]

        [DisplayName(@"Город адреса почтового (ин.яз)")]
        [Column("post_address_en_city")]
        public string PostAddressEnCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса почтового (ин.яз)")]
        [Column("post_address_en_post_index")]
        public string PostAddressEnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса почтового (ин.яз)")]
        [Column("post_address_en_region_code")]
        public string PostAddressEnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса почтового (ин.яз)")]
        [Column("post_address_en_language_code")]
        public string PostAddressEnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз), Строка1")]
        [Column("post_address_en_line1")]
        public string PostAddressEnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз), Строка2")]
        [Column("post_address_en_line2")]
        public string PostAddressEnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз), Строка3")]
        [Column("post_address_en_line3")]
        public string PostAddressEnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового (ин.яз), Строка4")]
        [Column("post_address_en_line4")]
        public string PostAddressEnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Организационно-правовая форма")]
        [Column("entity_legal_form")]
        public string EntityLegalForm { get; set; }

        [Nullable]

        [DisplayName(@"ОГРН (№гос.регистрации / №правил ПИФ)")]
        [Column("business_reg_id")]
        public string BusinessRegId { get; set; }

        [Nullable]
        [DisplayName(@"Мнемоника события прекращения действия")]
        [Column("event_type_mn")]
        public string EventTypeMn { get; set; }

        [Nullable]
        [DisplayName(@"Реестр регистрации")]
        [Column("registry_code")]
        public string RegistryCode { get; set; }

        [DisplayName(@"Дата прекращения действия организации")]
        [Column("expiration_date")]
        public DateTime? ExpirationDate { get; set; }

        [Nullable]
        [DisplayName(@"Причина прекращения действия организации")]
        [Column("entity_expiration_reason")]
        public string EntityExpirationReason { get; set; }

        [DisplayName(@"Для формата LEI 1.0")]
        [Column("lei1")]
        public bool Lei1 { get; set; }

        [DisplayName(@"Для формата LEI 2.0")]
        [Column("lei2")]
        public bool Lei2 { get; set; }

        [Column("successor_id")]
        [DisplayName(@"Идентификатор организации - правопреемника")]
        public int? SuccessorId { get; set; }

        [Nullable]
        [DisplayName(@"Код LEI правопреемника")]
        [Column("successor_lei_code")]
        public string SuccessorLeiCode { get; set; }

        [Nullable]
        [DisplayName(@"Название правопреемника")]
        [Column("successor_name")]
        public string SuccessorName { get; set; }

        [Nullable]
        [DisplayName(@"Иностранное название правопреемника")]
        [Column("successor_other_name")]
        public string SuccessorOtherName { get; set; }

        [Nullable]
        [DisplayName(@"Страна (по месту нахождения)")]
        [Column("legal_jurisdiction")]
        public string LegalJurisdiction { get; set; }

        [Nullable]        [DisplayName(@"Код НРД")]
        [Column("business_registry_name")]
        public string BusinessRegistryName { get; set; }

        [Nullable]
        [Column("managing_lou")]
        public string ManagingLou { get; set; }

        [Nullable]
        [DisplayName(@"Адрес почтовый LEI дополнительный")]
        [Column("post_address2_en")]
        public string PostAddress2En { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса почтового LEI дополнительного")]
        [Column("post_address2_en_country_code")]
        public string PostAddress2EnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса почтового LEI дополнительного")]
        [Column("post_address2_en_city")]
        public string PostAddress2EnCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса почтового LEI дополнительного")]
        [Column("post_address2_en_post_index")]
        public string PostAddress2EnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса почтового LEI дополнительного")]
        [Column("post_address2_en_region_code")]
        public string PostAddress2EnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса почтового LEI дополнительного")]
        [Column("post_address2_en_language_code")]
        public string PostAddress2EnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового LEI дополнительного, Строка1")]
        [Column("post_address2_en_line1")]
        public string PostAddress2EnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового LEI дополнительного, Строка2")]
        [Column("post_address2_en_line2")]
        public string PostAddress2EnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового LEI дополнительного, Строка3")]
        [Column("post_address2_en_line3")]
        public string PostAddress2EnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса почтового LEI дополнительного, Строка4")]
        [Column("post_address2_en_line4")]
        public string PostAddress2EnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Адрес местонахождения LEI дополнительный")]
        [Column("place_addr2_en")]
        public string PlaceAddr2En { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса местонахождения LEI дополнительного")]
        [Column("place_addr2_en_country_code")]
        public string PlaceAddr2EnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса местонахождения LEI дополнительного")]
        [Column("place_addr2_en_city")]
        public string PlaceAddr2EnCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса местонахождения LEI дополнительного")]
        [Column("place_addr2_en_post_index")]
        public string PlaceAddr2EnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса местонахождения LEI дополнительного")]
        [Column("place_addr2_en_region_code")]
        public string PlaceAddr2EnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса местонахождения LEI дополнительного")]
        [Column("place_addr2_en_language_code")]
        public string PlaceAddr2EnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка1")]
        [Column("place_addr2_en_line1")]
        public string PlaceAddr2EnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка2")]
        [Column("place_addr2_en_line2")]
        public string PlaceAddr2EnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка3")]
        [Column("place_addr2_en_line3")]
        public string PlaceAddr2EnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка4")]
        [Column("place_addr2_en_line4")]
        public string PlaceAddr2EnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Код LEI для УК")]
        [Column("managing_cmp_lei_code")]
        public string ManagingCompanyLeiCode { get; set; }

        [Nullable]
        [DisplayName(@"Официальное наименование УК")]
        [Column("managing_cmp_legal_name")]
        public string ManagingCompanyLegalName { get; set; }

        [Nullable]
        [DisplayName(@"Язык официального наименования УК")]
        [Column("managing_cmp_legal_name_language_code")]
        public string ManagingCompanyLegalNameLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Код ISO для организационно-правовой формы")]
        [Column("lei_iso_code")]
        public string LeiIsoCode { get; set; }

        [Nullable]
        [DisplayName(@"Организационно-правовая форма организации для LEI")]
        [Column("company_type_name")]
        public string CompanyTypeName { get; set; }

        [DisplayName(@"Наименования на других языках")]
        [Column("names_was_changed")]
        public bool NamesWasChanged { get; set; }

        [Nullable]
        [DisplayName(@"Фирменное наименование из устава полное")]
        [Column("full_name_original")]
        public string FullNameOriginal { get; set; }

        [Nullable]
        [DisplayName(@"Фирменное наименование из устава краткое")]
        [Column("short_name_original")]
        public string ShortNameOriginal { get; set; }

        [Nullable]
        [DisplayName(@"Английское наименование полное фирменное")]
        [Column("full_name_en_original")]
        public string FullNameEnOriginal { get; set; }

        [Nullable]
        [DisplayName(@"Английское наименование краткое фирменное")]
        [Column("short_name_en_original")]
        public string ShortNameEnOriginal { get; set; }

        [Nullable]
        [DisplayName(@"Английское наименование полное транслитерированное")]
        [Column("common_name_full_en_original")]
        public string CommonNameFullEnOriginal { get; set; }

        [Nullable]
        [DisplayName(@"Английское наименование краткое транслитерированное")]
        [Column("common_name_short_en_original")]
        public string CommonNameShortEnOriginal { get; set; }

        [DisplayName(@"Последняя запись после которой обновления данных (по коду LEI) не учитываются")]
        [Column("last_record_update_data_taken")]
        public bool LastRecordUpdateDataTaken { get; set; }

        [Column("old_lei_assignment_date")]
        public DateTime? OldLeiAssignmentDate { get; set; }

        [Column("old_lei_verification_date")]
        public DateTime? OldLeiVerificationDate { get; set; }

        [Column("old_lei_next_recertification_date")]
        public DateTime? OldLeiNextRecertificationDate { get; set; }

        [Column("old_expiration_date")]
        public DateTime? OldExpirationDate { get; set; }

        [Nullable]
        [Column("old_registration_status")]
        public string OldRegistrationStatus { get; set; }

        [Nullable]
        [Column("old_entity_expiration_reason")]
        public string OldEntityExpirationReason { get; set; }

        [Column("old_rus_company")]
        public bool OldRusCompany { get; set; }

        [Column("old_is_inactive_cmp")]
        public bool OldIsInactiveCmp { get; set; }

        [Nullable]
        [Column("old_is_inactive_cmp_str")]
        public string OldIsInactiveCmpStr { get; set; }

        [Column("old_lei_applicant")]
        public bool OldLeiApplicant { get; set; }

        [Nullable]
        [Column("old_full_name")]
        public string OldFullName { get; set; }

        [Nullable]
        [Column("old_short_name")]
        public string OldShortName { get; set; }

        [Nullable]
        [Column("old_full_name_en")]
        public string OldFullNameEn { get; set; }

        [Nullable]
        [Column("old_short_name_en")]
        public string OldShortNameEn { get; set; }

        [Nullable]
        [Column("old_legal_name")]
        public string OldLegalName { get; set; }

        [Nullable]
        [Column("old_other_entity_name")]
        public string OldOtherEntityName { get; set; }

        [Nullable]
        [Column("old_registry_code")]
        public string OldRegistryCode { get; set; }

        [Column("old_transliterated")]
        public bool OldTransliterated { get; set; }

        [Nullable]
        [Column("old_place_addr")]
        public string OldPlaceAddr { get; set; }

        [Nullable]
        [Column("old_place_addr_en")]
        public string OldPlaceAddrEn { get; set; }

        [Nullable]
        [Column("old_place_addr_city")]
        public string OldPlaceAddrCity { get; set; }

        [Nullable]
        [Column("old_place_addr_country_code")]
        public string OldPlaceAddrCountryCode { get; set; }

        [Nullable]
        [Column("old_place_addr_post_index")]
        public string OldPlaceAddrPostIndex { get; set; }

        [Nullable]
        [Column("old_place_addr_region_code")]
        public string OldPlaceAddrRegionCode { get; set; }

        [Nullable]
        [Column("old_place_addr_language_code")]
        public string OldPlaceAddrLanguageCode { get; set; }

        [Nullable]
        [Column("old_place_addr_line1")]
        public string OldPlaceAddrLine1 { get; set; }

        [Nullable]
        [Column("old_place_addr_line2")]
        public string OldPlaceAddrLine2 { get; set; }

        [Nullable]
        [Column("old_place_addr_line3")]
        public string OldPlaceAddrLine3 { get; set; }

        [Nullable]
        [Column("old_place_addr_line4")]
        public string OldPlaceAddrLine4 { get; set; }

        [Nullable]
        [Column("old_place_addr_en_city")]
        public string OldPlaceAddrEnCity { get; set; }

        [Nullable]
        [Column("old_place_addr_en_country_code")]
        public string OldPlaceAddrEnCountryCode { get; set; }

        [Nullable]
        [Column("old_place_addr_en_post_index")]
        public string OldPlaceAddrEnPostIndex { get; set; }

        [Nullable]
        [Column("old_place_addr_en_region_code")]
        public string OldPlaceAddrEnRegionCode { get; set; }

        [Nullable]
        [Column("old_place_addr_en_language_code")]
        public string OldPlaceAddrEnLanguageCode { get; set; }

        [Nullable]
        [Column("old_place_addr_en_line1")]
        public string OldPlaceAddrEnLine1 { get; set; }

        [Nullable]
        [Column("old_place_addr_en_line2")]
        public string OldPlaceAddrEnLine2 { get; set; }

        [Nullable]
        [Column("old_place_addr_en_line3")]
        public string OldPlaceAddrEnLine3 { get; set; }

        [Nullable]
        [Column("old_place_addr_en_line4")]
        public string OldPlaceAddrEnLine4 { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address")]
        public string OldBasicSocietyOrPostAddress { get; set; }

        [Nullable]
        [Column("old_post_address")]
        public string OldPostAddress { get; set; }

        [Nullable]
        [Column("old_post_address_country_code")]
        public string OldPostAddressCountryCode { get; set; }

        [Nullable]
        [Column("old_post_address_city")]
        public string OldPostAddressCity { get; set; }

        [Nullable]
        [Column("old_post_address_post_index")]
        public string OldPostAddressPostIndex { get; set; }

        [Nullable]
        [Column("old_post_address_region_code")]
        public string OldPostAddressRegionCode { get; set; }

        [Nullable]
        [Column("old_post_address_language_code")]
        public string OldPostAddressLanguageCode { get; set; }

        [Nullable]
        [Column("old_post_address_line1")]
        public string OldPostAddressLine1 { get; set; }

        [Nullable]
        [Column("old_post_address_line2")]
        public string OldPostAddressLine2 { get; set; }

        [Nullable]
        [Column("old_post_address_line3")]
        public string OldPostAddressLine3 { get; set; }

        [Nullable]
        [Column("old_post_address_line4")]
        public string OldPostAddressLine4 { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en")]
        public string OldBasicSocietyOrPostAddressEn { get; set; }

        [Nullable]

        [Column("old_basic_society_or_post_address_en_city")]
        public string OldBasicSocietyOrPostAddressEnCity { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_country_code")]
        public string OldBasicSocietyOrPostAddressEnCountryCode { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_post_index")]
        public string OldBasicSocietyOrPostAddressEnPostIndex { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_region_code")]
        public string OldBasicSocietyOrPostAddressEnRegionCode { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_language_code")]
        public string OldBasicSocietyOrPostAddressEnLanguageCode { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_line1")]
        public string OldBasicSocietyOrPostAddressEnLine1 { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_line2")]
        public string OldBasicSocietyOrPostAddressEnLine2 { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_line3")]
        public string OldBasicSocietyOrPostAddressEnLine3 { get; set; }

        [Nullable]
        [Column("old_basic_society_or_post_address_en_line4")]
        public string OldBasicSocietyOrPostAddressEnLine4 { get; set; }

        [Nullable]
        [Column("old_post_address_en")]
        public string OldPostAddressEn { get; set; }

        [Nullable]
        [Column("old_post_address_en_country_code")]
        public string OldPostAddressEnCountryCode { get; set; }

        [Nullable]
        [Column("old_post_address_en_city")]
        public string OldPostAddressEnCity { get; set; }

        [Nullable]
        [Column("old_post_address_en_post_index")]
        public string OldPostAddressEnPostIndex { get; set; }

        [Nullable]
        [Column("old_post_address_en_region_code")]
        public string OldPostAddressEnRegionCode { get; set; }

        [Nullable]
        [Column("old_post_address_en_language_code")]
        public string OldPostAddressEnLanguageCode { get; set; }

        [Nullable]
        [Column("old_post_address_en_line1")]
        public string OldPostAddressEnLine1 { get; set; }

        [Nullable]
        [Column("old_post_address_en_line2")]
        public string OldPostAddressEnLine2 { get; set; }

        [Nullable]
        [Column("old_post_address_en_line3")]
        public string OldPostAddressEnLine3 { get; set; }

        [Nullable]
        [Column("old_post_address_en_line4")]
        public string OldPostAddressEnLine4 { get; set; }

        [Nullable]
        [Column("old_entity_legal_form")]
        public string OldEntityLegalForm { get; set; }

        [Nullable]
        [Column("old_business_reg_id")]
        public string OldBusinessRegId { get; set; }

        [Nullable]
        [Column("old_event_type_mn")]
        public string OldEventTypeMn { get; set; }

        [Column("old_successor_id")]
        public int? OldSuccessorId { get; set; }

        [Nullable]
        [Column("old_successor_lei_code")]
        public string OldSuccessorLeiCode { get; set; }

        [Nullable]
        [Column("old_successor_name")]
        public string OldSuccessorName { get; set; }

        [Nullable]
        [Column("old_successor_other_name")]
        public string OldSuccessorOtherName { get; set; }

        [Nullable]
        [Column("old_legal_jurisdiction")]
        public string OldLegalJurisdiction { get; set; }

        [Nullable]
        [Column("old_business_registry_name")]
        public string OldBusinessRegistryName { get; set; }

        [Nullable]
        [Column("old_post_address2_en")]
        public string OldPostAddress2En { get; set; }

        [Nullable]
        [Column("old_post_address2_en_country_code")]
        public string OldPostAddress2EnCountryCode { get; set; }

        [Nullable]
        [Column("old_post_address2_en_city")]
        public string OldPostAddress2EnCity { get; set; }

        [Nullable]
        [Column("old_post_address2_en_post_index")]
        public string OldPostAddress2EnPostIndex { get; set; }

        [Nullable]
        [Column("old_post_address2_en_region_code")]
        public string OldPostAddress2EnRegionCode { get; set; }

        [Nullable]
        [Column("old_post_address2_en_language_code")]
        public string OldPostAddress2EnLanguageCode { get; set; }

        [Nullable]
        [Column("old_post_address2_en_line1")]
        public string OldPostAddress2EnLine1 { get; set; }

        [Nullable]
        [Column("old_post_address2_en_line2")]
        public string OldPostAddress2EnLine2 { get; set; }

        [Nullable]
        [Column("old_post_address2_en_line3")]
        public string OldPostAddress2EnLine3 { get; set; }

        [Nullable]
        [Column("old_post_address2_en_line4")]
        public string OldPostAddress2EnLine4 { get; set; }

        [Nullable]
        [Column("old_place_addr2_en")]
        public string OldPlaceAddr2En { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_country_code")]
        public string OldPlaceAddr2EnCountryCode { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_city")]
        public string OldPlaceAddr2EnCity { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_post_index")]
        public string OldPlaceAddr2EnPostIndex { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_region_code")]
        public string OldPlaceAddr2EnRegionCode { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_language_code")]
        public string OldPlaceAddr2EnLanguageCode { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_line1")]
        public string OldPlaceAddr2EnLine1 { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_line2")]
        public string OldPlaceAddr2EnLine2 { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_line3")]
        public string OldPlaceAddr2EnLine3 { get; set; }

        [Nullable]
        [Column("old_place_addr2_en_line4")]
        public string OldPlaceAddr2EnLine4 { get; set; }

        [Nullable]
        [Column("old_basic_society_address")]
        public string OldBasicSocietyAddress { get; set; }

        [Nullable]
        [Column("old_basic_society_country_code")]
        public string OldBasicSocietyCountryCode { get; set; }

        [Nullable]
        [Column("old_basic_society_city")]
        public string OldBasicSocietyCity { get; set; }

        [Nullable]
        [Column("old_basic_society_post_index")]
        public string OldBasicSocietyPostIndex { get; set; }

        [Nullable]
        [Column("old_basic_society_region_code")]
        public string OldBasicSocietyRegionCode { get; set; }

        [Nullable]
        [Column("old_basic_society_language_code")]
        public string OldBasicSocietyLanguageCode { get; set; }

        [Nullable]

        [Column("old_basic_society_line1")]
        public string OldBasicSocietyLine1 { get; set; }

        [Nullable]

        [Column("old_basic_society_line2")]
        public string OldBasicSocietyLine2 { get; set; }

        [Nullable]

        [Column("old_basic_society_line3")]
        public string OldBasicSocietyLine3 { get; set; }

        [Nullable]

        [Column("old_basic_society_line4")]
        public string OldBasicSocietyLine4 { get; set; }

        [Nullable]
        [Column("old_basic_society_en_address")]
        public string OldBasicSocietyEnAddress { get; set; }

        [Nullable]
        [Column("old_basic_society_en_country_code")]
        public string OldBasicSocietyEnCountryCode { get; set; }

        [Nullable]

        [Column("old_basic_society_en_city")]
        public string OldBasicSocietyEnCity { get; set; }

        [Nullable]
        [Column("old_basic_society_en_post_index")]
        public string OldBasicSocietyEnPostIndex { get; set; }

        [Nullable]
        [Column("old_basic_society_en_region_code")]
        public string OldBasicSocietyEnRegionCode { get; set; }

        [Nullable]
        [Column("old_basic_society_en_language_code")]
        public string OldBasicSocietyEnLanguageCode { get; set; }

        [Nullable]
        [Column("old_basic_society_en_line1")]
        public string OldBasicSocietyEnLine1 { get; set; }

        [Nullable]
        [Column("old_basic_society_en_line2")]
        public string OldBasicSocietyEnLine2 { get; set; }

        [Nullable]
        [Column("old_basic_society_en_line3")]
        public string OldBasicSocietyEnLine3 { get; set; }

        [Nullable]
        [Column("old_basic_society_en_line4")]
        public string OldBasicSocietyEnLine4 { get; set; }

        [Nullable]
        [Column("old_prev_legal_name")]
        public string OldPrevLegalName { get; set; }

        [Nullable]
        [Column("old_prev_legal_name_language_code")]
        public string OldPrevLegalNameLanguageCode { get; set; }

        [Nullable]
        [Column("old_managing_cmp_lei_code")]
        public string OldManagingCompanyLeiCode { get; set; }

        [Nullable]
        [Column("old_managing_cmp_legal_name")]
        public string OldManagingCompanyLegalName { get; set; }

        [Nullable]
        [Column("old_managing_cmp_legal_name_language_code")]
        public string OldManagingCompanyLegalNameLanguageCode { get; set; }

        [Nullable]
        [Column("old_lei_iso_code")]
        public string OldLeiIsoCode { get; set; }

        [Nullable]
        [Column("old_lei_code")]
        public string OldLeiCode { get; set; }

        [Nullable]
        [Column("old_lei_code_status")]
        public string OldLeiCodeStatus { get; set; }

        [Column("old_lei1")]
        public bool OldLei1 { get; set; }

        [Column("old_lei2")]
        public bool OldLei2 { get; set; }

        [Nullable]
        [Column("old_common_name_full_en_original")]
        public string OldCommonNameFullEnOriginal { get; set; }

        [Nullable]
        [Column("old_common_name_short_en_original")]
        public string OldCommonNameShortEnOriginal { get; set; }

        [Nullable]
        [Column("old_full_name_en_original")]
        public string OldFullNameEnOriginal { get; set; }

        [Nullable]
        [DisplayName(@"Фирменное наименование из устава полное")]
        [Column("old_full_name_original")]
        public string OldFullNameOriginal { get; set; }

        [Nullable]
        [Column("old_legal_name_language_code")]
        public string OldLegalNameLanguageCode { get; set; }

        [Nullable]
        [Column("old_short_name_en_original")]
        public string OldShortNameEnOriginal { get; set; }

        [Column("old_short_name_original")]
        public string OldShortNameOriginal { get; set; }

        [Column("have_error")]
        public bool HaveError { get; set; }

        [Nullable]
        [Column("error_text")]
        public string ErrorText { get; set; }

        [Column("is_acceptable_notupdatable_code")]
        public bool IsAcceptableNotupdatableCode { get; set; }

        [Column("is_duplicate")]
        public bool IsDuplicate { get; set; }

        [Column("assignee_company_expiration_date")]
        public DateTime? AssigneeCompanyExpirationDate { get; set; }

        [Column("assignee_company_expiration_reason")]
        public string AssigneeCompanyExpirationReason { get; set; }

        [Column("assignee_company_lei_code")]
        public string AssigneeCompanyLeiCode { get; set; }

        [Column("assignee_company_name")]
        public string AssigneeCompanyName { get; set; }

        [Nullable]
        [Column(@"accepted_lei_code_state")]
        public DocLeiAcceptCodeStatus? AcceptedLeiCodeState { get; set; }

        [Column("is_archived")]
        public bool IsArchived { get; set; }

        public List<LeiPublicName> Names = new List<LeiPublicName>();

        [Nullable]
        [DisplayName(@"Код LEI для Child")]
        [Column("child_lei_code")]
        public string ChildLeiCode { get; set; }

        [Nullable]
        [DisplayName(@"Статус кода LEI Child (по CDF 2.0)")]
        [Column("child_lei_code_status")]
        public string ChildLeiCodeStatus { get; set; }

        [Nullable]
        [DisplayName(@"Дата обновления кода LEI Child")]
        [Column("child_lei_update_date")]
        public DateTime? ChildLeiUpdateDate { get; set; }

        [Nullable]
        [DisplayName(@"Наименование организации (Child)")]
        [Column("child_full_company_name")]
        public string ChildFullCompanyName { get; set; }

        [DisplayName(@"Запись для истории")]
        [Column("is_history")]
        public bool IsHistory { get; set; }

        [DisplayName(@"Признак связи")]
        [Column("link_attrib")]
        public string LinkAttrib { get; set; }

        [DisplayName(@"Дата события")]
        [Column("event_date")]
        public DateTime EventDate { get; set; }

        [DisplayName(@"Номер события")]
        [Column("event_num")]
        public string EventNum { get; set; }

        [DisplayName(@"Запись для выгрузки в XML? (Y|N)")]
        [Column("is_xml")]
        public bool IsXml { get; set; }

        [Nullable]
        [Column("parent_company_id")]
        public int? ParentCompanyId { get; set; }

        [Nullable]
        [Column("parent_lei_code_details_id")]
        public int? ParentLeiCodeDetailsId { get; set; }

        [Nullable]
        [DisplayName(@"Дата создания записи")]
        [Column("initial_registration_date")]
        public DateTime? InitialRegistrationDate { get; set; }

        [Nullable]
        [DisplayName(@"Дата изменения записи")]
        [Column("last_update_date")]
        public DateTime? LastUpdateDate { get; set; }

        [Nullable]
        [DisplayName(@"Наименование организации (parent)")]
        [Column("parent_full_company_name")]
        public string ParentFullCompanyName { get; set; }

        [Nullable]
        [DisplayName(@"Код LEI для Parent")]
        [Column("parent_lei_code")]
        public string ParentLeiCode { get; set; }

        [Nullable]
        [DisplayName(@"Код PNI для Parent")]
        [Column("parent_pni_code")]
        public string ParentPniCode { get; set; }

        [Nullable]
        [DisplayName(@"Тип кода LEI")]
        [Column("lei_code_type")]
        public string LeiCodeType { get; set; }

        [Nullable]
        [DisplayName(@"Тип кода LEI Parent в «терминах КБД»")]
        [Column("lei_code_type_cdb")]
        public string LeiCodeTypeCdb { get; set; }

        [DisplayName(@"Статус связи")]
        [Column("relationship_status")]
        public string RelationshipStatus { get; set; }

        [Nullable]
        [DisplayName(@"Relationship Period (StartDate)")]
        [Column("relationship_period_start_date")]
        public DateTime? RelationshipPeriodStartDate { get; set; }

        [Nullable]
        [DisplayName(@"Relationship Period (EndDate)")]
        [Column("relationship_period_end_date")]
        public DateTime? RelationshipPeriodEndDate { get; set; }

        [Nullable]
        [DisplayName(@"Accounting Period (StartDate)")]
        [Column("accounting_period_start_date")]
        public DateTime? AccountingPeriodStartDate { get; set; }

        [Nullable]
        [DisplayName(@"Accounting Period (EndDate)")]
        [Column("accounting_period_end_date")]
        public DateTime? AccountingPeriodEndDate { get; set; }

        [Nullable]
        [DisplayName(@"Document_Filling Period (StartDate)")]
        [Column("document_filing_period_start_date")]
        public DateTime? DocumentFillingPeriodStartDate { get; set; }

        [Nullable]
        [DisplayName(@"Document_Filing Period (EndDate)")]
        [Column("document_filing_period_end_date")]
        public DateTime? DocumentFillingPeriodEndDate { get; set; }

        [Nullable]
        [DisplayName(@"Наименование категории")]
        [Column("qualifier_category")]
        public string QualifierCategory { get; set; }

        [Nullable]
        [DisplayName(@"Quantifier Amount")]
        [Column("quantifier_amount")]
        public decimal? QuantifierAmount { get; set; }

        [Nullable]
        [DisplayName(@"Статус регистрации связи")]
        [Column("parent_registration_status")]
        public string ParentRegistrationStatus { get; set; }

        [Nullable]
        [DisplayName(@"Дата следующей верификации (Parent)")]
        [Column("parent_next_renewal_date")]
        public DateTime? ParentNextRenewalDate { get; set; }

        [Nullable]
        [DisplayName(@"Стадия подтверждения данных")]
        [Column("validation_source")]
        public string ValidationSource { get; set; }

        [Nullable]
        [DisplayName(@"Подтверждающие документы")]
        [Column("validation_document")]
        public string ValidationDocument { get; set; }

        [Nullable]
        [DisplayName(@"Validation Reference")]
        [Column("validation_reference")]
        public string ValidationReference { get; set; }

        [Nullable]
        [DisplayName(@"Дата создания PNI")]
        [Column("pni_creation_date")]
        public DateTime? PniCreationDate { get; set; }

        [Nullable]
        [DisplayName(@"Дата обновления PNI")]
        [Column("pni_last_update_date")]
        public DateTime? PniLastUpdateDate { get; set; }

        [Nullable]
        [DisplayName(@"Дата следующей верификации (Child)")]
        [Column("cdf_next_renewal_date")]
        public DateTime? CdfNextRenewalDate { get; set; }

        [Nullable]
        [DisplayName(@"Статус регистрации организации")]
        [Column("cdf_registration_status")]
        public string CdfRegistrationStatus { get; set; }

        [Nullable]
        [DisplayName(@"Состояние организации действ./недейств.")]
        [Column("cdf_entity_status")]
        public string CdfEntityStatus { get; set; }

        [Nullable]
        [DisplayName(@"Полное наименование")]
        [Column("company_full_name")]
        public string CompanyFullName { get; set; }

        [Nullable]
        [DisplayName(@"Краткое наименование")]
        [Column("company_short_name")]
        public string CompanyShortName { get; set; }

        [Nullable]
        [DisplayName(@"Полное наименование на английском")]
        [Column("company_full_name_en")]
        public string CompanyFullNameEn { get; set; }

        [Nullable]
        [DisplayName(@"Краткое наименование на английском")]
        [Column("company_short_name_en")]
        public string CompanyShortNameEn { get; set; }

        [Nullable]
        [DisplayName(@"Адрес местонахождения (по анкете)")]
        [Column("place_address")]
        public string PlaceAddress { get; set; }

        [Nullable]
        [DisplayName(@"Адрес местонахождения (ин.яз.)")]
        [Column("place_address_en")]
        public string PlaceAddressEn { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса местонахождения (ин.яз.)")]
        [Column("place_address_en_country_code")]
        public string PlaceAddressEnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса местонахождения (ин.яз.)")]
        [Column("place_address_en_city")]
        public string PlaceAddressEnCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса местонахождения (ин.яз.)")]
        [Column("place_address_en_post_index")]
        public string PlaceAddressEnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса местонахождения (ин.яз.)")]
        [Column("place_address_en_region_code")]
        public string PlaceAddressEnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса местонахождения (ин.яз.)")]
        [Column("place_address_en_language_code")]
        public string PlaceAddressEnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка1")]
        [Column("place_address_en_line1")]
        public string PlaceAddressEnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка2")]
        [Column("place_address_en_line2")]
        public string PlaceAddressEnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка3")]
        [Column("place_address_en_line3")]
        public string PlaceAddressEnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения (ин.яз.), Строка4")]
        [Column("place_address_en_line4")]
        public string PlaceAddressEnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Адрес местонахождения LEI дополнительный")]
        [Column("place_address2_en")]
        public string PlaceAddress2En { get; set; }

        [Nullable]
        [DisplayName(@"Страна адреса местонахождения LEI дополнительного")]
        [Column("place_address2_en_country_code")]
        public string PlaceAddress2EnCountryCode { get; set; }

        [Nullable]
        [DisplayName(@"Город адреса местонахождения LEI дополнительного")]
        [Column("place_address2_en_city")]
        public string PlaceAddress2EnCity { get; set; }

        [Nullable]
        [DisplayName(@"Индекс адреса местонахождения LEI дополнительного")]
        [Column("place_address2_en_post_index")]
        public string PlaceAddress2EnPostIndex { get; set; }

        [Nullable]
        [DisplayName(@"Регион адреса местонахождения LEI дополнительного")]
        [Column("place_address2_en_region_code")]
        public string PlaceAddress2EnRegionCode { get; set; }

        [Nullable]
        [DisplayName(@"Язык адреса местонахождения LEI дополнительного")]
        [Column("place_address2_en_language_code")]
        public string PlaceAddress2EnLanguageCode { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка1")]
        [Column("place_address2_en_line1")]
        public string PlaceAddress2EnLine1 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка2")]
        [Column("place_address2_en_line2")]
        public string PlaceAddress2EnLine2 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка3")]
        [Column("place_address2_en_line3")]
        public string PlaceAddress2EnLine3 { get; set; }

        [Nullable]
        [DisplayName(@"Улица дом адреса местонахождения LEI дополнительного, Строка4")]
        [Column("place_address2_en_line4")]
        public string PlaceAddress2EnLine4 { get; set; }

        [Nullable]
        [DisplayName(@"Полное наименование на английском")]
        [Column("company_common_full_name_en")]
        public string CompanyCommonFullNameEn { get; set; }

        [Nullable]
        [DisplayName(@"Краткое наименование на английском")]
        [Column("company_common_short_name_en")]
        public string CompanyCommonShortNameEn { get; set; }

        [Nullable]
        [DisplayName(@"Наименование причины отсутствия связи")]
        [Column("exception_reason")]
        public string ExceptionReason { get; set; }

        [Nullable]
        [DisplayName(@"Комментарий к причине")]
        [Column("exception_reason_comment")]
        public string ExceptionReasonComment { get; set; }

        [DisplayName(@"Идентификатор записи на вкладке LEVEL2")]
        [Column("lei_level2_parents_rec_id")]
        public int LeiLevel2ParentsRecId { get; set; }

        [DisplayName(@"Дата внесения записи в таблицу")]
        [Column("insert_dt")]
        public DateTime InsertDt { get; set; }

        [DisplayName(@"Пользователь, добавивший запись")]
        [Column("insert_user")]
        public string InsertUser { get; set; }

        [Column("old_lei_code_id")]
        public int OldLeiCodeId { get; set; }

        [Column("old_company_id")]
        public int OldCompanyId { get; set; }

        [Column("old_child_lei_code")]
        public string OldChildLeiCode { get; set; }

        [Nullable]
        [Column("old_child_lei_code_status")]
        public string OldChildLeiCodeStatus { get; set; }

        [Nullable]
        [Column("old_child_lei_update_date")]
        public DateTime? OldChildLeiUpdateDate { get; set; }

        [Nullable]
        [Column("old_child_full_company_name")]
        public string OldChildFullCompanyName { get; set; }

        [Column("old_is_history")]
        public bool OldIsHistory { get; set; }

        [Column("old_link_attrib")]
        public string OldLinkAttrib { get; set; }

        [Column("old_event_date")]
        public DateTime OldEventDate { get; set; }

        [Column("old_event_num")]
        public string OldEventNum { get; set; }

        [Column("old_is_xml")]
        public bool OldIsXml { get; set; }

        [Nullable]
        [Column("old_initial_registration_date")]
        public DateTime? OldInitialRegistrationDate { get; set; }

        [Nullable]
        [Column("old_last_update_date")]
        public DateTime? OldLastUpdateDate { get; set; }

        [Nullable]
        [Column("old_parent_full_company_name")]
        public string OldParentFullCompanyName { get; set; }

        [Nullable]
        [Column("old_parent_lei_code")]
        public string OldParentLeiCode { get; set; }

        [Nullable]
        [Column("old_parent_pni_code")]
        public string OldParentPniCode { get; set; }

        [Nullable]
        [Column("old_lei_code_type")]
        public string OldLeiCodeType { get; set; }

        [Nullable]
        [Column("old_lei_code_type_cdb")]
        public string OldLeiCodeTypeCdb { get; set; }

        [Column("old_relationship_status")]
        public string OldRelationshipStatus { get; set; }

        [Nullable]
        [Column("old_relationship_period_start_date")]
        public DateTime? OldRelationshipPeriodStartDate { get; set; }

        [Nullable]
        [Column("old_relationship_period_end_date")]
        public DateTime? OldRelationshipPeriodEndDate { get; set; }

        [Nullable]
        [Column("old_accounting_period_start_date")]
        public DateTime? OldAccountingPeriodStartDate { get; set; }

        [Nullable]
        [Column("old_accounting_period_end_date")]
        public DateTime? OldAccountingPeriodEndDate { get; set; }

        [Nullable]
        [Column("old_document_filing_period_start_date")]
        public DateTime? OldDocumentFillingPeriodStartDate { get; set; }

        [Nullable]
        [Column("old_document_filing_period_end_date")]
        public DateTime? OldDocumentFillingPeriodEndDate { get; set; }

        [Nullable]
        [Column("old_qualifier_category")]
        public string OldQualifierCategory { get; set; }

        [Nullable]
        [Column("old_quantifier_amount")]
        public decimal? OldQuantifierAmount { get; set; }

        [Nullable]
        [Column("old_parent_registration_status")]
        public string OldParentRegistrationStatus { get; set; }

        [Nullable]
        [Column("old_parent_next_renewal_date")]
        public DateTime? OldParentNextRenewalDate { get; set; }

        [Nullable]
        [Column("old_validation_source")]
        public string OldValidationSource { get; set; }

        [Nullable]
        [Column("old_validation_document")]
        public string OldValidationDocument { get; set; }

        [Nullable]
        [Column("old_validation_reference")]
        public string OldValidationReference { get; set; }

        [Nullable]
        [Column("old_pni_creation_date")]
        public DateTime? OldPniCreationDate { get; set; }

        [Nullable]
        [Column("old_pni_last_update_date")]
        public DateTime? OldPniLastUpdateDate { get; set; }

        [Nullable]
        [Column("old_cdf_next_renewal_date")]
        public DateTime? OldCdfNextRenewalDate { get; set; }

        [Nullable]
        [Column("old_cdf_registration_status")]
        public string OldCdfRegistrationStatus { get; set; }

        [Column("old_cdf_entity_status")]
        public string OldCdfEntityStatus { get; set; }

        [Nullable]
        [Column("old_company_full_name")]
        public string OldCompanyFullName { get; set; }

        [Nullable]
        [Column("old_company_short_name")]
        public string OldCompanyShortName { get; set; }

        [Nullable]
        [Column("old_company_full_name_en")]
        public string OldCompanyFullNameEn { get; set; }

        [Nullable]
        [Column("old_company_short_name_en")]
        public string OldCompanyShortNameEn { get; set; }

        [Nullable]
        [Column("old_place_address")]
        public string OldPlaceAddress { get; set; }

        [Nullable]
        [Column("old_place_address_en")]
        public string OldPlaceAddressEn { get; set; }

        [Nullable]
        [Column("old_place_address_en_country_code")]
        public string OldPlaceAddressEnCountryCode { get; set; }

        [Nullable]
        [Column("old_place_address_en_city")]
        public string OldPlaceAddressEnCity { get; set; }

        [Nullable]
        [Column("old_place_address_en_post_index")]
        public string OldPlaceAddressEnPostIndex { get; set; }

        [Nullable]
        [Column("old_place_address_en_region_code")]
        public string OldPlaceAddressEnRegionCode { get; set; }

        [Nullable]
        [Column("old_place_address_en_language_code")]
        public string OldPlaceAddressEnLanguageCode { get; set; }

        [Nullable]
        [Column("old_place_address_en_line1")]
        public string OldPlaceAddressEnLine1 { get; set; }

        [Nullable]
        [Column("old_place_address_en_line2")]
        public string OldPlaceAddressEnLine2 { get; set; }

        [Nullable]
        [Column("old_place_address_en_line3")]
        public string OldPlaceAddressEnLine3 { get; set; }

        [Nullable]
        [Column("old_place_address_en_line4")]
        public string OldPlaceAddressEnLine4 { get; set; }

        [Nullable]
        [Column("old_place_address2_en")]
        public string OldPlaceAddress2En { get; set; }

        [Nullable]
        [Column("old_place_address2_en_country_code")]
        public string OldPlaceAddress2EnCountryCode { get; set; }

        [Nullable]
        [Column("old_place_address2_en_city")]
        public string OldPlaceAddress2EnCity { get; set; }

        [Nullable]
        [Column("old_place_address2_en_post_index")]
        public string OldPlaceAddress2EnPostIndex { get; set; }

        [Nullable]
        [Column("old_place_address2_en_region_code")]
        public string OldPlaceAddress2EnRegionCode { get; set; }

        [Nullable]
        [Column("old_place_address2_en_language_code")]
        public string OldPlaceAddress2EnLanguageCode { get; set; }

        [Nullable]
        [Column("old_place_address2_en_line1")]
        public string OldPlaceAddress2EnLine1 { get; set; }

        [Nullable]
        [Column("old_place_address2_en_line2")]
        public string OldPlaceAddress2EnLine2 { get; set; }

        [Nullable]
        [Column("old_place_address2_en_line3")]
        public string OldPlaceAddress2EnLine3 { get; set; }

        [Nullable]
        [Column("old_place_address2_en_line4")]
        public string OldPlaceAddress2EnLine4 { get; set; }

        [Nullable]
        [Column("old_names_was_changed")]
        public bool OldNamesWasChanged { get; set; }

        [Nullable]
        [Column("old_company_common_full_name_en")]
        public string OldCompanyCommonFullNameEn { get; set; }

        [Nullable]
        [Column("old_company_common_short_name_en")]
        public string OldCompanyCommonShortNameEn { get; set; }

        [Nullable]
        [Column("old_exception_reason")]
        public string OldExceptionReason { get; set; }

        [Nullable]
        [Column("old_exception_reason_comment")]
        public string OldExceptionReasonComment { get; set; }

        [Column("old_insert_dt")]
        public DateTime OldInsertDt { get; set; }

        [Column("old_insert_user")]

        //Альтернативные имена организации по стандарту LEI 1.0
        public List<LeiPublicNameLevel2> NamesLevel2;

        // Дополнительные события (ТОЛЬКО для вывода в журнал)
        private readonly List<LeiPublicLevel2> _extraEvents;

        public void AddExtraEvent(LeiPublicLevel2 leiPublicLevel2)
        {
            _extraEvents.Add(leiPublicLevel2);
        }

        public string State
        {
            get
            {
                return _extraEvents.Any()
                    ? _extraEvents.Select(e => new { EventNum = e.EventNum, EventDate = e.EventDate })
                        .Distinct()
                        .OrderBy(e => e.EventDate)
                        .Select(e => e.EventDate.ToString("HH:mm") + " State " + e.EventNum)
                        .Aggregate((i, j) => i + ", " + j)
                    : EventDate.ToString("HH:mm") + " State " + EventNum;
            }
            private set { }
        }

        [DisplayName(@"Состояние организации действ./недейств.")]
        public string CdfEntityStatusStr => IsInactiveCmp ? (IsInactiveCmp ? "Недействующая" : "Действующая") : string.Empty;

        public string OldCdfEntityStatusStr => OldIsInactiveCmp ? (OldIsInactiveCmp ? "Недействующая" : "Действующая") : string.Empty;
        public Companies Company { get; set; }
    }
}
