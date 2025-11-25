package com.cinemabooking.controller;

import com.cinemabooking.db.Screening;
import com.cinemabooking.services.ScreeningService;
import lombok.RequiredArgsConstructor;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/screenings")
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class ScreeningController {

    private final ScreeningService service;
    @PostMapping
    public com.cinemabooking.db.Screening create(@RequestBody com.cinemabooking.db.Screening s) {
        return service.create(s);
    }

    @GetMapping
    public List<com.cinemabooking.db.Screening> all() {
        return service.findAll();
    }

    @GetMapping("/id/{id}")
    public com.cinemabooking.db.Screening one(@PathVariable Long id) {
        return service.findById(id);
    }

    @PutMapping("/id/{id}")
    public com.cinemabooking.db.Screening update(@PathVariable Long id, @RequestBody com.cinemabooking.db.Screening s) {
        return service.update(id, s);
    }

    @DeleteMapping("/id/{id}")
    public void delete(@PathVariable Long id) {
        service.delete(id);
    }

    @GetMapping("/search")
    public List<Screening> search(
            @RequestParam(required = false) Long movieId,
            @RequestParam(required = false)
            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime from,
            @RequestParam(required = false)
            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime to) {
        return service.search(movieId, from, to);
    }
    @GetMapping("/today")
    public List<Screening> screeningToday() {
        return service.getToday();
    }
    @GetMapping("/popular")
    public List<Screening> popular(@RequestParam(defaultValue = "5") int top) {
        return service.getMostPopularScreening(top);
    }
}
