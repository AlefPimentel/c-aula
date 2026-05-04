namespace NewImobiliaria.Models
{
    public class Imovel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public double? Area { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public int? Quartos { get; set; }
    }
}
