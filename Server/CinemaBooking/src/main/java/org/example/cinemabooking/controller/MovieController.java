package org.example.cinemabooking.controller;

import org.example.cinemabooking.entity.Movie;
import org.example.cinemabooking.repo.MovieRepo;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/movies")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class MovieController {
    private final MovieRepo repo;

    @GetMapping public List<Movie> all(){ return repo.findAll(); }
    @GetMapping("/{id}") public Movie one(@PathVariable Long id){ return repo.findById(id).orElseThrow(); }
    @PostMapping public Movie create(@RequestBody @Valid Movie m){ return repo.save(m); }
    @PutMapping("/{id}") public Movie update(@PathVariable Long id, @RequestBody @Valid Movie m){
        Movie db = repo.findById(id).orElseThrow();
        db.setTitle(m.getTitle()); db.setGenre(m.getGenre()); db.setDuration(m.getDuration());
        return repo.save(db);
    }
    @DeleteMapping("/{id}") public void delete(@PathVariable Long id){ repo.deleteById(id); }
    @GetMapping("/by-genre/{g}") public List<Movie> byGenre(@PathVariable String g){ return repo.findByGenreIgnoreCase(g); }
}
