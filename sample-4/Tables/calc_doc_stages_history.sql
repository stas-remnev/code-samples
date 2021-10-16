create table calc_doc_stages_history
(
	rec_id int identity(1, 1) not null,
	doc_id int not null,
	calc_doc_stage_mn varchar(6) not null,
	insert_dt datetime not null,
	insert_user varchar(100) not null
)
GO
/* Primary key */
alter table calc_doc_stages_history add constraint PK_calcdocstageshistory_recid primary key clustered
(
	rec_id asc
)
GO
/* Indexes */
create nonclustered index IX_calcdocstageshistory_docid_insertdt on calc_doc_stages_history
(
	doc_id asc,
	insert_dt asc
)
GO
create nonclustered index IX_calcdocstageshistory_recid_calcdocstagemn on calc_doc_stages_history
(
	rec_id asc,
	calc_doc_stage_mn asc
)
GO
/* Foreign keys */
alter table calc_doc_stages_history with check add constraint FK_calcdocstageshistory_documents_docid_docid foreign key
(doc_id) references documents (doc_id)
GO
/* Default constraints */
alter table calc_doc_stages_history add constraint DF_calcdocstageshistory_insertdt default (getdate()) for insert_dt
GO
alter table calc_doc_stages_history add constraint DF_calcdocstageshistory_insertuser default (suser_sname()) for insert_user
GO
/* Description */
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'История рассчетных стадий документа',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'calc_doc_stages_history'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Рассчетная стадия документа',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'calc_doc_stages_history',
	@level2type = N'COLUMN', @level2name = N'calc_doc_stage_mn'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Идентификатор документа',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'calc_doc_stages_history',
	@level2type = N'COLUMN', @level2name = N'doc_id'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Дата / время рассчета стадии',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'calc_doc_stages_history',
	@level2type = N'COLUMN', @level2name = N'insert_dt'
GO
exec sys.sp_addextendedproperty @name = N'MS_Description',
	@value = N'Идентификатор записи',
	@level0type = N'SCHEMA', @level0name = N'dbo',
	@level1type = N'TABLE',  @level1name = N'calc_doc_stages_history',
	@level2type = N'COLUMN', @level2name = N'rec_id'
GO
/* Grants */
grant insert on calc_doc_stages_history to role_oper_sched as dbo
GO