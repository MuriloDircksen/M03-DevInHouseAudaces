using M03_Escola.Model;
using Microsoft.EntityFrameworkCore;

namespace M03_Escola.DataBase
{
    public class EscolaDBContexto : DbContext
    {
        public virtual DbSet<Aluno> Alunos { get; set; }

        public virtual DbSet<Turma> Turmas { get; set; }
        public virtual DbSet<AlunoTurma> AlunoTurmas { get; set; }
        public virtual DbSet<Materia> Materias { get; set; }
        public virtual DbSet<NotasMateria> NotasMaterias { get; set; }
        public virtual DbSet<Boletim> Boletims { get; set; }
        public EscolaDBContexto() { }
        public EscolaDBContexto(DbContextOptions<EscolaDBContexto> options)
            : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ServerConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(x => x.Login);
            modelBuilder.Entity<Usuario>().Property(x => x.Login)
                                           .HasMaxLength(50);

            modelBuilder.Entity<Usuario>().Property(x => x.Senha)
                                          .HasMaxLength(50);

            modelBuilder.Entity<Aluno>().ToTable("AlunoTB");

            modelBuilder.Entity<Aluno>().HasKey(x => x.Id)
                                        .HasName("Pk_aluno_id");

            modelBuilder.Entity<Aluno>().Property(x => x.Id)
                                        .HasColumnName("PK_ID")
                                        .HasColumnType("INT");

            modelBuilder.Entity<Aluno>().Property(x => x.Nome)
                                        .IsRequired()
                                        .HasColumnName("NOME")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(50);

            modelBuilder.Entity<Aluno>().Property(x => x.Sobrenome)
                                        .IsRequired()
                                        .HasColumnName("SOBRENOME")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(150);

            modelBuilder.Entity<Aluno>().Property(x => x.Idade)
                                        .IsRequired()
                                        .HasColumnName("Idade")
                                        .HasColumnType("INT");

           // modelBuilder.Entity<Aluno>().Ignore(x => x.Idade);

            modelBuilder.Entity<Aluno>().Property(x => x.Email)
                                        .IsRequired()
                                        .HasColumnName("EMAIL")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(50);


            modelBuilder.Entity<Aluno>().HasIndex(x => x.Email)
                                        .IsUnique();

            modelBuilder.Entity<Aluno>().Property(x => x.Genero)
                                        .HasColumnName("GENERO")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(20);

            modelBuilder.Entity<Aluno>().Property(x => x.Telefone)
                                        .HasColumnName("TELEFONE")
                                        .HasColumnType("VARCHAR")
                                        .HasMaxLength(30);

            modelBuilder.Entity<Aluno>().Property(x => x.DataNascimento)
                                        .HasColumnName("DATA_NASCIMENTO")
                                        .HasColumnType("datetime2");


            modelBuilder.Entity<Turma>().ToTable("TURMA");

            modelBuilder.Entity<Turma>().Property(x => x.Id)
                                        .HasColumnType("int")
                                        .HasColumnName("ID");

            modelBuilder.Entity<Turma>().HasKey(x => x.Id);


            modelBuilder.Entity<Turma>().Property(x => x.Curso)
                            .HasColumnType("varchar")
                            .HasMaxLength(50)
                            .HasDefaultValue("Curso Basico")
                            .HasColumnName("CURSO");

            modelBuilder.Entity<Turma>().Property(x => x.Nome)
                            .HasColumnType("varchar")
                            .HasMaxLength(50)
                            .HasColumnName("Nome");

            modelBuilder.Entity<Turma>().HasIndex(x => x.Nome)
                                        .IsUnique();

            modelBuilder.Entity<AlunoTurma>().ToTable("AlunoTurma");
            modelBuilder.Entity<AlunoTurma>().Property(x => x.Id)
                                              .HasColumnType("int")
                                              .HasColumnName("ID");
            modelBuilder.Entity<AlunoTurma>().Property(x => x.TurmaId)
                                             .HasColumnType("int")
                                             .HasColumnName("FK_Turma");
            modelBuilder.Entity<AlunoTurma>().Property(x => x.AlunoId)
                                             .HasColumnType("int")
                                             .HasColumnName("FK_Aluno");

            

            modelBuilder.Entity<Materia>().ToTable("Materia");

            modelBuilder.Entity<Materia>().Property(x => x.Id)
                                        .HasColumnType("int")
                                        .HasColumnName("ID");
            modelBuilder.Entity<Materia>().HasKey(x => x.Id);
            modelBuilder.Entity<Materia>().HasIndex(x => x.Nome)
                                        .IsUnique();

            modelBuilder.Entity<Boletim>().ToTable("BOLETIM");
            modelBuilder.Entity<Boletim>().Property(x => x.Id)
                                          .HasColumnType("int")
                                          .HasColumnName("ID");
            modelBuilder.Entity<Boletim>().HasKey(x => x.Id);


            modelBuilder.Entity<Boletim>().Property(x => x.Data)
                                            .HasColumnType("date")
                                           .HasColumnName("DATA");
            modelBuilder.Entity<Boletim>().Property(x => x.AlunoId)
                                        .HasColumnType("int")
                                        .HasColumnName("FK_Aluno");

            modelBuilder.Entity<NotasMateria>().ToTable("NotasMateria");
            modelBuilder.Entity<NotasMateria>().Property(x => x.Id)
                                               .HasColumnType("int")
                                               .HasColumnName("ID");
            modelBuilder.Entity<NotasMateria>().HasKey(x => x.Id);
            modelBuilder.Entity<NotasMateria>().Property(x => x.BoletimId)
                                               .HasColumnType("int")
                                               .HasColumnName("FK_Boletim");
            modelBuilder.Entity<NotasMateria>().Property(x => x.MateriaId)
                                               .HasColumnType("int")
                                               .HasColumnName("FK_Materia");
            modelBuilder.Entity<NotasMateria>().Property(x => x.Nota)
                                               .HasColumnType("int")
                                               .HasColumnName("Nota");

            //definição conexões entre as tabelas via fluent 

            modelBuilder.Entity<Boletim>().HasOne(x => x.Aluno)
                                          .WithMany(x => x.Boletins)
                                          .HasForeignKey(x => x.AlunoId);

            modelBuilder.Entity<Boletim>().HasMany(x => x.NotasMaterias)
                                          .WithOne(x => x.Boletim)
                                          .HasForeignKey(x => x.BoletimId);

            modelBuilder.Entity<NotasMateria>().HasOne(x => x.Materia)
                                               .WithMany(x => x.NotasMaterias)
                                               .HasForeignKey(x => x.MateriaId);
            modelBuilder.Entity<Aluno>().HasMany(x => x.Turmas)
                                        .WithMany(x => x.Alunos)
                                        .UsingEntity<AlunoTurma>(
                                            t => t.HasOne<Turma>(x => x.Turma).WithMany().HasForeignKey(x => x.TurmaId),
                                            a => a.HasOne<Aluno>(x => x.Aluno).WithMany().HasForeignKey(x => x.AlunoId)
                                            //um aluno aluno tem muitas turmas pela chave estrangeira alunoid de aluno turmas
                                            );






        }
    }
}
