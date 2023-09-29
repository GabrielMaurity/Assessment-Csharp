using CarBusiness;

namespace OperacoesCarro
{
    public class CarOperation
    {
        private IMRepository _repository;

        public CarOperation(IMRepository repository)
        {
            _repository = repository;



        }
        public void Adicionar()
        {
            _repository.Adicionar();
        }

    }
}

