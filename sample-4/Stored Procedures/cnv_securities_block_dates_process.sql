create procedure cnv_securities_block_dates_process
  (
    @action_id      int,
    @object_id      int,
    @object_type    varchar(1),
    @operation_type varchar(1),
    @oper_date      datetime,
    @security_state varchar(1)
  )
as
  begin
    set nocount on;

    declare @tc int,
            @tran_name varchar(255)

    set @tc = @@trancount
    set @tran_name = 'cnv_securities_block_dates_process'

    declare @ndc_code varchar(64) = (
      select xs.ndc_code
      from x_sbr xs
      where xs.object_id = @object_id
        and xs.object_type = @object_type
    )
    if @tc = 0
      begin tran
    else
      save tran @tran_name

    begin try
      --  1.	При первичном заполнения поля «Дата приостановки операции»
      if not exists (
          select *
          from cnv_securities_block_dates_history ch
          where ch.action_id = @action_id
            and ch.object_id = @object_id
            and ch.object_type = @object_type
            and ch.operation_type = @operation_type
            and ch.oper_date is not null
        )
        and @oper_date is not null
        begin
          if (@operation_type = 'B')
            begin
              --    Если дата приостановки операции=текущий календарный день, то изменение в анкету вносится сразу в текущий календарный день
              if (cast(@oper_date as date) = cast(getdate() as date))
                begin
                  insert into create_security_anket_queue (object_id, object_type, action_id, security_state, operation_type, ndc_code, status, insert_dt, insert_user)
                  values (@object_id, @object_type, @action_id, @security_state, @operation_type, @ndc_code, 'NEW', default, default);

                  update hde_issues
                  set hde_mode = 'S'
                  where object_id = @object_id
                    and object_type = @object_type
                    and hde_mode != 'S'
                end
              else
              if (cast(@oper_date as date) > cast(getdate() as date))
                insert into cnv_securities_block_dates_queue (action_id, security_state, object_id, object_type, ndc_code, operation_type, hde_mode, oper_date, status)
                values (@action_id, @security_state, @object_id, @object_type, @ndc_code, 'B', 'S', @oper_date, 'NEW');
            end
          else
          if (@operation_type = 'U')
            begin
              --    Если дата приостановки операции=текущий календарный день, то изменение в анкету вносится сразу в текущий календарный день
              if (cast(@oper_date as date) = cast(getdate() as date))
                begin
                  insert into create_security_anket_queue (object_id, object_type, action_id, security_state, operation_type, ndc_code, status, insert_dt, insert_user)
                  values (@object_id, @object_type, @action_id, @security_state, @operation_type, @ndc_code, 'NEW', default, default);

                  update hde_issues
                  set hde_mode = 'A'
                  where object_id = @object_id
                    and object_type = @object_type
                    and hde_mode != 'A'
                end
              else
              if (cast(@oper_date as date) > cast(getdate() as date))
                insert into cnv_securities_block_dates_queue (action_id, security_state, object_id, object_type, ndc_code, operation_type, hde_mode, oper_date, status, insert_dt, insert_user)
                values (@action_id, @security_state, @object_id, @object_type, @ndc_code, 'U', 'A', @oper_date, 'NEW', default, default);
            end
        end

      insert into cnv_securities_block_dates_history (action_id, object_id, object_type, oper_date, operation_type)
      values (@action_id, @object_id, @object_type, @oper_date, @operation_type);

    end try
    begin catch

      if @tc = 0
        rollback tran
      else
        rollback tran @tran_name

      exec sys#rethrow_error

      return -1

    end catch

    if @tc = 0
      commit tran

    return 0
  end
go

/* Grants */
grant execute on cnv_securities_block_dates_process to [sql_corpdb_sp&write] as dbo
go