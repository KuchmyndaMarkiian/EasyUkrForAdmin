namespace EasyUkr.WebApi.Models.RestModels
{
    //Get words
    public class Word : Topic
    {
        public int ParentId { get; set; }
    }
}