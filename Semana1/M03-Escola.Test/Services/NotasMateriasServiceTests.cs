using M03_Escola.Interfaces.Repositories;
using M03_Escola.Model;
using M03_Escola.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03_Escola.Test.Services
{
    public class NotasMateriasServiceTests
    {
        [Test]
        public void Cadastrar_NotaMenorQueZero_ReturnError()
        {
            //Arange
            var notasMateriasRepositryMock = new Mock<INotasMateriaRepository>();

            notasMateriasRepositryMock.Setup(x => x.Inserir(It.IsAny<NotasMateria>()))
                                                    .Returns<NotasMateria>(x => { x.Id = 10; //sempre retorna um id gerado automatico
                                                        return x;
                                                    });

            var notasMateriaService = new NotasMateriaService(notasMateriasRepositryMock.Object);

            var notasMateria = new NotasMateria() { Nota = -1};
            var expectedParam = "Nota";
            var expectedMessage = "Nota deve ser maior que zero";
            var expectedValue = -1;

            //ACT
            

            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => 
            { notasMateriaService.Cadastrar(notasMateria);});

            Assert.AreEqual(expectedParam, ex.ParamName);
            Assert.IsTrue(ex.Message.Contains(expectedMessage));
            Assert.AreEqual(expectedValue, ex.ActualValue);
        }
        [Test]
        public void Cadastrar_NotamaiorQueDeis_ReturnError()
        {
            //Arange
            var notasMateriasRepositryMock = new Mock<INotasMateriaRepository>();

            notasMateriasRepositryMock.Setup(x => x.Inserir(It.IsAny<NotasMateria>()))
                                                    .Returns<NotasMateria>(x => {
                                                        x.Id = 10; 
                                                        return x;
                                                    });

            var notasMateriaService = new NotasMateriaService(notasMateriasRepositryMock.Object);

            var notasMateria = new NotasMateria() { Nota = 11 };
            var expectedParam = "Nota";
            var expectedMessage = "Nota deve ser menor ou igual a 10";
            var expectedValue = 11;

            //ACT


            //Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            { notasMateriaService.Cadastrar(notasMateria); });

            Assert.AreEqual(expectedParam, ex.ParamName);
            Assert.IsTrue(ex.Message.Contains(expectedMessage));
            Assert.AreEqual(expectedValue, ex.ActualValue);
        }


    }
}
