using DataAccess.Concrete.EntityFramework.Configuration.Extensions;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Entity
{
    public class TranslateConfiguration : BaseEntityConfiguration<Translate>
    {
        public override void EntityConfigure(EntityTypeBuilder<Translate> builder)
        {
        }
    }
}
