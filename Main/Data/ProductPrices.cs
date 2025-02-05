public class Product
{
    public double Price { get; set; }
}

public class Vegetable : Product
{
    public VegetableType Type { get; set; }
}

public class PriceList
{
    public List<Vegetable> Vegetables { get; set; } = new List<Vegetable>();

    public PriceList(string priceFile)
    {
        SetVegetablePricesFromCsv(priceFile);
    }

    private void SetVegetablePricesFromCsv(string csvFilePath)
    {
        if (csvFilePath == "") {
            Vegetables = new List<Vegetable>();
        }
        
        try
        {
            string pricePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", csvFilePath);
            Vegetables = File.ReadAllLines(pricePath)
                           .Skip(1)
                           .Select(x => x.Split(','))
                           .Select(x => new Vegetable
                           {
                               Type = (VegetableType)Enum.Parse(typeof(VegetableType), x[0], true),
                               Price = double.Parse(x[1]) // TODO: Possible apply discount function here
                           }).ToList();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}