package org.example.cinemabooking.controller;

import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.example.cinemabooking.entity.Screening;
import org.example.cinemabooking.repo.MovieRepo;
import org.example.cinemabooking.repo.ScreeningRepo;
import org.springframework.data.domain.PageRequest;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;

@RestController
@RequestMapping("/api/screenings")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class ScreeningController {
    private final ScreeningRepo repo;
    private final MovieRepo movies;

    @GetMapping
    public List<Screening> all(){ return repo.findAll(); }

    @PostMapping
    public Screening create(@RequestParam Long movieId, @RequestBody @Valid Screening s){
        s.setMovie(movies.findById(movieId).orElseThrow());
        return repo.save(s);
    }

    @PutMapping("/{id}")
    public Screening update(@PathVariable Long id, @RequestBody @Valid Screening s, @RequestParam Long movieId){
        Screening db = repo.findById(id).orElseThrow();
        db.setMovie(movies.findById(movieId).orElseThrow());
        db.setHall(s.getHall()); db.setTime(s.getTime());
        db.setSeats_total(s.getSeats_total()); db.setSeats_sold(s.getSeats_sold());
        return repo.save(db);
    }

    @DeleteMapping("/{id}") public void delete(@PathVariable Long id){ repo.deleteById(id); }

    @GetMapping("/search")
    public List<Screening> search(@RequestParam(required=false) Long movieId,
                                  @RequestParam(required=false) @DateTimeFormat(iso=DateTimeFormat.ISO.DATE_TIME) LocalDateTime from,
                                  @RequestParam(required=false) @DateTimeFormat(iso=DateTimeFormat.ISO.DATE_TIME) LocalDateTime to){
        return repo.search(movieId, from, to);
    }

    @GetMapping("/popular")
    public List<Screening> popular(@RequestParam(defaultValue="5") int top){
        return repo.mostPopular(PageRequest.of(0, top)).getContent();
    }
}
