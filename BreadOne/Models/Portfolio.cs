using System.Text.Json;
using System.Text.Json.Serialization;

namespace BreadOne.Models
{
    /// <summary>
    /// 모델 클래스: Model, ViewModel, Dto, Object, Entity, ... 접미사를 쓸 수도 있음
    /// </summary>
    public class Portfolio
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("img")]
        public string Image { get; set; }
        public List<int> Ratings { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
