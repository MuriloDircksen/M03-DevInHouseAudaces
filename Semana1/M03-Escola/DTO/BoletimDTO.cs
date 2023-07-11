using M03_Escola.Model;

namespace M03_Escola.DTO
{
    public class BoletimDTO
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int AlunoId { get; set; }


        public BoletimDTO()
        {

        }
        public BoletimDTO(Boletim boletim)
        {

            Id = boletim.Id;
            Data = boletim.Data;
            AlunoId = boletim.AlunoId;
        }
    }
}
