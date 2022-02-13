namespace Grad_Proj.Entites
{
    public class ItemImages
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}