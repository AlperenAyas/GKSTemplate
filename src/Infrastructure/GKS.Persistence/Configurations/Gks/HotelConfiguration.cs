using GKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence.Configurations.Gks
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Adress).IsRequired();
            builder.Property(x => x.Adress).HasMaxLength(256);

            builder.Property(x => x.CreatedTime).IsRequired();
            builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.Now);

            builder.Property(x => x.UpdatedTime).IsRequired(false);

            builder.Property(x => x.Deleted).IsRequired();
            builder.Property(x => x.Deleted).HasDefaultValue(false);

            builder.Property(x => x.WebSite).IsRequired(false);
            builder.Property(x => x.WebSite).HasMaxLength(128);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(256);

            builder.Property(x => x.Phone).IsRequired(false);
            builder.Property(x => x.Phone).HasMaxLength(32);
        }
    }
}
