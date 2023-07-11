namespace M03_Escola.Model
{
    public class AlunoTurma
    {
        public int Id { get; set; }
        public virtual Aluno Aluno { get; set;}
        public virtual Turma Turma { get; set;} 
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}
