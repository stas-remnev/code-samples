create procedure corp_action_files#get_for_comparison
  (
    @from datetime,
    @to   datetime
  )
as
  begin
    set nocount on;

    select caf.action_file_id,
           caf.action_id,
           caf.status,
           caf.file_ftp_path,
           caf.file_path,
           caf.publish_date,
           caf.file_name,
           caf.extension,
           caf.file_name + caf.extension as file_full_name,
           case caf.status
             when 'Pub' then 'Опубликован'
             when 'New' then 'Новый'
             when 'Del' then 'Удален'
           end as status_ext,
           case caf.status_info
             when 'exec' then 'Выполнено'
             when 'notexec' then 'Не выполнено'
           end as status_info_ext,
           ca.url,
           ca.fin_instr_category,
           case
             when ca.fin_instr_category = 'R' then 'Росс'
             when ca.fin_instr_category = 'F' then 'Иностр'
             else null
           end as fin_instr_category_name,
           case
             when ca.action_date_fact is null then ca.action_date_plan
             when ca.action_date_fact is not null then ca.action_date_fact
           end as action_date_fact,
           ca.insert_dt [ca_insert_dt],
           ca.fix_date_plan,
           ca.ftpdir,
           fpfdc.file_body_hash [file_body_hash_daily_parameter],
           fpfdc.date_modify [cm_date_modify_daily_parameter],
           fpfwc.file_body_hash [file_body_hash_weekly_parameter],
           fpfwc.date_modify [cm_date_modify_weekly_parameter]
    from corp_actions ca
    join corp_action_files caf on caf.action_id = ca.action_id
    left join file_params_for_daily_checking fpfdc on caf.file_path = fpfdc.file_id
    left join file_params_for_weekly_checking fpfwc on caf.file_path = fpfwc.file_id
    where ca.insert_dt >= @from
      and (@to is null or ca.insert_dt < dateadd(day, 1, @to));
  end
go

/* Grants */
grant execute on corp_action_files#get_for_comparison to role_oper_sched as dbo
go