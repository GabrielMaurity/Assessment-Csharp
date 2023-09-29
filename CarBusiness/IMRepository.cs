using System;
using CarLibrary;

namespace CarBusiness
{
    public interface IMRepository
    {
        void Adicionar();
        void Listar();
        void Atualizar();
        void Excluir();

    }
}
