package com.cinemabooking.services;

import com.cinemabooking.repo.MovieRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
@Service
public class MovieService {
    private static MovieRepo movieRepo;

    @Autowired
    public MovieService(MovieRepo movieRepo) {
        MovieService.movieRepo = movieRepo;
    }

    public static com.cinemabooking.db.Movie createMovie(com.cinemabooking.db.Movie m){
        return movieRepo.save(m);
    }

    public static List<com.cinemabooking.db.Movie> findAllMovies(){
        return movieRepo.findAll();
    }

    public static com.cinemabooking.db.Movie findMovieById(Long id){
        return movieRepo.findById(id).orElseThrow();
    }

    public static com.cinemabooking.db.Movie updateMovie(Long id, com.cinemabooking.db.Movie m) {
        com.cinemabooking.db.Movie existing = movieRepo.findById(id).orElseThrow();
        existing.setTitle(m.getTitle());
        existing.setGenre(m.getGenre());
        existing.setDuration(m.getDuration());
        existing.setStatus(m.getStatus());
        return movieRepo.save(existing);
    }

    public static void deleteMovie(Long id) {
        movieRepo.deleteById(id);
    }

    public static List<com.cinemabooking.db.Movie> findByGenre(String genre) {
        return movieRepo.findByGenreIgnoreCase(genre);
    }

}
