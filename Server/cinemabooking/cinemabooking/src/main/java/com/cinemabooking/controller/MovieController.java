package com.cinemabooking.controller;

import com.cinemabooking.db.Movie;
import com.cinemabooking.repo.MovieRepo;
import com.cinemabooking.services.MovieService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;
@RestController
@RequestMapping("/api/movies")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5174","http://localhost:5173"})
public class MovieController {
    private final MovieRepo repo;

    @PostMapping
    public com.cinemabooking.db.Movie createMovie(@RequestBody com.cinemabooking.db.Movie m){
        return MovieService.createMovie(m);
    }
    @GetMapping
    public List<com.cinemabooking.db.Movie> findAll(){
        return MovieService.findAllMovies();
    }

    @GetMapping("/{id}")
    public com.cinemabooking.db.Movie findById(@PathVariable("id") Long id) {
        return MovieService.findMovieById(id);
    }

    @PutMapping("/{id}")
    public Movie updateMovie(@PathVariable Long id, @Valid @RequestBody Movie m) {
        return MovieService.updateMovie(id, m);
    }

    @DeleteMapping("/{id}")
    public void deleteMovie(@PathVariable Long id) {
        MovieService.deleteMovie(id);
    }

    @GetMapping("/by-genre/{g}")
    public List<Movie> findByGenre(@PathVariable String g) {
        return MovieService.findByGenre(g);
    }
}
