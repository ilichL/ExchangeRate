namespace ExchangeRate.Data
{
    public class PatchModel
    {//нужен для ICurencyRepozitory
        public string PropertyName { get; set; }//ключ
        public object PropertyValue { get; set; }//значение
    }//объект ключ-значение
}