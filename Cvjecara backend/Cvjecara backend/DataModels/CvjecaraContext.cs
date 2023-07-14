using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cvjecara_backend.DataModels;

public partial class CvjecaraContext : DbContext
{
    public CvjecaraContext()
    {
    }

    public CvjecaraContext(DbContextOptions<CvjecaraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buket> Bukets { get; set; }

    public virtual DbSet<Cvjet> Cvjets { get; set; }

    public virtual DbSet<Narudzba> Narudzbas { get; set; }

    public virtual DbSet<Sadrzajbuketum> Sadrzajbuketa { get; set; }

    public virtual DbSet<Sadrzajnarudzbe> Sadrzajnarudzbes { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_buket");

            entity.ToTable("buket");

            entity.HasIndex(e => e.Id, "buket_pk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Kolicina).HasColumnName("kolicina");
            entity.Property(e => e.Naziv)
                .HasMaxLength(255)
                .HasColumnName("naziv");
            entity.Property(e => e.Opis).HasColumnName("opis");
            entity.Property(e => e.Slika).HasColumnName("slika");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<Cvjet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_cvjet");

            entity.ToTable("cvjet");

            entity.HasIndex(e => e.Id, "cvjet_pk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cijena).HasColumnName("cijena");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Kolicina).HasColumnName("kolicina");
            entity.Property(e => e.Naziv)
                .HasMaxLength(255)
                .HasColumnName("naziv");
            entity.Property(e => e.Opis).HasColumnName("opis");
            entity.Property(e => e.Slika).HasColumnName("slika");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<Narudzba>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_narudzba");

            entity.ToTable("narudzba");

            entity.HasIndex(e => e.Id, "narudzba_pk").IsUnique();

            entity.HasIndex(e => e.UseId, "relationship_6_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cjena).HasColumnName("cjena");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(255)
                .HasDefaultValueSql("'cekanje na preuzimanje'::character varying")
                .HasColumnName("order_status");
            entity.Property(e => e.UseId).HasColumnName("use_id");

            entity.HasOne(d => d.Use).WithMany(p => p.Narudzbas)
                .HasForeignKey(d => d.UseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_narudzba_relations_users");
        });

        modelBuilder.Entity<Sadrzajbuketum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_sadrzajbuketa");

            entity.ToTable("sadrzajbuketa");

            entity.HasIndex(e => e.BukId, "relationship_1_fk");

            entity.HasIndex(e => e.CvjId, "relationship_2_fk");

            entity.HasIndex(e => e.Id, "sadrzajbuketa_pk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BukId).HasColumnName("buk_id");
            entity.Property(e => e.CvjId).HasColumnName("cvj_id");
            entity.Property(e => e.Kolicina).HasColumnName("kolicina");

            entity.HasOne(d => d.Buk).WithMany(p => p.Sadrzajbuketa)
                .HasForeignKey(d => d.BukId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sadrzajb_relations_buket");

            entity.HasOne(d => d.Cvj).WithMany(p => p.Sadrzajbuketa)
                .HasForeignKey(d => d.CvjId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sadrzajb_relations_cvjet");
        });

        modelBuilder.Entity<Sadrzajnarudzbe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_sadrzajnarudzbe");

            entity.ToTable("sadrzajnarudzbe");

            entity.HasIndex(e => e.CvjId, "relationship_3_fk");

            entity.HasIndex(e => e.BukId, "relationship_4_fk");

            entity.HasIndex(e => e.NarId, "relationship_5_fk");

            entity.HasIndex(e => e.Id, "sadrzajnarudzbe_pk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BukId).HasColumnName("buk_id");
            entity.Property(e => e.Cijena).HasColumnName("cijena");
            entity.Property(e => e.Column4).HasColumnName("column_4");
            entity.Property(e => e.CvjId).HasColumnName("cvj_id");
            entity.Property(e => e.Kolicina).HasColumnName("kolicina");
            entity.Property(e => e.NarId).HasColumnName("nar_id");

            entity.HasOne(d => d.Buk).WithMany(p => p.Sadrzajnarudzbes)
                .HasForeignKey(d => d.BukId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sadrzajn_relations_buket");

            entity.HasOne(d => d.Cvj).WithMany(p => p.Sadrzajnarudzbes)
                .HasForeignKey(d => d.CvjId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sadrzajn_relations_cvjet");

            entity.HasOne(d => d.Nar).WithMany(p => p.Sadrzajnarudzbes)
                .HasForeignKey(d => d.NarId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sadrzajn_relations_narudzba");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_users");

            entity.ToTable("users");

            entity.HasIndex(e => e.Id, "users_pk").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasDefaultValueSql("USER")
                .HasColumnName("role");
            entity.Property(e => e.Title)
                .HasMaxLength(60)
                .HasColumnName("title");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.VerifiedAdmin).HasColumnName("verified_admin");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
