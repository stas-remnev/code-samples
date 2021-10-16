using System;
using Nsd.Repository.Base;
using Nsd.Repository.Ef.StorageProcedureEntities.Lei;
using System.Collections.Generic;
using System.Linq;
using Nsd.Repository.Ef.Model.Entities;
using LinqToDB.Data;
using Nsd.Common;
using LinqToDB;
using LinqToDB.Common;
using Nsd.Common.Extensions;

namespace Nsd.Repository.Ef.Repositories.Lei.Extensions
{
    public static class LeiPublicExtensions
    {
        public static List<LeiPublicChanges> LeiPublicGetChanges(this IStoredProcedureRepository repository)
        {
            return repository.QueryProc<LeiPublicChanges>("lei_public#get_changes", TimeSpan.FromMinutes(5), new { lei_code_status = (string)null, need_check_8 = "Y" })
                .ToList();
        }
        public static List<LeiPublicLevel2Changes> LeiPublicLevel2GetChanges(this IStoredProcedureRepository repository)
        {
            // Вызываем именно lei_public_Level2_update с флагом fixation_mode = "N". В этой ХП данные дополнительно обрабатывается, Чтобы получить конечную версию данных, внутри происходит перехват данных без фиксации.
            return repository.QueryProc<LeiPublicLevel2Changes>("lei_public_level2_update", TimeSpan.FromMinutes(15), new { fixation_mode = "N" }).ToList();
        }

        /// <summary>
        /// Загружает данные по записям для проверки на дубликаты из Level1 (Журнал Level1, Закладка ЗГКО, активный чекбокс "Показать записи для проверки на дубликаты")
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static LeiPublicChanges GetDuplicateCheckRecordsLevel1(this IStoredProcedureRepository repository, int? companyId = null)
        {
            if (companyId != null)
            {
                return repository.QueryProc<LeiPublicChanges>("lei_check_duplicate_records#get_list", TimeSpan.FromMinutes(5), new { company_id = companyId }).FirstOrDefault();
            }
            return null;
        }

        public static List<LeiPublicLevel2PreCheckModel> GetLeiPublicLevel2PreCheck(this IRepository<LeiPublicLevel2PreCheck> repository, List<int> companyIds)
        {
            return repository.Query<LeiPublicLevel2PreCheckModel>($"select * from lei_public_level2_pre_check where company_id in ({String.Join(", ", companyIds.ToArray())})", null)
                .ToList();
        }

        public static List<LeiPreCheckModel> GetLeiPreCheck(this IRepository<LeiPreCheck> repository, List<int> companyIds)
        {
            return repository.Query<LeiPreCheckModel>($@"
                select lei_pre_check_id,
                       lei_code,
                       company_id,
                       insert_dt,
                       insert_user,
                       request_initiator,
                       send_date,
                       request_author,
                       status,
                       response_date,
                       response,
                       response_status,
                       response_status_desc,
                       response_status_details,
                       send_permission_type,
                       send_permission_initiator,
                       send_permission_date,
                       manual_confirmation,
                       manual_confirmation_initiator,
                       manual_confirmation_date,
                       auto_confirmation,
                       comment
                from lei_pre_check where company_id in ({String.Join(", ", companyIds.ToArray())})", null)
                .ToList();
        }

        public static LeiPublicLevel2Changes GetChangesForPendingValidation(this IStoredProcedureRepository repository, string leiCode, string linkAttrib)
        {
            DataParameter[] dataParameters = new DataParameter[]
            {
                new DataParameter("lei_code", leiCode),
                new DataParameter("link_attrib", linkAttrib)
            };

            return repository.QueryProc<LeiPublicLevel2Changes>("lei_public_level2_pre_check#get_changes_for_pending_validation", TimeSpan.FromMinutes(5), dataParameters)
                .FirstOrDefault<LeiPublicLevel2Changes>();
        }

        public static void SetLeiPreCheckId(this IRepository<LeiPublicLevel2PreCheck> repository, int leiPreCheckId,
            Guid uId, int companyId, int? level1Id = null, List<int> level1NamesIds = null, List<int> level2NamesIds = null)
        {

            if (!level1Id.IsNullOrWhitespace())
                repository.Execute($@"
            update lei_public_pre_check
            set lei_pre_check_id = {leiPreCheckId}
            where lei_code_id = {level1Id}");


            repository.Execute($@"
            update lei_public_level2_pre_check
            set lei_pre_check_id = {leiPreCheckId}
            where u_id = '{uId}' and company_id = {companyId}");

            if (!level1NamesIds.IsNullOrEmpty())
                repository.Execute($@"
            update lei_public_names_pre_check
            set lei_pre_check_id = {leiPreCheckId}
            where rec_id in ({String.Join(", ", level1NamesIds.ToArray())})");

            if (!level2NamesIds.IsNullOrEmpty())
                repository.Execute($@"
            update lei_public_names_level2_pre_check
            set lei_pre_check_id = {leiPreCheckId}
            where rec_id in ({String.Join(", ", level2NamesIds.ToArray())})");
        }
    }
}
