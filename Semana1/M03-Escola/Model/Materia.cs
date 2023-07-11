using M03_Escola.DTO;

namespace M03_Escola.Model
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual List<NotasMateria> NotasMaterias { get; set; }

        public Materia() { }

        public Materia(MateriaDTO materiaDTO) 
        { 
            Id = materiaDTO.Id;
            Nome = materiaDTO.Nome;
        }

        public void Update(Materia materia)
        {
            Id = materia.Id;
            Nome = materia.Nome;
        }
    }
}
