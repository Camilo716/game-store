using System.Collections;
using GameStore.Api.Dtos.GameDtos;
using GameStore.Tests.Seed;

namespace GameStore.Tests.Api.ClassData;

public class InvalidGamesPutRequestTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            new GamePutRequest() { Game = null }
        ];

        SimpleGameWithIdDto invalidGameMissingName = GetValidGameDto();
        invalidGameMissingName.Name = null;
        yield return
        [
            new GamePutRequest() { Game = invalidGameMissingName }
        ];

        SimpleGameWithIdDto invalidGameMissingPrice = GetValidGameDto();
        invalidGameMissingPrice.Price = default;
        yield return
        [
            new GamePutRequest() { Game = invalidGameMissingPrice }
        ];

        SimpleGameWithIdDto invalidGameNegativeUnitsInStock = GetValidGameDto();
        invalidGameNegativeUnitsInStock.UnitsInStock = -1;
        yield return
        [
            new GamePutRequest() { Game = invalidGameNegativeUnitsInStock }
        ];

        yield return
        [
            new GamePutRequest() { Game = GetValidGameDto(), Publisher = Guid.Empty }
        ];

        SimpleGameWithIdDto invalidGameMissingId = GetValidGameDto();
        invalidGameMissingId.Id = Guid.Empty;
        yield return
        [
            new GamePutRequest() { Game = invalidGameMissingId }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static SimpleGameWithIdDto GetValidGameDto()
    {
        return new()
        {
            Id = GameSeed.GearsOfWar.Id,
            Name = GameSeed.GearsOfWar.Name,
            Key = GameSeed.GearsOfWar.Key,
            Description = GameSeed.GearsOfWar.Description,
            Price = GameSeed.GearsOfWar.Price,
            UnitsInStock = GameSeed.GearsOfWar.UnitsInStock,
            Discount = GameSeed.GearsOfWar.Discount,
        };
    }
}