create procedure doc#commit_order_export
  (
    @doc_id      int,
    @order_type  varchar(3),
    @finish_date datetime = null
  )
as
  begin
    set nocount on

    declare @doc_cmp_anket_id int,
            @doc_sec_anket_id int,
            @cmp_code varchar(20),
            @sec_ndc_code varchar(64),
            @cpa_message_id int

    --таблица "control" - системный параметр «Создавать ИС по новым спискам на сайт»
    declare @autoisnew varchar(1) = (
      select varvalue
      from control
      where varname = 'AUTOISNEW'
        and systemid = 'SHL'
    )

    -- if (@order_type = '061') or (@order_type = '070')
    -- begin
    --
    --   update
    --     documents
    --   set
    --     doc_stage_mn='ИСП'
    --   where
    --     doc_id= @doc_id
    --
    --   return 0
    -- end

    set @doc_cmp_anket_id = null
    set @doc_sec_anket_id = null
    set @cpa_message_id = null

    select @doc_cmp_anket_id = dca.doc_id,
           @cmp_code = dca.cmp_code,
           @cpa_message_id = cim.message_id
    from doc_af005 d005
    join doc_cmp_ankets dca on d005.doc_cmp_anket_id = dca.doc_id
    left join cpa_incoming_messages as cim on cim.af005_doc_id = d005.doc_id
    where d005.doc_id = @doc_id

    select @doc_sec_anket_id = d.doc_sec_anket_id,
           @sec_ndc_code = a.sec_ndc_code
    from doc_af092 d
    join doc_sec_ankets a on d.doc_sec_anket_id = a.doc_id
    where d.doc_id = @doc_id

    set xact_abort on

    begin tran
    update documents
    set doc_stage_mn = 'ИСП'
    where doc_id = @doc_id

    update doc_orders
    set finish_date = @finish_date
    where doc_id = @doc_id

    declare @msglog varchar(max) = '@autoisnew=' + @autoisnew
    exec log_event 'doc#commit_order_export',
                   @doc_id,
                   @order_type,
                   @msglog

    -- обработка 70 поручения
    -- Если 70 поручение исполнено, то переводим в статус Исполнено документ Запрос на отмену сбора списка (NEW)
    if @order_type = '70'
      begin
        declare @source_doc_id int
        declare @is_order61_rermitted int = (
          select
          top 1 1
          from doc_af070 da70
          join doc_af061 da61 on da61.doc_id = da70.canceled_doc_id
          join corp_action2document ca2d on ca2d.action_id = da61.action_id
          where da70.doc_id = @doc_id
            and da61.is_new_tech = 'Y'
            and da61.list_type != 'SERV'
        )
        set @source_doc_id = (
          select
          top 1 dlrc.doc_id
          from doc_af070 da70
          join doc_af061 da61 on da61.doc_id = da70.canceled_doc_id
          join corp_action2document ca2d on ca2d.action_id = da61.action_id
          left join doc_list_req_cancellations dlrc on dlrc.source_doc_id = ca2d.doc_id
          where da70.doc_id = @doc_id
            and @is_order61_rermitted = 1
        )
        update documents
        set doc_stage_mn = 'ИСП'
        where doc_id = @source_doc_id

        if @autoisnew = 'Y'
          begin
            declare @msg varchar(max) = 'null; @source_doc_id=' + cast(@source_doc_id as varchar(16))
            exec log_event 'doc#commit_order_export',
                           @doc_id,
                           @order_type,
                           @msg

            if exists (
                select 1
                from documents as d
                join doc_af070 da on d.doc_id = da.doc_id
                where d.doc_id = @doc_id
                  and d.doc_type_id in (
                    select doc_type_id
                    from dbo.doc_types
                    where doc_type_mn in ('ПоручGF070')
                  )
                  and d.doc_stage_mn = 'ИСП'
              )
              and @is_order61_rermitted = 1
              and not exists (
                select top 1 1
                from corp_action2document ca2d
                join corp_action_messages cam on ca2d.action_id = cam.action_id
                join im_messages im on cam.message_id = im.message_id
                where ca2d.doc_id = @doc_id
                  and im.reason_mn = 'CANCEL_OWNERS_LIST'
              )
              begin
                -- сохранение поручения в очереди для автоматической отправки ИС «Об отмене составления списка»
                insert into im_doc_list_req_cancellation_queue (doc_id, order_type, order_status)
                values (@doc_id, 'ПоручGF070', 'NEW')
              end
          end
      end

    if @doc_cmp_anket_id is not null
      begin
        update d
        set doc_stage_mn = 'НДСТВ'
        from documents d
        inner join doc_cmp_ankets dca on d.doc_id = dca.doc_id
        where d.doc_stage_mn = 'ДСТВ'
          and dca.cmp_code = @cmp_code /*and
            d.doc_kind_id = 'O'*/

        update documents
        set doc_stage_mn = 'ДСТВ'
        where doc_id = @doc_cmp_anket_id

        /** Если есть привязанное сообщение из ЛКУ, то вызвать функцию обработки статуса 05 поручения */
        if @cpa_message_id is not null
          begin
            exec cpa_incoming_messages#process_status_af005 @af005_doc_id      = @doc_id,
                                                            @af005_status      = 'ИСП',
                                                            @af005_abort_cause = null
          end
      end

    if @doc_sec_anket_id is not null
      begin
        update d
        set d.doc_stage_mn = 'НДСТВ'
        from documents d
        inner join doc_sec_ankets dsa on d.doc_id = dsa.doc_id
          and sec_ndc_code = @sec_ndc_code
        where d.doc_stage_mn = 'ДСТВ'

        update documents
        set doc_stage_mn = 'ДСТВ'
        where doc_id = @doc_sec_anket_id

        /* Анкета была обновлена -> нужно перепроверить изменения в ц.б. */
        update smf
        set new_or_changed                 = 'N',
            is_new                         = 'N',
            basic_changed                  = 'N',
            ndc_code_changed               = 'N',
            issuer_ndc_code_changed        = 'N',
            security_type_changed          = 'N',
            hde_mode_changed               = 'N',
            facial_number_changed          = 'N',
            state_reg_num_and_date_changed = 'N',
            issue_volume_changed           = 'N',
            hde_state_changed              = 'N',
            taxes_changed                  = 'N',
            other_codes_changed            = 'N',
            service_status_changed         = 'N',
            base_security_changed          = 'N',
            calendar_changed               = 'N',
            add_issues_changed             = 'N',
            ca_status_changed              = 'N',
            manual_changes_count           = manual_changes_count + 1
        from security_modified_flags as smf
        join x_sbr as xs on xs.object_id = smf.object_id
          and xs.object_type = smf.object_type
        join doc_sec_ankets as dsa on dsa.sec_ndc_code = xs.ndc_code
        where dsa.doc_id = @doc_sec_anket_id
      end

    commit tran

    return 0
  end
go

/* Grants */
grant execute on doc#commit_order_export to ora_odi as dbo
go

grant execute on doc#commit_order_export to ora_srvbus as dbo
go

grant execute on doc#commit_order_export to role_oper as dbo
go