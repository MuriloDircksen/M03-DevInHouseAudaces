using M03_Escola.DataBase.Repositories;
using M03_Escola.Exceptions;
using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;
using M03_Escola.Services;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03_Escola.Test.Services
{
    public class MateriaServiceTests
    {
        [Test]
        public void Materia_InserirNovaMateria_Sucesso()
        {
            //Arrange
            var materiaRepositoryMock = new Mock<IMateriaRepository>();
            materiaRepositoryMock.Setup(x => x.Inserir(It.IsAny<Materia>()))
                .Returns<Materia>(x => x);

            var materiaService = new MateriaService(materiaRepositoryMock.Object);
            var materia = new Materia() { Nome = "Matematica" };

            var expectedMateria = new Materia() { Nome = "Matematica" };

            //Act
            var result = materiaService.Cadastrar(materia);
            //Assert 
            Assert.AreEqual(JsonConvert.SerializeObject(expectedMateria), JsonConvert.SerializeObject(result));
        }
        [Test]
        public void Materia_InserirMateriaDuplicada_ReturnError()
        {
            //Arrange
            var materiaRepositoryMock = new Mock<IMateriaRepository>();
            materiaRepositoryMock.Setup(x => x.ObterPorNome(It.IsAny<string>()))
                .Returns<string>(x =>
                {
                    return new List<Materia>()
                    {
                        new Materia()
                        {
                            Nome = x,
                            Id = 10
                        }
                    };
                }); // retorna uma lista de objetos de nome de materia indefinido, não travado. Uso de tipos dinamicos

            var materiaService = new MateriaService(materiaRepositoryMock.Object);
            var materia = new Materia() { Nome = "Matematica" };
            var expectedMessagem = "Matéria já cadastrada";


            //Act
            //Assert 
            var ex = Assert.Throws<RegistroDuplicadoException>(()=> materiaService.Cadastrar(materia));
            

            Assert.AreEqual(expectedMessagem, ex.Message);
        }
    }
}
