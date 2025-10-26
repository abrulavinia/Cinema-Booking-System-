package com.cinemabooking.controller;

import com.cinemabooking.db.Screening;
import com.cinemabooking.services.ScreeningService;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.*;
import org.springframework.data.domain.Page;

import java.time.LocalDateTime;
import java.util.List;

@RestController
@RequestMapping("/api/screenings")
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173","http://localhost:5174"})
public class ScreeningController {

    @PostMapping
    public com.cinemabooking.db.Screening create(@RequestBody com.cinemabooking.db.Screening s) {
        return ScreeningService.create(s);
    }

    @GetMapping
    public List<com.cinemabooking.db.Screening> all() {
        return ScreeningService.findAll();
    }

    @GetMapping("/{id}")
    public com.cinemabooking.db.Screening one(@PathVariable Long id) {
        return ScreeningService.findById(id);
    }

    @PutMapping("/{id}")
    public com.cinemabooking.db.Screening update(@PathVariable Long id, @RequestBody com.cinemabooking.db.Screening s) {
        return ScreeningService.update(id, s);
    }

    @DeleteMapping("/{id}")
    public void delete(@PathVariable Long id) {
        ScreeningService.delete(id);
    }

    @GetMapping("/search")
    public List<Screening> search(
            @RequestParam(required = false) Long movieId,
            @RequestParam(required = false)
            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime from,
            @RequestParam(required = false)
            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) LocalDateTime to) {
        return ScreeningService.search(movieId, from, to);
    }
    @GetMapping("/popular")
    public List<Screening> popular(@RequestParam(defaultValue = "5") int top) {
        return ScreeningService.mostPopular(0, top).getContent();
    }
}
