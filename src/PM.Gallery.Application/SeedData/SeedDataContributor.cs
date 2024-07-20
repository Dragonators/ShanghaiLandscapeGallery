using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.IRepository;
using PM.Gallery.Domain.ValueObjects;

namespace PM.Gallery.Application.SeedData;

public class SeedDataContributor
{
    private readonly IRepository<Image> _repository;

    public SeedDataContributor(IRepository<Image> repository)
    {
        _repository = repository;
    }

    public async Task SeedAsync()
    {
        // if (_repository.ExistsAny())
        // {
        //     return; // DB has been seeded
        // }

        var random = new Random();

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        var images = new List<Image>
        {
            await CreateImage("东方明珠",
                Path.Combine(baseDirectory, "SeedData", "imgs", "东方明珠.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 5, 4, 4, 5 },
                new List<string> { "Amazing!", "Beautiful!" },
                new List<string> { "Nature", "Landscape" }),
            await CreateImage("南京路",
                Path.Combine(baseDirectory, "SeedData", "imgs", "南京路.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 3, 4 },
                new List<string> { "Not bad!", "I like it!" },
                new List<string> { "City", "Architecture" }),
            // create 东方明珠
            await CreateImage("朱家角古镇",
                Path.Combine(baseDirectory, "SeedData", "imgs", "朱家角古镇.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 5, 5, 4 },
                new List<string> { "Very cultural!", "Nice place!" },
                new List<string> { "Culture", "History" }),

            await CreateImage("迪士尼乐园",
                Path.Combine(baseDirectory, "SeedData", "imgs", "迪士尼乐园.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 5, 5, 5, 4 },
                new List<string> { "Fun!", "Great for kids!" },
                new List<string> { "Amusement", "Family" }),

            await CreateImage("上海环球金融中心-上海塔",
                Path.Combine(baseDirectory, "SeedData", "imgs", "上海环球金融中心-上海塔.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 4, 4, 3 },
                new List<string> { "Tall buildings!", "Impressive!" },
                new List<string> { "Architecture", "Skyscraper" }),

            await CreateImage("上海博物馆",
                Path.Combine(baseDirectory, "SeedData", "imgs", "上海博物馆.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 4, 5, 5 },
                new List<string> { "Informative!", "Great exhibits!" },
                new List<string> { "Museum", "Exhibits" }),

            await CreateImage("外滩",
                Path.Combine(baseDirectory, "SeedData", "imgs", "外滩.jpg"),
                DateTime.Now.AddDays(-random.Next(1, 31)),
                new List<int> { 5, 5, 5, 4 },
                new List<string> { "Historical!", "Beautiful view!" },
                new List<string> { "Historical", "Landscape" })
        };

        await _repository.AddRangeAsync(images);
    }

    private static async Task<Image> CreateImage(string title, string path, DateTime createdAt, IEnumerable<int> ratings,
        IEnumerable<string> comments, IEnumerable<string> tags)
    {
        var random = new Random();
        var image = new Image(Guid.NewGuid(), title, await ImageHelper.ImageToBytes(path), createdAt);
        foreach (var rating in ratings)
        {
            image.AddRating(new Rating(rating, DateTime.Now.AddDays(-random.Next(1, 31))));
        }

        foreach (var comment in comments)
        {
            image.AddComment(new Comment(comment, DateTime.Now.AddDays(-random.Next(1, 31))));
        }

        foreach (var tag in tags)
        {
            image.AddTag(new Tag(tag, DateTime.Now.AddDays(-random.Next(1, 31))));
        }

        return image;
    }
}