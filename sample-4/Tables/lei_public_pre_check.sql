create table lei_public_pre_check
(
	lei_code_id int identity(1, 1) not null,
	lei_pre_check_id int null,
	lei_code_status char(1) not null,
	lei_code varchar(20) not null,
	company_id int not null,
	lei_assignment_date datetime null,
	lei_update_date datetime null,
	lei_verification_date datetime null,
	lei_next_recertification_date datetime null,
	expiration_date datetime null,
	registration_status varchar(20) null,
	rus_company char(1) null,
	is_inactive_cmp char(1) null,
	lei_applicant char(1) null,
	investment_fund char(1) null,
	full_name varchar(254) null,
	short_name varchar(150) null,
	full_name_en varchar(250) null,
	short_name_en varchar(150) null,
	other_entity_name varchar(254) null,
	transliterated char(1) null,
	place_addr varchar(2200) null,
	place_addr_country_code char(2) null,
	place_addr_city varchar(50) null,
	place_addr_post_index varchar(6) null,
	place_addr_region_code varchar(10) null,
	place_addr_language_code varchar(2) null,
	place_addr_line1 nvarchar(500) null,
	place_addr_line2 nvarchar(500) null,
	place_addr_line3 nvarchar(500) null,
	place_addr_line4 nvarchar(500) null,
	place_addr_en nvarchar(2200) null,
	place_addr_en_city varchar(50) null,
	place_addr_en_country_code char(2) null,
	place_addr_en_post_index varchar(6) null,
	place_addr_en_region_code varchar(10) null,
	place_addr_en_language_code varchar(2) null,
	place_addr_en_line1 nvarchar(500) null,
	place_addr_en_line2 nvarchar(500) null,
	place_addr_en_line3 nvarchar(500) null,
	place_addr_en_line4 nvarchar(500) null,
	basic_society_or_post_address varchar(2200) null,
	post_address varchar(2200) null,
	mail_routing_address varchar(2200) null,
	post_address_country_code char(2) null,
	post_address_city varchar(50) null,
	post_address_post_index varchar(6) null,
	post_address_region_code varchar(10) null,
	post_address_language_code varchar(2) null,
	post_address_line1 nvarchar(500) null,
	post_address_line2 nvarchar(500) null,
	post_address_line3 nvarchar(500) null,
	post_address_line4 nvarchar(500) null,
	basic_society_or_post_address_en nvarchar(2200) null,
	basic_society_or_post_address_en_city varchar(50) null,
	basic_society_or_post_address_en_country_code char(2) null,
	basic_society_or_post_address_en_post_index varchar(6) null,
	basic_society_or_post_address_en_region_code varchar(10) null,
	basic_society_or_post_address_en_language_code varchar(2) null,
	basic_society_or_post_address_en_line1 nvarchar(500) null,
	basic_society_or_post_address_en_line2 nvarchar(500) null,
	basic_society_or_post_address_en_line3 nvarchar(500) null,
	basic_society_or_post_address_en_line4 nvarchar(500) null,
	post_address_en varchar(2200) null,
	mail_routing_address_en varchar(2200) null,
	post_address_en_country_code char(2) null,
	post_address_en_city varchar(50) null,
	post_address_en_post_index varchar(6) null,
	post_address_en_region_code varchar(10) null,
	post_address_en_language_code varchar(2) null,
	post_address_en_line1 nvarchar(500) null,
	post_address_en_line2 nvarchar(500) null,
	post_address_en_line3 nvarchar(500) null,
	post_address_en_line4 nvarchar(500) null,
	entity_legal_form varchar(280) null,
	business_reg_id varchar(50) null,
	event_type_mn varchar(6) null,
	successor_id int null,
	successor_lei_code varchar(20) null,
	successor_name varchar(254) null,
	successor_other_name varchar(254) null,
	legal_name nvarchar(400) null,
	legal_name_language_code varchar(2) null,
	prev_legal_name nvarchar(400) null,
	prev_legal_name_language_code varchar(2) null,
	basic_society_address varchar(2200) null,
	basic_society_country_code char(2) null,
	basic_society_city varchar(50) null,
	basic_society_post_index varchar(6) null,
	basic_society_region_code varchar(10) null,
	basic_society_language_code varchar(2) null,
	basic_society_line1 nvarchar(500) null,
	basic_society_line2 nvarchar(500) null,
	basic_society_line3 nvarchar(500) null,
	basic_society_line4 nvarchar(500) null,
	basic_society_en_address varchar(2200) null,
	basic_society_en_country_code char(2) null,
	basic_society_en_city varchar(50) null,
	basic_society_en_post_index varchar(6) null,
	basic_society_en_region_code varchar(10) null,
	basic_society_en_language_code varchar(2) null,
	basic_society_en_line1 nvarchar(500) null,
	basic_society_en_line2 nvarchar(500) null,
	basic_society_en_line3 nvarchar(500) null,
	basic_society_en_line4 nvarchar(500) null,
	managing_cmp_legal_name nvarchar(400) null,
	managing_cmp_legal_name_language_code varchar(2) null,
	legal_jurisdiction char(2) null,
	business_registry_name varchar(40) null,
	managing_cmp_lei_code varchar(20) null,
	lei_iso_code varchar(4) null,
	company_type_name varchar(280) null,
	post_address2_en nvarchar(2200) null,
	post_address2_en_country_code char(2) null,
	post_address2_en_city varchar(50) null,
	post_address2_en_post_index varchar(6) null,
	post_address2_en_region_code varchar(10) null,
	post_address2_en_language_code varchar(2) null,
	post_address2_en_line1 nvarchar(500) null,
	post_address2_en_line2 nvarchar(500) null,
	post_address2_en_line3 nvarchar(500) null,
	post_address2_en_line4 nvarchar(500) null,
	place_addr2_en nvarchar(2200) null,
	place_addr2_en_country_code char(2) null,
	place_addr2_en_city varchar(50) null,
	place_addr2_en_post_index varchar(6) null,
	place_addr2_en_region_code varchar(10) null,
	place_addr2_en_language_code varchar(2) null,
	place_addr2_en_line1 nvarchar(500) null,
	place_addr2_en_line2 nvarchar(500) null,
	place_addr2_en_line3 nvarchar(500) null,
	place_addr2_en_line4 nvarchar(500) null,
	names_was_changed char(1) null,
	full_name_original varchar(254) null,
	short_name_original varchar(150) null,
	full_name_en_original varchar(250) null,
	short_name_en_original varchar(150) null,
	common_name_full_en_original varchar(250) null,
	common_name_short_en_original varchar(150) null,
	registry_code varchar(8) null,
	entity_expiration_reason varchar(16) null,
	lei1 char(1) not null,
	lei2 char(1) not null,
	last_record_update_data_taken char(1) not null,
	insert_dt datetime not null,
	insert_user varchar(100) not null
)
GO
/* Primary key */
alter table lei_public_pre_check add constraint PK_leipublicprecheck_leicodeid primary key clustered
(
	lei_code_id asc
)
GO
/* Indexes */
create nonclustered index IX_leipublicprecheck_companyid on lei_public_pre_check
(
	company_id asc
)
GO
/* Foreign keys */
alter table lei_public_pre_check with check add constraint FK_leipublicprecheck_companies_companyid_companyid foreign key
(company_id) references companies (company_id)
GO
alter table lei_public_pre_check with check add constraint FK_leipublicprecheck_leiprecheck_leiprecheckid_leiprecheckid foreign key
(lei_pre_check_id) references lei_pre_check (lei_pre_check_id)
GO
/* Check constraints */
alter table lei_public_pre_check with check add constraint CK_leipublicprecheck_1 check ((transliterated='Y' or transliterated='N'))
GO
alter table lei_public_pre_check with check add constraint CK_leipublicprecheck_2 check ((lei1='Y' or lei1='N'))
GO
alter table lei_public_pre_check with check add constraint CK_leipublicprecheck_3 check ((lei2='Y' or lei2='N'))
GO
/* Default constraints */
alter table lei_public_pre_check add constraint DF_leipublicprecheck_insertdt default (getdate()) for insert_dt
GO
alter table lei_public_pre_check add constraint DF_leipublicprecheck_insertuser default (suser_sname()) for insert_user
GO
alter table lei_public_pre_check add constraint DF_leipublicprecheck_lastrecordupdatedatataken default ('N') for last_record_update_data_taken
GO
alter table lei_public_pre_check add constraint DF_leipublicprecheck_lei1 default ('N') for lei1
GO
alter table lei_public_pre_check add constraint DF_leipublicprecheck_lei2 default ('N') for lei2
GO
alter table lei_public_pre_check add constraint DF_leipublicprecheck_leicodestatus default ('A') for lei_code_status
GO
alter table lei_public_pre_check add constraint DF_leipublicprecheck_transliterated default ('N') for transliterated
GO
/* Description */
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Выгрузка кодов LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес основного общества',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_address'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса основного общества',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса основного общества',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес основного общества (ин.яз)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_address'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса основного общества (ин.яз)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса основного общества (ин.яз)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса основного общества (ин.яз)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества (ин.яз), Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества (ин.яз), Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества (ин.яз), Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества (ин.яз), Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса основного общества (ин.яз)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса основного общества (ин.яз)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_en_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса основного общества',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества, Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества, Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества, Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса основного общества, Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес почтовый (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес почтовый (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_or_post_address_en_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса основного общества',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса основного общества',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'basic_society_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'ОГРН, если пусто – Номер госрегистрации или номер правил ПИФ',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'business_reg_id'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Наименование регистрирующего органа',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'business_registry_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Английское наименование полное транслитерированное',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'common_name_full_en_original'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Английское наименование краткое транслитерированное',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'common_name_short_en_original'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Организационно-правовая форма организации для LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'company_type_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Причина прекращения действия организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'entity_expiration_reason'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Организационно-правовая форма',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'entity_legal_form'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Мнемоника события прекращения действия',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'event_type_mn'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Дата прекращения действия организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'expiration_date'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Полное наименование',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'full_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Полное английское наименование',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'full_name_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Английское наименование полное фирменное',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'full_name_en_original'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Фирменное наименование из устава полное',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'full_name_original'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Признак "НЕДЕЙСТВУЮЩАЯ"',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'is_inactive_cmp'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Последняя запись, после которой обновления данных (по коду LEI) не учитываются',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'last_record_update_data_taken'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Код страны по месту нахождения',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'legal_jurisdiction'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Признак "Официальное наименование организации"',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'legal_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык официального наименования организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'legal_name_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Заявитель кода LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_applicant'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Дата создания кода LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_assignment_date'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Код LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_code_id'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Статус кода LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_code_status'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Код ISO для организационно-правовой формы',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_iso_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Дата последующей сертификации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_next_recertification_date'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Дата последнего обновления LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_update_date'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Последняя дата верификации кода LEI',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei_verification_date'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Для формата LEI 1.0',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Для формата LEI 2.0',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'lei2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Официальное наименование УК',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'managing_cmp_legal_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык официального наименования УК',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'managing_cmp_legal_name_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Код LEI для УК',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'managing_cmp_lei_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Признак наличия изменений наименований',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'names_was_changed'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Другое наименование организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'other_entity_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес местонахождения (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса местонахождения (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса местонахождения (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса местонахождения (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (ин.яз.), Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (ин.яз.), Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (ин.яз.), Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (ин.яз.), Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса местонахождения (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса местонахождения (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_en_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (по анкете), Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (по анкете), Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (по анкете), Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения (по анкете), Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес местонахождения LEI дополнительный',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса местонахождения LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса местонахождения LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса местонахождения LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения LEI дополнительного, Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения LEI дополнительного, Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения LEI дополнительного, Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса местонахождения LEI дополнительного, Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса местонахождения LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса местонахождения LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'place_addr2_en_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес местонахождения (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса почтового (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса почтового (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес почтовый (ин.яз) (отличен от поля basic_society_or_post_address тем, что все время заполняется исключительно при помощи одного реквизита без вариантов в случае его отсутствия',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (ин.яз.), Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса почтового (ин.яз.)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_en_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса почтового (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (по анкете), Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (по анкете), Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (по анкете), Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового (по анкете), Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса почтового (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса почтового (по анкете)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Адрес почтовый LEI дополнительный',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Город адреса почтового LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_city'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Страна адреса почтового LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_country_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык адреса почтового LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового LEI дополнительного, Строка1',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_line1'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового LEI дополнительного, Строка2',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_line2'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового LEI дополнительного, Строка3',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_line3'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Улица дом адреса почтового LEI дополнительного, Строка4',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_line4'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Индекс адреса почтового LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_post_index'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Регион адреса почтового LEI дополнительного',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'post_address2_en_region_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Предыдущее официальное наименование организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'prev_legal_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Язык предыдущего официального наименования организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'prev_legal_name_language_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Статус регистрации организации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'registration_status'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Реестр регистрации',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'registry_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Организация-резидент',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'rus_company'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Краткое наименование',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'short_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Краткое английское наименование',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'short_name_en'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Английское наименование краткое фирменное',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'short_name_en_original'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Фирменное наименование из устава краткое',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'short_name_original'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Идентификатор организации-правопреемника',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'successor_id'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Кол LEI правопреемника',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'successor_lei_code'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Название правопреемника',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'successor_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Иностранное название правопреемника',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'successor_other_name'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Признак транслитерированности other_name',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'lei_public_pre_check',
	@level2type = N'COLUMN', @level2name = N'transliterated'
GO
/* Grants */
grant select on lei_public_pre_check to role_oper as dbo
GO
grant insert on lei_public_pre_check to role_oper as dbo
GO
grant update on lei_public_pre_check to role_oper as dbo
GO
grant select on lei_public_pre_check to role_oper_sched as dbo
GO
grant insert on lei_public_pre_check to role_oper_sched as dbo
GO
grant update on lei_public_pre_check to role_oper_sched as dbo
GO
grant select on lei_public_pre_check to [SQL_corpdb_sp&read] as dbo
GO
grant select on lei_public_pre_check to [SQL_corpdb_sp&write] as dbo
GO
grant select on lei_public_pre_check to web_corpdb_omr as dbo
GO