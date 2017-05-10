namespace EasyUkr.WebApi.Models.RestModels
{
        //Get topis
        public class Topic
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Translate { get; set; }
            public int TranslateImageId { get; set; }
        }
    
}