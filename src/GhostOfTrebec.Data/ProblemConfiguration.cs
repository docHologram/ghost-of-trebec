using GhostOfTrebec.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostOfTrebec.Data
{
    public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
    {
        public void Configure(EntityTypeBuilder<Problem> builder)
        {
            builder.ToTable("Problems", "game");

            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.Property(p => p.Question);

            builder.OwnsMany(p => p.Answers, answerBuilder =>
            {
                answerBuilder.ToTable("Answers", "game");

                answerBuilder.Property<int>("Id");
                answerBuilder.HasKey("Id");

                answerBuilder.Property<int>("ProblemId");
                answerBuilder.HasIndex("ProblemId");

                answerBuilder.Property(a => a.IsCorrect);

                answerBuilder.Property(a => a.Text)
                    .HasMaxLength(450);
            });
        }
    }
}
