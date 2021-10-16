create procedure doc#create_if069G
  (
    @action_id   int,
    @create_mode char(1),
    @doc_id      int output,
    @ignore      int = 0
  )
as
  begin
    set nocount on

    declare @ERRORS table
      (
        error varchar(2000)
      )

    begin try

      declare @department_id int,
              @if069G_doc_type_id int,
              @doc_date datetime,
              @outer_trancount int,
              @internal_department_id int,
              @doc_form_id int,
              @action_type_cod_SWIFT varchar(7),
              @extension_mn varchar(30),
              @movement_attribute_extension_mn varchar(30)

      set @outer_trancount = @@TRANCOUNT

      set @if069G_doc_type_id = null
      set @internal_department_id = null

      select @if069G_doc_type_id = doc_type_id,
             @internal_department_id = default_internal_department_id
      from doc_types
      where doc_type_class_mn = 'ORDER_IF69_G'

      if @if069G_doc_type_id is null
        begin
          raiserror ('Не найден тип документа "Поручение IF069/G"', 16, 1)
          return -1
        end

      select @action_type_cod_SWIFT = action_type_cod_SWIFT,
             @extension_mn = c_a_t_e.extension_mn,
             @movement_attribute_extension_mn = c_a_t_e_mv.extension_mn
      from corp_actions as c_a
      join corp_action_types as c_t on c_a.corp_action_type_id = c_t.corp_action_type_id
      left join corp_action_type_extensions as c_a_t_e on c_a.extension_id = c_a_t_e.extension_id
      left join corp_action_type_extensions as c_a_t_e_mv on c_a.movement_attribute_extension_id = c_a_t_e_mv.extension_id
      where c_a.action_id = @action_id

      set @doc_date = dbo.date_only(getdate())

      set @department_id = null

      select @department_id = department_id
      from s_users
      where sysuser_name_full = dbo.modify_user_name(suser_sname())

      if @department_id is null
        set @department_id = 1

      -- Форма док-та
      select @doc_form_id = doc_form_id
      from doc_forms
      where doc_form_short_name = 'ЭлДок'
      if @doc_form_id is null
        begin
          raiserror ('Не найдена форма документа с типом "Электронный документ"', 16, 1)
          return -1
        end

      set xact_abort on

      if @outer_trancount = 0
        begin tran doc_create_if069G
      else
        save tran doc_create_if069G

      begin /*Набираем данные во временные таблицы*/
        --поручение
        declare @code_type int

        select @code_type = cmp_code_type_id
        from cmp_code_types
        where cmp_code_type_mn = 'NDC'

        declare @obj_id int
        declare @obj_type char(1)
        select top 1 @obj_id = object_id,
                     @obj_type = object_type
        from corp_action2instr as cai
        where action_id = @action_id

        select top 1 @action_id as action_id,
                     corp_action_type_id,
                     extension_id,
                     action_date_plan,
                     reestr_dt,
                     egrul_date,
                     fix_date_plan,
                     registrar_dep_code,
                     issr_dep_code,
                     registrar_short_name,
                     issr_short_name,
                     fin_instr_category,
                     is_ready,
                     is_partial_cancel_mode,
                     indispensibility_attrib,
                     lockreestr_date,
                     lock_reest,
                     locknrd_date,
                     num_securities
        into #orders
        from (
          select ca.corp_action_type_id,
                 ca.extension_id,
                 ca.action_date_plan,
                 case
                   when cat.action_type_cod_swift in ('MRGR', 'SOFF') or
                     (cat.action_type_cod_swift in ('EXOF') and @extension_mn is not null and @extension_mn in ('RRGN', 'STEX')) then cs.source_fkcb_date
                   when cat.action_type_cod_SWIFT in ('PARI', 'PARI/JN', 'PARI/UN') then conv.reestr_dt
                   else conv.fkcb_date
                 end as reestr_dt,
                 case
                   when cat.action_type_cod_SWIFT in ('PARI', 'PARI/JN', 'PARI/UN') then null
                   else conv.egrul_date
                 end as egrul_date,
                 case
                   when cat.action_type_cod_SWIFT in ('BONU', 'MRGR', 'SOFF') or
                     (cat.action_type_cod_swift in ('EXOF') and @extension_mn not in ('SHEX')) then conv.reestr_dt
                   else null
                 end as fix_date_plan,
                 registrators_codes.cmp_code as registrar_dep_code,
                 issuers_codes.cmp_code as issr_dep_code,
                 registrators.short_name as registrar_short_name,
                 issuers.short_name as issr_short_name,
                 ca.fin_instr_category,
                 conv.is_ready,
                 null as is_partial_cancel_mode,
                 ca.indispensibility_attrib,
                 null as lockreestr_date,
                 null as lock_reest,
                 null as locknrd_date,
                 null as num_securities
          from corp_actions as ca
          join corp_action_types as cat on cat.corp_action_type_id = ca.corp_action_type_id
          join conversions as conv on ca.action_id = conv.action_id
          join companies as issuers on conv.issuer_id = issuers.company_id
          left join cnv_securities as cs on conv.action_id = cs.action_id
          left join storage s on s.object_id = @obj_id
            and s.object_type = @obj_type
            and s.is_main = 'Y'
          left join companies as registrators on s.storage_id = registrators.company_id
          left join cmp_codes as issuers_codes on issuers.company_id = issuers_codes.company_id
            and issuers_codes.cmp_code_type_id = @code_type
          left join cmp_codes as registrators_codes on registrators.company_id = registrators_codes.company_id
            and registrators_codes.cmp_code_type_id = @code_type
          where ca.action_id = @action_id

          union all

          select ca.corp_action_type_id,
                 ca.extension_id,
                 ca.action_date_plan,
                 caopp.reestr_dt,
                 null as egrul_date,
                 null as fix_date_plan,
                 registrators_codes.cmp_code as registrar_dep_code,
                 issuers_codes.cmp_code as issr_dep_code,
                 registrators.short_name as registrar_short_name,
                 issuers.short_name as issr_short_name,
                 ca.fin_instr_category,
                 caopp.is_ready as is_ready,
                 null as is_partial_cancel_mode,
                 ca.indispensibility_attrib,
                 caopp.lockreestr_date,
                 caopp.lock_reest,
                 caopp.locknrd_date,
                 caopp.num_securities
          from corp_actions as ca
          join corp_action_types as cat on cat.corp_action_type_id = ca.corp_action_type_id
          join corp_action_other_pai_part_redemption as caopp on ca.action_id = caopp.action_id
          left join storage s on s.object_id = @obj_id
            and s.object_type = @obj_type
            and s.is_main = 'Y'
          left join companies as registrators on s.storage_id = registrators.company_id
          left join x_sbr on x_sbr.object_id = @obj_id
            and x_sbr.object_type = @obj_type
          left join companies as issuers on x_sbr.issuer_id = issuers.company_id
          left join cmp_codes as issuers_codes on issuers.company_id = issuers_codes.company_id
            and issuers_codes.cmp_code_type_id = @code_type
          left join cmp_codes as registrators_codes on registrators.company_id = registrators_codes.company_id
            and registrators_codes.cmp_code_type_id = @code_type
          where ca.action_id = @action_id
        ) as q

        if (@action_type_cod_swift in ('WRTH')
          or (@action_type_cod_swift in ('REDM')
          and @extension_mn = 'UNTR'))
          begin

            select top 1 @action_id as action_id,
                         ca.corp_action_type_id,
                         ca.extension_id,
                         ca.action_date_plan,
                         case
                           when @action_type_cod_swift in ('WRTH') then dd.reestr_dt
                           when (@action_type_cod_swift in ('REDM') and @extension_mn = 'UNTR') then up.reestr_dt
                           else null
                         end as reestr_dt,
                         dd.egrul_date as egrul_date,
                         null as fix_date_plan,
                         registrators_codes.cmp_code registrar_dep_code,
                         issuers_codes.cmp_code issr_dep_code,
                         registrators.short_name registrar_short_name,
                         issuers.short_name issr_short_name,
                         ca.fin_instr_category,
                         case
                           when @action_type_cod_swift in ('WRTH') then dd.is_ready
                           when (@action_type_cod_swift in ('REDM') and @extension_mn = 'UNTR') then up.is_ready
                         end as is_ready,
                         up.is_partial_cancel_mode,
                         ca.indispensibility_attrib,
                         caopp.lockreestr_date,
                         caopp.lock_reest,
                         caopp.locknrd_date,
                         caopp.num_securities
            into #orders_isu
            from corp_actions ca
            join corp_action_types as cat on cat.corp_action_type_id = ca.corp_action_type_id
            join corp_action2instr ca1 on ca.action_id = ca1.action_id
            join x_sbr xs on xs.object_id = ca1.object_id
              and xs.object_type = ca1.object_type
            join companies issuers on xs.issuer_id = issuers.company_id
            left join cmp_codes issuers_codes on issuers.company_id = issuers_codes.company_id
              and issuers_codes.cmp_code_type_id = @code_type
            left join storage s on s.object_id = xs.object_id
              and s.object_type = xs.object_type
              and s.is_main = 'Y'
            left join cmp_codes registrators_codes on s.storage_id = registrators_codes.company_id
              and registrators_codes.cmp_code_type_id = @code_type
            left join companies registrators on s.storage_id = registrators.company_id
            left join corp_actions_xml cax on cax.action_id = ca.action_id
            left join unit_payments up on up.action_id = ca.action_id
            left join discardings dd on dd.action_id = ca.action_id
            left join corp_action_other_pai_part_redemption as caopp on ca.action_id = caopp.action_id
            where ca.action_id = @action_id
          end;

        --ценные бумаги
        select sec_type,
               x_sbr.isin,
               hde_issues.hde_short_name as issue_short_name,
               coalesce(cnv_sec.state_reg_number, x_sbr.state_reg_number) as state_reg_number,
               x_sbr.ndc_code,
               x_dr.ndc_code as ndc_code_frac,
               cnv_sec.issue_desc,
               cnv_sec.numerator_factor,
               cnv_sec.denominator_factor,
               cnv_sec.part_percent,
               cnv_sec.frctn_dspstn,
               cnv_sec.add_zr_rls,
               cnv_sec.slctd_rndg_nb,
               cnv_sec.rndg_rls_addtl_inf,
               cnv_sec.security_movement,
               cnv_sec.reestr_dt,
               cast(null as decimal(24, 2)) as part_redemption_percent
        into #issues
        from (
          -- source - базовые
          select top 1 'B' as sec_type,
                       null as issue_desc,
                       source_numerator as numerator_factor,
                       source_denominator as denominator_factor,
                       source_object_id as object_id,
                       source_object_type as object_type,
                       source_dr_object_id as dr_object_id,
                       source_dr_object_type as dr_object_type,
                       source_frctn_dspstn as frctn_dspstn,
                       source_add_zr_rls as add_zr_rls,
                       source_slctd_rndg_nb as slctd_rndg_nb,
                       source_rndg_rls_addtl_inf as rndg_rls_addtl_inf,
                       null as state_reg_number,
                       part_percent,
                       case
                         when @action_type_cod_swift in ('MRGR', 'SOFF', 'EXOF') then isnull(source_move, 'N')
                         when @action_type_cod_swift in ('BONU') then 'N'
                         else 'Y'
                       end as security_movement,
                       case
                         when @action_type_cod_swift in ('MRGR', 'SOFF', 'EXOF') then source_fkcb_date
                         else cc.reestr_dt
                       end as reestr_dt
          from cnv_securities
          left join conversions cc on cnv_securities.action_id = cc.action_id
          where cnv_securities.action_id = @action_id

          union

          -- target - новые
          select 'N' as sec_type,
                 target_desc as issue_desc,
                 target_numerator as numerator_factor,
                 target_denominator as denominator_factor,
                 target_object_id as object_id,
                 target_object_type as object_type,
                 target_dr_object_id as dr_object_id,
                 target_dr_object_type as dr_object_type,
                 target_frctn_dspstn as frctn_dspstn,
                 target_add_zr_rls as add_zr_rls,
                 target_slctd_rndg_nb as slctd_rndg_nb,
                 target_rndg_rls_addtl_inf as rndg_rls_addtl_inf,
                 target_state_reg_number as state_reg_number,
                 null as part_percent,
                 case
                   when @action_type_cod_swift in ('MRGR', 'SOFF', 'EXOF') then isnull(target_move, 'N')
                   when @action_type_cod_swift in ('BONU') then 'Y'
                   else 'Y'
                 end as security_movement,
                 case
                   when @action_type_cod_swift in ('MRGR', 'SOFF', 'EXOF') then target_fkcb_date
                   else cc.reestr_dt
                 end as reestr_dt
          from cnv_securities
          left join conversions cc on cnv_securities.action_id = cc.action_id
          where cnv_securities.action_id = @action_id
        ) cnv_sec
        left join x_sbr on cnv_sec.object_id = x_sbr.object_id
          and cnv_sec.object_type = x_sbr.object_type
        left join x_sbr as x_dr on cnv_sec.dr_object_id = x_dr.object_id
          and cnv_sec.dr_object_type = x_dr.object_type
        left join hde_issues on cnv_sec.object_id = hde_issues.object_id
          and cnv_sec.object_type = hde_issues.object_type

        -- для КД OTHR без признака движения
        -- те, что с признаком, работают как конвертации, выбираются предыдущим запросом
        if @action_type_cod_swift in ('OTHR')
          and @movement_attribute_extension_mn is null
          begin

            insert into #issues (sec_type, isin, issue_short_name, state_reg_number, ndc_code, ndc_code_frac, issue_desc, numerator_factor, denominator_factor, part_percent, frctn_dspstn, add_zr_rls, slctd_rndg_nb, rndg_rls_addtl_inf, security_movement, reestr_dt, part_redemption_percent)
            select top 1 'B' as sec_type,
                         x_sbr.isin,
                         hde_issues.hde_short_name as issue_short_name,
                         x_sbr.state_reg_number,
                         x_sbr.ndc_code,
                         null as ndc_code_frac,
                         null as issue_desc,
                         null as numerator_factor,
                         null as denominator_factor,
                         null as part_percent,
                         p_p_r.frctn_dspstn,
                         null as add_zr_rls,
                         null as slctd_rndg_nb,
                         null as rndg_rls_addtl_inf,
                         'Y' as security_movement,
                         p_p_r.reestr_dt,
                         p_p_r.part_percent as part_redemption_percent
            from corp_action2instr as ca2i
            join x_sbr on ca2i.object_id = x_sbr.object_id
              and ca2i.object_type = x_sbr.object_type
            left join corp_action_other_pai_part_redemption as p_p_r on ca2i.action_id = p_p_r.action_id
            left join hde_issues on ca2i.object_id = hde_issues.object_id
              and ca2i.object_type = hde_issues.object_type
            where ca2i.action_id = @action_id

          end

        if (@action_type_cod_swift in ('WRTH')
          or (@action_type_cod_swift in ('REDM')
          and @extension_mn = 'UNTR'))
          begin

            select top 1 sec_type,
                         xs.isin,
                         hde_issues.hde_short_name issue_short_name,
                         xs.state_reg_number as state_reg_number,
                         xs.ndc_code,
                         case
                           when @action_type_cod_swift in ('WRTH') then xs_dr.ndc_code
                           else null
                         end ndc_code_frac,
                         isu_sec.issue_desc,
                         isu_sec.numerator_factor,
                         isu_sec.denominator_factor,
                         isu_sec.part_percent,
                         isu_sec.frctn_dspstn,
                         isu_sec.add_zr_rls,
                         isu_sec.slctd_rndg_nb,
                         isu_sec.rndg_rls_addtl_inf,
                         isu_sec.security_movement,
                         isu_sec.reestr_dt
            into #issues_isu
            from (
              -- source - базовые
              select top 1 'B' as sec_type,
                           null as issue_desc,
                           null as numerator_factor,
                           null as denominator_factor,
                           object_id as object_id,
                           object_type as object_type,
                           null as dr_object_id,
                           null as dr_object_type,
                           null as frctn_dspstn,
                           null as add_zr_rls,
                           null as slctd_rndg_nb,
                           null as rndg_rls_addtl_inf,
                           null as state_reg_number,
                           null as part_percent,
                           'Y' as security_movement,
                           case
                             when @action_type_cod_swift in ('WRTH') then dd.reestr_dt
                             when (@action_type_cod_swift in ('REDM') and @extension_mn = 'UNTR') then up.reestr_dt
                             else null
                           end as reestr_dt
              from corp_action2instr cai
              left join unit_payments up on up.action_id = cai.action_id
              left join discardings dd on dd.action_id = cai.action_id
              where cai.action_id = @action_id
            ) isu_sec
            left join x_sbr xs on isu_sec.object_id = xs.object_id
              and isu_sec.object_type = xs.object_type
            left join hde_issues on isu_sec.object_id = hde_issues.object_id
              and isu_sec.object_type = hde_issues.object_type
            left join issues i on xs.issue_id = i.int_issue_id
            left join x_sbr xs_dr on xs_dr.issue_id = i.issue_id
          end;

        -- связанные КД
        select ca_links.action_id,
               ca_links.link_type
        into #corp_action_links
        from (
          select l.action_to_id as action_id,
                 l.link_type as link_type
          from corp_action_links as l
          where l.action_from_id = @action_id

          union

          select l.action_from_id as action_id,
                 case
                   when l.link_type = 'AFTE' then 'BEFO'
                   when l.link_type = 'BEFO' then 'AFTE'
                 end link_type
          from corp_action_links as l
          where l.action_to_id = @action_id
        ) as ca_links
      end

      begin /* Делаем проверки */

        declare @main_issues_codes varchar(max),
                @dr_issues_codes varchar(max)

        -- 1. Корпоративное действие может находиться только в состоянии «Не состоялось».
        if not exists (
            select 1
            from corp_actions
            where action_id = @action_id
              and action_status = 'N'
          )
          insert into @ERRORS
          values (replace('Корпоративное действие @action_id находится в состоянии, отличном от «Не состоялось»', '@action_id', @action_id))

        if (@action_type_cod_swift in ('WRTH', 'OTHR')
          or (@action_type_cod_swift in ('REDM')
          and @extension_mn = 'UNTR'))
          begin
            -- 2. Проверяется наличие базовой ц/б
            if not exists (
                select top 1 1
                from corp_action2instr cai
                where cai.action_id = @action_id
              )
              insert into @ERRORS
              values ('Не указана базовая ценная бумага')
          end
        else
          begin
            -- 2. Проверяется наличие базовой ц/б в таблице с размещенным выпуском в КД
            if not exists (
                select top 1 1
                from cnv_securities cs
                join x_sbr x on cs.source_object_id = x.object_id
                  and cs.source_object_type = x.object_type
                where cs.action_id = @action_id
                  and cs.source_object_id is not null
                  and cs.source_object_type is not null
                  and x.ndc_code is not null
              )
              insert into @ERRORS
              values ('Не указана базовая ценная бумага')
          end;

        -- 3. Проверяется тип финансовых инструментов (российские или иностранные), связанных с КД. Поле «Росс. /Иностр.» на закладке «ФИ» должно содержать значение "Российские".
        if not exists (
            select 1
            from corp_actions
            where action_id = @action_id
              and fin_instr_category = 'R'
          )
          insert into @ERRORS
          values ('Некорректный тип финансовых инструментов. Поручение 69/G может быть создано только по российским ц.б.')

        select i.issue_id,
               x.ndc_code,
               f.f_issue_id,
               f.f_ndc_code,
               g.g_issue_id,
               g.g_ndc_code,
               case
                 when i.int_issue_id is not null then 'N'
                 else 'Y'
               end as is_g_issue,
               case
                 when i_s.issue_state_name_mn in ('В размещ', 'Размещен') then 'Y'
                 else 'N'
               end as is_active
        into #t
        from corp_action2instr ca2i
        join x_sbr x on x.object_id = ca2i.object_id
          and x.object_type = ca2i.object_type
        join issues i on x.issue_id = i.issue_id
        join issue_states i_s on i.issue_state_id = i_s.issue_state_id
        --ищем дробный выпуск по основному выпуску
        outer apply (
          select i_f.issue_id as f_issue_id,
                 x.ndc_code as f_ndc_code
          from issues as i_f
          join x_sbr as x on i_f.issue_id = x.issue_id
          where i_f.int_issue_id = i.issue_id
            and x.is_served = 'Y'
        ) f
        --ищем основной выпуск по дробному
        outer apply (
          select i_g.issue_id as g_issue_id,
                 x.ndc_code as g_ndc_code
          from issues as i_g
          join x_sbr as x on i_g.issue_id = x.issue_id
          where i_g.issue_id = i.int_issue_id
            and x.is_served = 'Y'
        ) g
        where ca2i.action_id = @action_id;

        -- 4. Если в связанных бумагах для КД указан выпуск, для которого есть действующий дробный выпуск, то этот дробный тоже должен быть привязан к КД.
        insert into @ERRORS
        select replace
          (
          replace
          (
          replace('В корпоративном действии @action_id не привязан дробный выпуск @f_ndc_code для основного выпуска @g_ndc_code', '@action_id', @action_id),
          '@f_ndc_code',
          coalesce(t.f_ndc_code, '')
          ),
          '@g_ndc_code',
          coalesce(t.ndc_code, '')
          ) as error
        from #t as t
        join x_sbr as x on x.issue_id = t.f_issue_id
        left join corp_action2instr as ca2i on ca2i.object_id = x.object_id
          and ca2i.object_type = x.object_type
          and ca2i.action_id = @action_id
        where t.is_g_issue = 'Y'
          and
          --есть дробная часть
          t.f_issue_id is not null
          and
          --но не привязана к кд
          ca2i.action_id is null;

        -- 5. Если в связанных бумагах для КД указан действующий дробный выпуск, то основной тоже должен быть привязан к КД.
        insert into @ERRORS
        select replace
          (
          replace
          (
          replace('В корпоративном действии @action_id не привязан основной выпуск @g_ndc_code для дробного выпуска @f_ndc_code', '@action_id', @action_id),
          '@f_ndc_code',
          coalesce(t.ndc_code, '')
          ),
          '@g_ndc_code',
          coalesce(t.g_ndc_code, '')
          ) as error
        from #t as t
        join x_sbr as x on x.issue_id = t.g_issue_id
        left join corp_action2instr as ca2i on ca2i.object_id = x.object_id
          and ca2i.object_type = x.object_type
          and ca2i.action_id = @action_id
        where	--если это дробный выпуск
          t.is_g_issue = 'N'
          and
          --но его основной выпуск не привязан к кд
          ca2i.action_id is null;

        -- 6. Если в связанных бумагах для КД у основного выпуска указан только НЕдействующий дробный выпуск, то выводится сообщение «В корпоративном действии <Референс КД> указан недействующий дробный выпуск <Код НРД дробного>»
        insert into @ERRORS
        select replace
          (
          replace('В корпоративном действии @action_id указан недействующий дробный выпуск @f_ndc_code', '@action_id', @action_id),
          '@f_ndc_code',
          coalesce(t.ndc_code, '')
          ) as error
        -- дробные
        from #t as t
        -- основные, у которых нет действующих дробных
        join (
          select distinct issue_id
          from #t as t1
          where t1.is_g_issue = 'Y'
            and (
              select count(*)
              from #t as t2
              where t2.g_issue_id = t1.issue_id
                and t2.is_active = 'Y'
            ) = 0
        ) t2 on t.g_issue_id = t2.issue_id

        -- 7. Если в КД INCR, DECR, CONV, SPLF, SPLR, BONU размещаемый выпуск определен, то должна быть указана дата, определенная решением о выпуске.
        if @action_type_cod_SWIFT in ('INCR', 'DECR', 'CONV', 'SPLF', 'SPLR', 'BONU')
          and exists (
            select top 1 1
            from cnv_securities
            where action_id = @action_id
              and target_object_id is not null
              and target_object_type is not null
          )
          and not exists (
            select 1
            from conversions
            where action_id = @action_id
              and nullif(egrul_date, '21110101') is not null
          )
          insert into @ERRORS
          values ('Не указана дата, определенная решением о выпуске')

        -- 8.	Если в КД BONU, SOFF с подтипом SPNF размещаемый выпуск определен, то должна быть указана дата фиксации. Иначе выдается ошибка «Не указана дата фиксации».
        if @action_type_cod_SWIFT in ('BONU')
          and exists (
            select top 1 1
            from cnv_securities
            where action_id = @action_id
              and target_object_id is not null
              and target_object_type is not null
          )
          and not exists (
            select top 1 *
            from conversions
            where action_id = @action_id
              and reestr_dt is not null
          )
          insert into @ERRORS
          values ('Не указана дата фиксации')

        --9.	При создании поручения в случае отсутствия какого-либо из обязательных параметров поручения 69/G выдается сообщение об ошибке.
        --Ошибка: «Значение обязательного параметра <Наименование параметра> отсутствует. Создание поручения 69/G невозможно».
        begin
          declare @tmpStr varchar(max) = 'Значение обязательного параметра @param отсутствует. Создание поручения 69/G невозможно'

          if exists (
              select 1
              from #orders
              where corp_action_type_id is null
            )
            insert into @ERRORS
            values (replace(@tmpStr, '@param', 'Тип КД'))

          if exists (
              select 1
              from #orders
              where action_id is null
            )
            insert into @ERRORS
            values (replace(@tmpStr, '@param', 'Референс КД'))

          if exists (
              select 1
              from #orders
              where action_date_plan is null
            )
            insert into @ERRORS
            values (replace(@tmpStr, '@param', 'Дата КД'))

          if exists (
              select 1
              from #orders
              where issr_dep_code is null
            )
            insert into @ERRORS
            values (replace(@tmpStr, '@param', 'Эмитент'))

          if exists (
              select 1
              from #orders
              where fin_instr_category is null
            )
            insert into @ERRORS
            values (replace(@tmpStr, '@param', 'Признак ц/б'))

          if exists (
              select top 1 1
              from #issues
              where sec_type is null
            )
            insert into @ERRORS
            values (replace(@tmpStr, '@param', 'Тип ц/б'))

        end

        --      10.	Для КД CONV должен быть указан подтип, отличный от значения «0-Не определено». Иначе выдается ошибка «Не указан подтип КД».
        if @action_type_cod_SWIFT in ('CONV')
          and exists (
            select top 1 1
            from corp_actions ca
            where ca.action_id = @action_id
              and ca.extension_id is null
          )
          insert into @ERRORS
          values ('Не указан подтип КД')

        if exists (select 1 from @ERRORS)
          begin
            select @doc_id = -1
            if (select count(*) from @ERRORS) > 1
              update @ERRORS
              set error = '• ' + error
            select * from @ERRORS
            return;
          end
      end

      begin /*Сохраняем поручение в постоянные таблицы*/

        -- Вставка в основную таблицу с документами
        insert documents (adm_dep, department_id, doc_date, doc_kind_id, doc_form_id, doc_type_id, person_id, storage_place_id, doc_stage_mn, internal_department_id)
        values ('N', @department_id, @doc_date, 'O', @doc_form_id, @if069G_doc_type_id, dbo.get_ndc_id(), dbo.get_ndc_id(), null /*'СФРМ'*/, @internal_department_id)

        set @doc_id = scope_identity()

        -- Вставка в таблицу с отправленными данными в Аламеду
        insert doc_orders (doc_id, create_mode)
        values (@doc_id, @create_mode)

        if (@action_type_cod_swift in ('WRTH')
          or (@action_type_cod_swift in ('REDM')
          and @extension_mn = 'UNTR'))
          begin
            -- Вставка данных в таблицу с 69-ым поручением
            insert doc_if069G (doc_id, action_id, ca_type_id, corp_action_subtype, ca_date, reestr_dt, egrul_date, fix_date, registrar_dep_code, issr_dep_code, registrar_short_name, issr_short_name, fin_instr_category, is_ready, is_partial_cancel_mode, indispensibility_attrib, reestr_block_dt, reestr_block_set, ndc_block_dt, blocked_sec_count)
            select top 1 @doc_id,
                         action_id,
                         corp_action_type_id,
                         extension_id,
                         action_date_plan,
                         reestr_dt,
                         egrul_date,
                         fix_date_plan,
                         isnull(registrar_dep_code, 'N'),
                         issr_dep_code,
                         registrar_short_name,
                         issr_short_name,
                         fin_instr_category,
                         is_ready,
                         is_partial_cancel_mode,
                         indispensibility_attrib,
                         lockreestr_date,
                         isnull(lock_reest, 'N'),
                         locknrd_date,
                         num_securities
            from #orders_isu
          end
        else
          begin
            -- Вставка данных в таблицу с 69-ым поручением
            insert doc_if069G (doc_id, action_id, ca_type_id, corp_action_subtype, ca_date, reestr_dt, egrul_date, fix_date, registrar_dep_code, issr_dep_code, registrar_short_name, issr_short_name, fin_instr_category, is_ready, is_partial_cancel_mode, indispensibility_attrib, reestr_block_dt, reestr_block_set, ndc_block_dt, blocked_sec_count)
            select top 1 @doc_id,
                         action_id,
                         corp_action_type_id,
                         extension_id,
                         action_date_plan,
                         reestr_dt,
                         egrul_date,
                         fix_date_plan,
                         isnull(registrar_dep_code, 'N'),
                         issr_dep_code,
                         registrar_short_name,
                         issr_short_name,
                         fin_instr_category,
                         is_ready,
                         is_partial_cancel_mode,
                         indispensibility_attrib,
                         lockreestr_date,
                         isnull(lock_reest, 'N'),
                         locknrd_date,
                         num_securities
            from #orders
          end;

        -- Добавляем связь КД и документ
        insert into corp_action2document (action_id, doc_id, is_basis, inserted_automatically)
        values (@action_id, @doc_id, 'N', 'Y')

        if (@action_type_cod_swift in ('WRTH')
          or (@action_type_cod_swift in ('REDM')
          and @extension_mn = 'UNTR'))
          begin
            --ценные бумаги
            insert into doc_if069G_issues (doc_id, sec_type, isin, issue_short_name, state_reg_number, ndc_code, ndc_code_frac, issue_desc, numerator_factor, denominator_factor, part_percent, frctn_dspstn, add_zr_rls, slctd_rndg_nb, rndg_rls_addtl_inf, reestr_dt, security_movement)
            select @doc_id,
                   sec_type,
                   isin,
                   issue_short_name,
                   state_reg_number,
                   ndc_code,
                   ndc_code_frac,
                   issue_desc,
                   numerator_factor,
                   denominator_factor,
                   part_percent,
                   frctn_dspstn,
                   add_zr_rls,
                   slctd_rndg_nb,
                   rndg_rls_addtl_inf,
                   reestr_dt,
                   security_movement
            from #issues_isu
          end
        else
          begin
            --ценные бумаги
            insert into doc_if069G_issues (doc_id, sec_type, isin, issue_short_name, state_reg_number, ndc_code, ndc_code_frac, issue_desc, numerator_factor, denominator_factor, part_percent, frctn_dspstn, add_zr_rls, slctd_rndg_nb, rndg_rls_addtl_inf, reestr_dt, security_movement, part_redemption_percent)
            select @doc_id,
                   sec_type,
                   isin,
                   issue_short_name,
                   state_reg_number,
                   ndc_code,
                   ndc_code_frac,
                   issue_desc,
                   numerator_factor,
                   denominator_factor,
                   part_percent,
                   frctn_dspstn,
                   add_zr_rls,
                   slctd_rndg_nb,
                   rndg_rls_addtl_inf,
                   reestr_dt,
                   security_movement,
                   part_redemption_percent
            from #issues
          end;

        -- внешние референсы КД
        insert into doc_if069_linked_corp_actions (doc_id, action_id, link_type)
        select @doc_id,
               action_id,
               link_type
        from #corp_action_links
      end

      if @outer_trancount = 0
        commit tran doc_create_if069G
      else
        save tran doc_create_if069G

      set nocount off

      return 0

    end try
    begin catch

      insert into @ERRORS
      select ('Exception was thrown. ErrorNumber = ' + cast(error_number() as varchar(20)) + '. ErrorMessage = ' + error_message())

      select @doc_id = -1
      select * from @ERRORS
      if @outer_trancount = 0
        rollback tran doc_create_if069G

      return;

    end catch

  end
go

/* Grants */
grant execute on doc#create_if069g to role_oper_sched as dbo
go

grant execute on doc#create_if069g to [SQL_corpdb_sp&write] as dbo
go