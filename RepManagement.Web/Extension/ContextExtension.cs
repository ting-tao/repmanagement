using RepManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepManagement.Web.Extension
{
    public static class ContextExtension
    {
        public static void AttachAddEntity(this RepManagementContext context,object entry,string userId)
        {
            context.Entry(entry).Property(ConstData.ShadowPropName_CreatedBy).CurrentValue = Guid.Parse(userId);
            context.Entry(entry).Property(ConstData.ShadowPropName_CreatedDate).CurrentValue = DateTime.Now;
            context.Entry(entry).Property(ConstData.ShadowPropName_IsDeleted).CurrentValue = false;
        }

        public static void AttachUpdateEntity(this RepManagementContext context,object entry,string userId)
        {
            context.Entry(entry).Property(ConstData.ShadowPropName_UpdatedBy).CurrentValue = Guid.Parse(userId);
            context.Entry(entry).Property(ConstData.ShadowPropName_UpdatedDate).CurrentValue = DateTime.Now;
            
        }

        public static void AttachDeleteEntity(this RepManagementContext context, object entry, string userId)
        {
            context.Entry(entry).Property(ConstData.ShadowPropName_UpdatedBy).CurrentValue = Guid.Parse(userId);
            context.Entry(entry).Property(ConstData.ShadowPropName_UpdatedDate).CurrentValue = DateTime.Now;
            context.Entry(entry).Property(ConstData.ShadowPropName_IsDeleted).CurrentValue = true;
        }
    }
}
