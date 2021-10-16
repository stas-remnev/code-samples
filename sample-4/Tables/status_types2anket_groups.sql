create table status_types2anket_groups
(
	rec_id int identity(1, 1) not null,
	gr int not null,
	status_type_mn char(3) not null,
	insert_dt datetime null,
	insert_user nvarchar(50) null
)
GO
/* Primary key */
alter table status_types2anket_groups add constraint PK_statustypes2anketgroups_recid primary key clustered
(
	rec_id asc
)
GO
/* Foreign keys */
alter table status_types2anket_groups with check add constraint FK_statustypes2anketgroups_statustypes_statustypemn_statustypemn foreign key
(status_type_mn) references status_types (status_type_mn)
GO
/* Default constraints */
alter table status_types2anket_groups add constraint DF_statustypes2anketgroups_insertdt default (getdate()) for insert_dt
GO
alter table status_types2anket_groups add constraint DF_statustypes2anketgroups_insertuser default (suser_sname()) for insert_user
GO
/* Description */
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Cправочник соответствий типов статусов организации/физ.лица и групп для обмена данными по организациям с ПО Аламеда ',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'status_types2anket_groups'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Номер группы (1/2)',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'status_types2anket_groups',
	@level2type = N'COLUMN', @level2name = N'gr'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Мнемокод статуса',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'status_types2anket_groups',
	@level2type = N'COLUMN', @level2name = N'status_type_mn'
GO
/* Grants */
grant select on status_types2anket_groups to role_oper_sched as dbo
GO
grant insert on status_types2anket_groups to role_oper_sched as dbo
GO
grant update on status_types2anket_groups to role_oper_sched as dbo
GO