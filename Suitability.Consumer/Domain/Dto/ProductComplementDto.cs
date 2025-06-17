using Newtonsoft.Json;

namespace Suitability.Consumer.Domain.Dto
{
    public class ProductComplementDto
    {
        public required bool success { get; set; }
        public Data? data { get; set; }
    }

    public class Achievements
    {
        public int total { get; set; }
        public List<Highlighted>? highlighted { get; set; }
    }

    public class Agcom
    {
        public string? rating { get; set; }
        public string? descriptors { get; set; }
    }

    public class Cadpa
    {
        public string? rating { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string? description { get; set; }
    }

    public class ContentDescriptors
    {
        public List<int>? ids { get; set; }
        public string? notes { get; set; }
    }

    public class Data
    {
        public string? type { get; set; }
        public string? name { get; set; }
        public int steam_appid { get; set; }
        //public string? required_age { get; set; }
        public bool is_free { get; set; }
        public List<int>? dlc { get; set; }
        public string? detailed_description { get; set; }
        public string? about_the_game { get; set; }
        public string? short_description { get; set; }
        public string? supported_languages { get; set; }
        public string? header_image { get; set; }
        public string? capsule_image { get; set; }
        public string? capsule_imagev5 { get; set; }
        public string? website { get; set; }
        //public PcRequirements? pc_requirements { get; set; }

        //public PcRequirements? pc_requirements { get; set; }

        //public MacRequirements? mac_requirements { get; set; }
        //public LinuxRequirements? linux_requirements { get; set; }
        public List<string>? developers { get; set; }
        public List<string>? publishers { get; set; }
        public List<int>? packages { get; set; }
        public List<PackageGroup>? package_groups { get; set; }
        public Platforms? platforms { get; set; }
        public List<Category>? categories { get; set; }
        public List<Genre>? genres { get; set; }
        public List<Screenshot>? screenshots { get; set; }
        public List<Movie>? movies { get; set; }
        public Recommendations? recommendations { get; set; }
        public Achievements? achievements { get; set; }
        public ReleaseDate? release_date { get; set; }
        public SupportInfo? support_info { get; set; }
        public string? background { get; set; }
        public string? background_raw { get; set; }
        public ContentDescriptors? content_descriptors { get; set; }
        public Ratings? ratings { get; set; }
    }

    public class Dejus
    {
        public string? rating { get; set; }
        public string? descriptors { get; set; }
    }

    public class Genre
    {
        public string? id { get; set; }
        public string? description { get; set; }
    }

    public class Highlighted
    {
        public string? name { get; set; }
        public string? path { get; set; }
    }

    public class LinuxRequirements
    {
        public string? minimum { get; set; }
    }

    public class MacRequirements
    {
        public string? minimum { get; set; }
    }

    public class Movie
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? thumbnail { get; set; }
        public Webm? webm { get; set; }
        public Mp4? mp4 { get; set; }
        public bool highlight { get; set; }
    }

    public class Mp4
    {
        [JsonProperty("480")]
        public string? _480 { get; set; }
        public string? max { get; set; }
    }

    public class PackageGroup
    {
        public string? name { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? selection_text { get; set; }
        public string? save_text { get; set; }
        public int display_type { get; set; }
        public string? is_recurring_subscription { get; set; }
        public List<Sub>? subs { get; set; }
    }

    public class PcRequirements
    {
        public string? minimum { get; set; }
    }

    public class Platforms
    {
        public bool windows { get; set; }
        public bool mac { get; set; }
        public bool linux { get; set; }
    }

    public class Ratings
    {
        public Usk? usk { get; set; }
        public Agcom? agcom { get; set; }
        public Cadpa? cadpa { get; set; }
        public Dejus? dejus { get; set; }
        public SteamGermany? steam_germany { get; set; }
    }

    public class Recommendations
    {
        public int total { get; set; }
    }

    public class ReleaseDate
    {
        public bool coming_soon { get; set; }
        public string? date { get; set; }
    }

    public class Root
    {
        public ProductComplementDto? game { get; set; }
    }

    public class Screenshot
    {
        public int id { get; set; }
        public string? path_thumbnail { get; set; }
        public string? path_full { get; set; }
    }

    public class SteamGermany
    {
        public string? rating_generated { get; set; }
        public string? rating { get; set; }
        public string? required_age { get; set; }
        public string? banned { get; set; }
        public string? use_age_gate { get; set; }
        public string? descriptors { get; set; }
    }

    public class Sub
    {
        public int packageid { get; set; }
        public string? percent_savings_text { get; set; }
        public int percent_savings { get; set; }
        public string? option_text { get; set; }
        public string? option_description { get; set; }
        public string? can_get_free_license { get; set; }
        public bool is_free_license { get; set; }
        public int price_in_cents_with_discount { get; set; }
    }

    public class SupportInfo
    {
        public string? url { get; set; }
        public string? email { get; set; }
    }

    public class Usk
    {
        public string? rating { get; set; }
        public string? descriptors { get; set; }
    }

    public class Webm
    {
        [JsonProperty("480")]
        public string? _480 { get; set; }
        public string? max { get; set; }
    }
}
