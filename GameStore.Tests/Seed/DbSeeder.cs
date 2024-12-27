using GameStore.Core.Models;
using GameStore.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests.Seed;

internal class DbSeeder
{
    internal static void SeedData(GameStoreDbContext context)
    {
        var platforms = PlatformSeed.GetPlatforms();
        var genres = GenreSeed.GetGenres();
        var publishers = PublisherSeed.GetPublishers();
        var games = GameSeed.GetGames();
        var comments = CommentSeed.GetComments();

        AttachGenresToGames(genres, games);
        AttachGamesToGenres(genres, games);

        AttachPlatformsToGames(platforms, games);
        AttachGamesToPlatforms(platforms, games);

        context.Platforms.AddRange(platforms);
        context.Genres.AddRange(genres);
        context.Publishers.AddRange(publishers);
        context.Games.AddRange(games);
        context.Comments.AddRange(comments);

        context.SaveChanges();
    }

    private static void AttachPlatformsToGames(List<Platform> platforms, List<Game> games)
    {
        games.ForEach(game =>
        {
            var platformsOfGame = platforms
                .Where(p => game.Platforms.Select(p => p.Id).Contains(p.Id))
                .ToList();

            game.Platforms = platformsOfGame;
        });
    }

    private static void AttachGamesToPlatforms(List<Platform> platforms, List<Game> games)
    {
        platforms.ForEach(platform =>
        {
            var gamesOfplatform = games
                .Where(g => g.Platforms.Select(g => g.Id).Contains(platform.Id))
                .ToList();

            platform.Games = gamesOfplatform;
        });
    }

    private static void AttachGamesToGenres(List<Genre> genres, List<Game> games)
    {
        genres.ForEach(genre =>
        {
            var gamesOfGenre = games
                .Where(g => g.Genres.Select(g => g.Id).Contains(genre.Id))
                .ToList();

            genre.Games = gamesOfGenre;
        });
    }

    private static void AttachGenresToGames(List<Genre> genres, List<Game> games)
    {
        games.ForEach(game =>
        {
            var genresOfGame = genres
                .Where(g => game.Genres.Select(g => g.Id).Contains(g.Id))
                .ToList();

            game.Genres = genresOfGame;
        });
    }
}