using M03_Escola.DTO;

namespace M03_Escola.Model
{
    public class NotasMateria
    {
        public int Id { get; set; }
        public Boletim Boletim { get; set; }
        public int BoletimId { get; set; }
        public Materia Materia { get; set; }
        public int MateriaId { get; set; }
        public int Nota { get; set; }

        public NotasMateria() { }

        public NotasMateria(NotasMateriaDTO notasMateriaDTO)
        {
            Id = notasMateriaDTO.Id;
            BoletimId = notasMateriaDTO.BoletimId;
            MateriaId = notasMateriaDTO.MateriaId;
            Nota = notasMateriaDTO.Nota;
        }

        public void Update(NotasMateria notasMateria)
        {
            Id = notasMateria.Id;
            BoletimId = notasMateria.BoletimId;
            MateriaId = notasMateria.MateriaId;
            Nota = notasMateria.Nota;
        }
    }    
}
