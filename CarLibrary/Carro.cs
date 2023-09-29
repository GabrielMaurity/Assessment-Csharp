namespace CarLibrary
{
    public class Carro
    {       
        public Guid Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime Ano { get; set; }
        public double Preco { get; set; }
        public bool Possui4Portas { get; set; }
    }

}
