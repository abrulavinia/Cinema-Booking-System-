package com.cinemabooking.services;

import com.cinemabooking.repo.MovieRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;

@Service
@RequiredArgsConstructor
public class MovieService {
    private final MovieRepo movieRepo;

    public com.cinemabooking.db.Movie createMovie(com.cinemabooking.db.Movie m){
        return movieRepo.save(m);
    }

    public List<com.cinemabooking.db.Movie> findAllMovies(){
        return movieRepo.findAll();
    }

    public com.cinemabooking.db.Movie findMovieById(Long id){
        return movieRepo.findById(id).orElseThrow();
    }

    public com.cinemabooking.db.Movie updateMovie(Long id, com.cinemabooking.db.Movie m) {
        com.cinemabooking.db.Movie existing = movieRepo.findById(id).orElseThrow();
        existing.setTitle(m.getTitle());
        existing.setGenre(m.getGenre());
        existing.setDuration(m.getDuration());
        existing.setStatus(m.getStatus());
        return movieRepo.save(existing);
    }

    public void deleteMovie(Long id) {
        movieRepo.deleteById(id);
    }

    public List<com.cinemabooking.db.Movie> findByGenre(String genre) {
        return movieRepo.findByGenreIgnoreCase(genre);
    }

}
