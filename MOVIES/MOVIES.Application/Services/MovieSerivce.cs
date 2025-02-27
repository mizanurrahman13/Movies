using FluentValidation;
using MOVIES.Application.Models;
using MOVIES.Application.Repositories;

namespace MOVIES.Application.Services;

public class MovieSerivce : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IValidator<Movie> _movieValidator;
    private readonly IRatingRepository _ratingRepository;
    private readonly IValidator<GetAllMoviesOptions> _optionsValidator;

    public MovieSerivce(IMovieRepository movieRepository, IValidator<Movie> movieValidator, IRatingRepository ratingRepository, IValidator<GetAllMoviesOptions> optionsValidator)
    {
        _movieRepository = movieRepository;
        _movieValidator = movieValidator;
        _ratingRepository = ratingRepository;
        _optionsValidator = optionsValidator;
    }

    public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);
        return await _movieRepository.CreateAsync(movie, token);
    }

    public Task<Movie?> GetByIdAsync(Guid id, Guid? userId = default, CancellationToken token = default)
    {
        return _movieRepository.GetByIdAsync(id, userId, token);
    }

    public Task<Movie?> GetBySlugAsync(string slug, Guid? userId = default, CancellationToken token = default)
    {
        return _movieRepository.GetBySlugAsync(slug, userId, token);
    }

    public async Task<IEnumerable<Movie>> GetAllAsync(GetAllMoviesOptions options, CancellationToken token = default)
    {
        await _optionsValidator.ValidateAndThrowAsync(options, token);

        return await _movieRepository.GetAllAsync(options, token);
    }

    public async Task<Movie?> UpdateAsync(Movie movie, Guid? userId = default, CancellationToken token = default)
    {
        await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);
        var movieExists = await _movieRepository.ExistsByIdAsync(movie.Id, token);

        if (!movieExists)
            return null;

        await _movieRepository.UpdateAsync(movie, token);

        if (!userId.HasValue)
        {
            var rating = await _ratingRepository.GetRatingAsync(movie.Id, token);
            movie.Rating = rating;
            return movie;
        }

        var ratings = await _ratingRepository.GetRatingAsync(movie.Id, userId.Value, token);
        movie.Rating = ratings.Rating;
        movie.UserRaing = ratings.UserRating;

        return movie;
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return _movieRepository.DeleteByIdAsync(id, token);
    }
}
