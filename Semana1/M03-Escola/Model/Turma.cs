namespace M03_Escola.Model
{
    public class Turma
    {
        public int Id { get; set; }
        public string Curso { get; set; }
        public string Nome { get; set; }
        public virtual List<Aluno> Alunos { get; set; }
    }
}
