package com.cinemabooking.controller;
import com.cinemabooking.services.MovieService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;
@RestController
@RequestMapping("/api/movies")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class MovieController {
    private final MovieService service;

    @PostMapping
    public com.cinemabooking.db.Movie createMovie(@RequestBody com.cinemabooking.db.Movie m){
        return service.createMovie(m);
    }
    @GetMapping
    public List<com.cinemabooking.db.Movie> findAll(){
        return service.findAllMovies();
    }

    @GetMapping("/{id}")
    public com.cinemabooking.db.Movie findById(@PathVariable("id") Long id) {
        return service.findMovieById(id);
    }

    @PutMapping("/{id}")
    public com.cinemabooking.db.Movie updateMovie(@PathVariable Long id, @Valid @RequestBody com.cinemabooking.db.Movie m) {
        return service.updateMovie(id, m);
    }

    @DeleteMapping("/{id}")
    public void deleteMovie(@PathVariable Long id) {
        service.deleteMovie(id);
    }

    @GetMapping("/by-genre/{g}")
    public List<com.cinemabooking.db.Movie> findByGenre(@PathVariable String g) {
        return service.findByGenre(g);
    }
}
