namespace M03_Escola.Model
{
    public class Boletim
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public virtual Aluno Aluno { get; set; }
        public int AlunoId { get; set; }

        public List<NotasMateria> NotasMaterias { get; set; }

        public void Update(Boletim boletim)
        {

            Data = boletim.Data;
        }

        public Boletim()
        {

        }
        //public Boletim(BoletimDTO boletim)
        //{

        //    Id = boletim.Id;
        //    Data = boletim.Data;
        //    AlunoId = boletim.AlunoId;
        //}
    }
}
