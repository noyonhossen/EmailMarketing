using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.UnitOfWorks.SMTP;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EmailMarketing.Common.Extensions;
using EmailMarketing.Framework.Entities.SMTP;

namespace EmailMarketing.Framework.Services.SMTP
{
    public class SMTPService : ISMTPService
    {
        private ISMTPUnitOfWork _smtpUnitOfWork;

        public SMTPService(ISMTPUnitOfWork smtpUnitOfWork)
        {
            _smtpUnitOfWork = smtpUnitOfWork;
        }

        public async Task<(IList<SMTPConfig> Items, int Total, int TotalFilter)> GetAllAsync(
            Guid? userId,string searchText, string orderBy, int pageIndex, int pageSize)
        {
            var columnsMap = new Dictionary<string, Expression<Func<SMTPConfig, object>>>()
            {
                ["Server"] = v => v.Server,
                ["Port"] = v => v.Port,
                ["SenderName"] = v => v.SenderName,
                ["SenderEmail"] = v => v.SenderEmail,
                ["UserName"] = v => v.UserName,
                ["Password"] = v => v.Password,
                ["EnableSSL"] = v => v.EnableSSL
            };

            var result = await _smtpUnitOfWork.SMTPRepository.GetAsync<SMTPConfig>(
                x => x, x => x.Server.Contains(searchText) && (!userId.HasValue || x.UserId == userId.Value),
                x => x.ApplyOrdering(columnsMap, orderBy), null,
                pageIndex, pageSize, true);

            return (result.Items, result.Total, result.TotalFilter);
        }

        public async Task<SMTPConfig> GetByIdAsync(Guid id)
        {
            return await _smtpUnitOfWork.SMTPRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(SMTPConfig entity)
        {
            var isExists = await _smtpUnitOfWork.SMTPRepository.IsExistsAsync(x => x.Server == entity.Server && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Server));

            await _smtpUnitOfWork.SMTPRepository.AddAsync(entity);
            await _smtpUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(SMTPConfig entity)
        {
            var isExists = await _smtpUnitOfWork.SMTPRepository.IsExistsAsync(x => x.Server == entity.Server && x.Id != entity.Id);
            if (isExists)
                throw new DuplicationException(nameof(entity.Server));

            var updateEntity = await GetByIdAsync(entity.Id);
            updateEntity.Server = entity.Server;
            updateEntity.Port = entity.Port;
            updateEntity.SenderName = entity.SenderName;
            updateEntity.SenderEmail = entity.SenderEmail;
            updateEntity.UserName = entity.UserName;
            updateEntity.Password = entity.Password;
            updateEntity.EnableSSL = entity.EnableSSL;

            await _smtpUnitOfWork.SMTPRepository.UpdateAsync(updateEntity);
            await _smtpUnitOfWork.SaveChangesAsync();
        }

        public async Task<SMTPConfig> DeleteAsync(Guid id)
        {
            var smtp = await GetByIdAsync(id);
            await _smtpUnitOfWork.SMTPRepository.DeleteAsync(id);
            await _smtpUnitOfWork.SaveChangesAsync();
            return smtp;
        }

        public void Dispose()
        {
            _smtpUnitOfWork?.Dispose();
        }

        public async Task<IList<SMTPConfig>> GetAllSMTPConfig(Guid? userId)
        {
            return await _smtpUnitOfWork.SMTPRepository.GetAsync(x => x, x => x.UserId == userId, null, null, true);
        }
    }
}
